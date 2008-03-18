using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;
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
            if (Logger.DslDocument != null && Connector.Type != Constants.Generalization)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;
                DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
                dr.Target.Xml.SetAttribute("Name", Connector.TargetText);
                dr.Target.Xml.SetAttribute("DisplayName", Connector.TargetText);
            }
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationEndName(Connector.GUID, Connector.TargetText, ConnectionTypes.EndConnected.ToString());
            }
        }
    }
}
