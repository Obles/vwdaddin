using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationDeleted : AssociationAction
    {
        public AssociationDeleted(VisioConnector targetShape)
            : base(targetShape)
        {            
        }

        override public void Apply(Logger Logger)
        {
            Dsl Dsl = Logger.DslDocument.Dsl;
            DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;

            if (dr.IsValid)
            {
                Dsl.Relationships.RemoveLinked(dr);
            }
        }
    }
}
