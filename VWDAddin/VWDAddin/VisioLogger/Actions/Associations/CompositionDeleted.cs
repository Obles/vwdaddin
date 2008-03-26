using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class CompositionDeleted : AssociationAction
    {
        public CompositionDeleted(VisioConnector targetShape)
            : base(targetShape)
        {            
        }

        override public void Apply(Logger Logger)
        {
            Dsl Dsl = Logger.DslDocument.Dsl;

            DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
            XmlClassData xcd;

            String Name = dr.Source.RolePlayer;
            if (Name != null)
            {
                DomainClass dc = Dsl.Classes[Name] as DomainClass;
                xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                xcd.ElementData.Remove(xcd.GetRelationshipData(dr));

                Name = dr.Target.RolePlayer;
                if (Name != null)
                {
                    dc.ElementMergeDirectives.Remove(
                        dc.GetElementMergeDirective(Name)
                    );
                }
            }

            xcd = Dsl.XmlSerializationBehavior.GetClassData(dr);

            Dsl.Relationships.Remove(dr);
            Dsl.XmlSerializationBehavior.ClassData.Remove(xcd);
        }
    }
}