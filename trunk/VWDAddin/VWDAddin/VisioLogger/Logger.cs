using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.IO;

namespace VWDAddin.VisioLogger
{
    public class Logger
    {
        private List<Action> actionList = new List<Action>();
        private int currentAction = -1;

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

        public void Add(Action Action)
        {
            currentAction++;
            actionList.RemoveRange(currentAction, actionList.Count - currentAction);
            actionList.Add(Action);
            Document.Application.AddUndoUnit(new UndoUnit(this));
        }

        public Action CurrentAction
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
                // actionList[i].Apply(Document);
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
            String TempDslPath = VisioHelpers.GetTempDSLPath(associatedDocument);
            if (File.Exists(TempDslPath))
            {
                File.Delete(TempDslPath);
            }
        }
    }
}
