using System;
using System.Collections.Generic;
using VWDAddin.VisioWrapper;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;
using System.Text;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationSourceNameChanged : AssociationAction
    {
        public AssociationSourceNameChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationEndName(Connector.GUID, Connector.SourceText, ConnectionTypes.BeginConnected.ToString());
            }
        }
    }
}
