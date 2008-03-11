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
            ClassShape = targetShape.ToStaticClass();
        }

        #region Members
        private StaticClass _classShape;
        public StaticClass ClassShape
        {
            get { return _classShape; }
            set { _classShape = value; }
        }
        #endregion
    }
}
