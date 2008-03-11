using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationTargetNameChanged : AssociationAction
    {
        public AssociationTargetNameChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationEndName(Connector.GUID, Connector.TargetText, ConnectionTypes.EndConnected.ToString());
            }
        }
    }
}
