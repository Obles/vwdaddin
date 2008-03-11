using System;
using System.Collections.Generic;
using System.Text;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationConnected : AssociationAction
    {
        public AssociationConnected(VisioConnector targetShape, ConnectionTypes connectType)
            : base(targetShape)
        {
            ConnectType = connectType;
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                if (ConnectType == ConnectionTypes.BeginConnected)
                {
                    Logger.WordDocument.AddAssociation(Connector.Source.GUID, Connector.GUID, Connector.Name, Connector.SourceText, Connector.SourceMultiplicity, "association", ConnectType.ToString());
                }
                else
                {
                    Logger.WordDocument.AddAssociation(Connector.Target.GUID, Connector.GUID, Connector.Name, Connector.TargetText, Connector.TargetMultiplicity, "association", ConnectType.ToString());
                }
            }
        }

        private ConnectionTypes _connectType;
        public ConnectionTypes ConnectType
        {
            get { return _connectType; }
            set { _connectType = value; }
        }
    }
}
