using System;
using System.Collections.Generic;
using System.Text;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    class AssociationConnected: AssociationAction
    {
        public AssociationConnected(VisioConnector targetShape, ConnectionTypes connectType)
            : base(targetShape)
        {
            ConnectType = connectType;
        }
        
        private ConnectionTypes _connectType;
        public ConnectionTypes ConnectType
        {
            get { return _connectType; }
            set { _connectType = value; }
        }
    }
}
