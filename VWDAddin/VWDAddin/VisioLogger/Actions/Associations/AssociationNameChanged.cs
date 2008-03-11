using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationNameChanged : AssociationAction
    {
        public AssociationNameChanged(VisioConnector targetShape)
            : base(targetShape)
        {           
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationName(Connector.GUID, Connector.Name);
            }
        }

    }
}
