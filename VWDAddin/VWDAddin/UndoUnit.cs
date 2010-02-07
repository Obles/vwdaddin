using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;

namespace VWDAddin
{
    class UndoUnit : IVBUndoUnit
    {
        private bool stateDo; // false - undo, true - redo
        private Logger Logger;

        public UndoUnit(Logger Logger)
        {
            stateDo = false;
            this.Logger = Logger;
        }

        /// <summary>Approximate memory size in bytes of the undo unit.</summary>
        public int UnitSize
        {
            get { return 6; }
        }

        public void Do(IVBUndoManager undoManager)
        {
            //try
            {
                if (stateDo)
                {
                    System.Diagnostics.Debug.WriteLine("Redoing the action.");
                    Logger.Redo();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Undoing the action.");
                    Logger.Undo();
                }

                // Toggle the state flag.
                stateDo = !stateDo;

                // If this method receives a valid undoManager as a
                // parameter, re-add this instance to the Undo Manager.
                if (undoManager != null)
                {
                    undoManager.Add(this);
                }
            }
            //catch (Exception err)
            //{
            //    System.Diagnostics.Debug.WriteLine(err.Message);
            //}
        }

        #region Unused methods
        /// <summary>This method gets called when the next unit in the same
        /// scope gets added to the undo stack.  This method is an
        /// implementation of IUndoUnit.OnNextAdd method.</summary>
        public void OnNextAdd()
        {
        }

        /// <summary>Description of the undo unit. This property is an
        /// implementation of IVBUndoUnit.Description property.</summary>
        public string Description
        {
            get { return "VWDAddInUndoUnit"; }
        }

        /// <summary>ClassID of the undo unit.  This property is an
        /// implementation of IVBUndoUnit.UnitTypeCLSID property.</summary>
        public string UnitTypeCLSID
        {
            get { return String.Empty; }
        }

        /// <summary>The type of the undo unit.  This property is an
        /// implementation of IVBUndoUnit.UnitTypeLong property.</summary>
        public int UnitTypeLong
        {
            get { return 0; }
        }
        #endregion
    }
}
