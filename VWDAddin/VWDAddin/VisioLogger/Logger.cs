using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.IO;
using VWDAddin.VisioLogger.Actions;
using VWDAddin.Synchronize;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger
{
    public class Logger
    {
        private List<BaseAction> actionList = new List<BaseAction>();
        private int currentAction = -1;

        public Logger(LoggerManager LoggerManager, Document Document)
        {
            Trace.WriteLine("Create logger for " + Document.Name);
            associatedDocument = Document;
            loggerManager = LoggerManager;

            Document.Application.PurgeUndo();

            CreateDSLControlPoint();
        }

        public void Cleanup()
        {
            Trace.WriteLine("Cleanup logger for " + Document.Name);
            RemoveDSLControlPoint();
        }

        private LoggerManager loggerManager;
        public LoggerManager LoggerManager
        {
            get { return loggerManager; }
        }

        private Document associatedDocument;
        public Document Document
        {
            get { return associatedDocument; }
        }

        private WordDocument _wordDocument = new WordDocument();
        public WordDocument WordDocument
        {
            get { return _wordDocument; }
            set { _wordDocument = WordDocument; }
        }

        private DslDocument dslDocument = null;
        public DslDocument DslDocument
        {
            get { return dslDocument; }
            set { dslDocument = value; }
        }

        private bool active = true;
        public bool Active
        {
            get { return active; }
            set { active = value; Trace.WriteLine("Logger " + (value ? "Activated" : "Deactivated")); }
        }

        public void Add(BaseAction Action)
        {
            if (active)
            {
                currentAction++;
                actionList.RemoveRange(currentAction, actionList.Count - currentAction);
                actionList.Add(Action);
                Document.Application.AddUndoUnit(new UndoUnit(this));
            }
        }

        public BaseAction CurrentAction
        {
            get { return actionList[currentAction]; }
        }

        public void Undo()
        {
            Trace.WriteLine("Undoing Action " + currentAction + " in " + associatedDocument.Name);
            currentAction--;
        }

        public void Redo()
        {
            currentAction++;
            Trace.WriteLine("Redoing Action " + currentAction + " in " + associatedDocument.Name);
        }

        public void ApplyChanges()
        {
            Trace.WriteLine("Applying Changes in " + associatedDocument.Name + (Active ? "" : " Refused"));

            if (!Active) return;

            Trace.Indent();
            try
            {
                // Инициализация dsl-документа и возврат к контрольной точке
                String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
                if (File.Exists(DslPath))
                {
                    String TempDslPath = VisioHelpers.GetTempDSLPath(associatedDocument);
                    File.Copy(TempDslPath, DslPath, true);

                    dslDocument = new DslDocument();
                    dslDocument.Load(DslPath);
                }

                if (dslDocument != null)
                {
                    // Внесение изменений
                    for (int i = 0; i <= currentAction; i++)
                    {
                        Trace.WriteLine("Apply " + actionList[i].ToString());
                        actionList[i].Apply(this);
                    }

                    // Синхронизация всего остального
                    new DslSync(this).Synchronize();

                    // Сохранение dsl-документа
                    File.WriteAllText(DslPath + ".diagram", String.Empty);
                    dslDocument.Save(DslPath);
                    dslDocument = null;
                }
            }
            catch(Exception e)
            {
                Debug.Indent();
                Debug.WriteLine(e.TargetSite + ": " + e.Message);
                Debug.WriteLine(e.StackTrace);
                Debug.Unindent();
            }
            Trace.Unindent();
        }

        /// <summary>Инициализация контрольной точки, от которой будут 
        /// отсчитываться все изменения в данном логе</summary>
        private void CreateDSLControlPoint()
        {
            String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
            if (File.Exists(DslPath))
            {
                File.Copy(DslPath, VisioHelpers.GetTempDSLPath(associatedDocument), true);
            }
        }

        /// <summary>Уничтожение всей вспомогательной информации</summary>
        private void RemoveDSLControlPoint()
        {
            //TODO пока будем тут сохранять инфу для сравнения.
            // потом надо будет переписать DSL-Comparer для сравнения vsd с dsl
            String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
            if (File.Exists(DslPath))
            {
                File.Copy(DslPath, VisioHelpers.GetTempDSLPath(associatedDocument), true);
            }
        }

        new public String ToString()
        {
            String s = base.ToString();
            for (int i = 0; i <= currentAction; i++)
            {
                s += "\n" + i + ". " + actionList[i].ToString();
            }
            return s;
        }
    }
}
