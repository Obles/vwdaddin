using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

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
    }
}
