using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    public class AssociationAction : BaseAction
    {
        public AssociationAction(VisioConnector targetShape)           
        {
            Connector = targetShape.ToStaticConnector();
        }

        #region Members
        private StaticConnector _connector;
        public StaticConnector Connector
        {
            get { return _connector; }
            set { _connector = value; }
        }
        #endregion
    }
}
