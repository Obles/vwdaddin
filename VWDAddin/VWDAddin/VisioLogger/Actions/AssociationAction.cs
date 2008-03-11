using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using ActionTypes = VWDAddin.Constants.ActionTypes;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    public class AssociationAction : BaseAction
    {
        public AssociationAction(VisioConnector targetShape)           
        {
            Connector = targetShape;
        }

        #region Members
        private VisioConnector _connector;
        public VisioConnector Connector
        {
            get { return _connector; }
            set { _connector = value; }
        }
        #endregion
    }
}
