using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.IO;
using VWDAddin.VisioLogger.Actions;
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
            CreateWordControlPoint();
        }

        public void Cleanup()
        {
            Trace.WriteLine("Cleanup logger for " + Document.Name);
            RemoveDSLControlPoint();
            RemoveWordControlPoint();
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
                // ������������� dsl-��������� � ������� � ����������� �����
                String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
                if (File.Exists(DslPath))
                {
                    String TempDslPath = VisioHelpers.GetTempDSLPath(associatedDocument);
                    File.Copy(TempDslPath, DslPath, true);

                    dslDocument = new DslDocument();
                    dslDocument.Load(DslPath);
                }

                // ������������� word-��������� � ������� � ����������� �����
                //String wordPath = VisioHelpers.GetWordPath(associatedDocument);
                //String tempWordPath = VisioHelpers.GetTempWordPath(associatedDocument);
                //if (File.Exists(wordPath) && File.Exists(tempWordPath))
                //{
                //    if (WordDocument.IsAssociated)
                //        WordDocument.CloseWordDocument();
                //    File.Copy(tempWordPath, wordPath, true);
                //    WordDocument.ParseDocx(wordPath);                                        
                //}

                // �������� ���������
                for (int i = 0; i <= currentAction; i++)
                {
                    Trace.WriteLine("Apply " + actionList[i].ToString());
                    actionList[i].Apply(this);
                }

                // ���������� dsl-���������
                if (dslDocument != null)
                {
                    File.WriteAllText(DslPath + ".diagram", String.Empty);
                    dslDocument.Save(DslPath);
                    dslDocument = null;
                }

                // ���������� word-���������
                //WordDocument.CloseWordDocument();
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

        /// <summary>������������� ����������� �����, �� ������� ����� 
        /// ������������� ��� ��������� � ������ ����</summary>
        private void CreateDSLControlPoint()
        {
            String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
            if (File.Exists(DslPath))
            {
                File.Copy(DslPath, VisioHelpers.GetTempDSLPath(associatedDocument), true);
            }
        }

        /// <summary>����������� ���� ��������������� ����������</summary>
        private void RemoveDSLControlPoint()
        {
            //TODO ���� ����� ��� ��������� ���� ��� ���������.
            // ����� ���� ����� ���������� DSL-Comparer ��� ��������� vsd � dsl
            String DslPath = VisioHelpers.GetDSLPath(associatedDocument);
            if (File.Exists(DslPath))
            {
                File.Copy(DslPath, VisioHelpers.GetTempDSLPath(associatedDocument), true);
            }
        }

        private void CreateWordControlPoint()
        {
            //String wordPath = VisioHelpers.GetWordPath(associatedDocument);
            //if (File.Exists(wordPath))
            //{
            //    File.Copy(wordPath, VisioHelpers.GetTempWordPath(associatedDocument), true);                
            //}
        }

        private void RemoveWordControlPoint()
        {
            //WordDocument.CloseWordDocument();
            //String tempWordPath = VisioHelpers.GetTempWordPath(associatedDocument);
            //if (File.Exists(tempWordPath))
            //{
            //    File.Delete(tempWordPath);
            //}
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