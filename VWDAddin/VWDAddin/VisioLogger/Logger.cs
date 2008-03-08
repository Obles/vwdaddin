using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.IO;
using VWDAddin.VisioLogger.Actions;

namespace VWDAddin.VisioLogger
{
    public class Logger
    {
        private List<BaseAction> actionList = new List<BaseAction>();
        private int currentAction = -1;

        private WordDocument m_wordDocument;
        public WordDocument wordDocument
        {
            get { return m_wordDocument; }
        }

        public Logger(Document Document)
        {
            Trace.WriteLine("Create logger for " + Document.Name);
            associatedDocument = Document;

            CreateDSLControlPoint();
        }

        public void Cleanup()
        {
            Trace.WriteLine("Cleanup logger for " + Document.Name);          
            RemoveDSLControlPoint();
        }

        private Document associatedDocument;
        public Document Document
        {
            get { return associatedDocument; }
        }

        public void Add(BaseAction Action)
        {
            currentAction++;
            actionList.RemoveRange(currentAction, actionList.Count - currentAction);
            actionList.Add(Action);
            Document.Application.AddUndoUnit(new UndoUnit(this));
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
            Trace.WriteLine("Applying Changes in " + associatedDocument.Name);
            for (int i = 0; i <= currentAction; i++)
            {
                //actionList[i].Apply(Document);
            }
        }

        /// <summary>Инициализация контрольной точки, от которой будут 
        /// отсчитываться все изменения в данном логе</summary>
        private void CreateDSLControlPoint()
        {
            String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
            if (File.Exists(DslPath))
            {
                File.Copy(DslPath, VisioHelpers.GetTempDSLPath(associatedDocument));
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
                File.Copy(DslPath, VisioHelpers.GetTempDSLPath(associatedDocument));
            }
        }
    }
}
