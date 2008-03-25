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
            if (Logger.DslDocument != null)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;

                switch (Connector.Type)
                {
                case Constants.Association:
                    {
                        DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
                        XmlClassData xcd;

                        String srcName = dr.Source.RolePlayer;
                        if (srcName != null)
                        {
                            DomainClass dc = Dsl.Classes[srcName] as DomainClass;
                            xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                            xcd.ElementData.Remove(xcd.GetRelationshipData(dr));
                        }
                        String dstName = dr.Target.RolePlayer;
                        if (dstName != null)
                        {
                            DomainClass dc = Dsl.Classes[dstName] as DomainClass;
                            xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                            xcd.Xml.RemoveAttribute("SerializeId");
                        }

                        xcd = Dsl.XmlSerializationBehavior.GetClassData(dr);
                        ConnectionBuilder cb = Dsl.GetConnectionBuilder(dr);

                        Dsl.Relationships.Remove(dr);
                        Dsl.XmlSerializationBehavior.ClassData.Remove(xcd);
                        Dsl.ConnectionBuilders.Remove(cb);
                        break;
                    }
                case Constants.Composition:
                    {
                        //TODO удаление композиции
                        break;
                    }
                case Constants.Generalization:
                    {
                        //TODO удаление наследования
                        break;
                    }
                default: throw new NotSupportedException();
                }
            }
        }
    }
}
