using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;
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
            if (Logger.DslDocument != null && Connector.Type != Constants.Generalization)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;
                DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
                dr.Source.Multiplicity = MultiplicityHelper.Compatible(Connector.SourceMultiplicity);
            }
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationMP(Connector.GUID, Connector.SourceMultiplicity, ConnectionTypes.BeginConnected.ToString());
            }
        }
    }
}
