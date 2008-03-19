using System;
using System.Collections.Generic;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;
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
            if (Logger.DslDocument != null && Connector.Type != Constants.Generalization)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;
                DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
                dr.Source.Xml.SetAttribute("Name", Connector.SourceText);
                dr.Source.Xml.SetAttribute("DisplayName", Connector.SourceText);
            }
            if (Logger.WordDocument.IsAssociated)
            {
                //Logger.WordDocument.ChangeAssociationEndName(Connector.GUID, Connector.SourceText, ConnectionTypes.BeginConnected.ToString());
            }
        }
    }
}
