using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    public class ClassAction : BaseAction
    {
        public ClassAction(VisioClass targetShape)            
        {
            ClassShape = targetShape;
        }

        #region Members
        private VisioClass _classShape;
        public VisioClass ClassShape
        {
            get { return _classShape; }
            set { _classShape = value; }
        }
        #endregion
    }
}
