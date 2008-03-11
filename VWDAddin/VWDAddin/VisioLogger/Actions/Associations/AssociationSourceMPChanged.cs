using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationSourceMPChanged : AssociationAction
    {
        public AssociationSourceMPChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationMP(Connector.GUID, Connector.SourceMultiplicity, ConnectionTypes.BeginConnected.ToString());
            }
        }
    }
}
