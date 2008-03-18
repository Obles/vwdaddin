using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationAdded : AssociationAction
    {
        public AssociationAdded(VisioConnector targetShape)
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
                        DomainRelationship dr = new DomainRelationship(Logger.DslDocument);
                        dr.GUID = Connector.GUID;
                        dr.Xml.SetAttribute("Name", Connector.Name);
                        dr.Xml.SetAttribute("DisplayName", Connector.Name);

                        dr.Source = new DomainRole(Logger.DslDocument);
                        dr.Target = new DomainRole(Logger.DslDocument);

                        Dsl.Relationships.Append(dr);
                        Dsl.XmlSerializationBehavior.ClassData.Append(new XmlClassData(dr));
                        Dsl.ConnectionBuilders.Append(new ConnectionBuilder(dr));
                        break;
                    }
                case Constants.Composition:
                    {
                        //TODO создание композиции
                        break;
                    }
                case Constants.Generalization:
                    {
                        //TODO создание наследования
                        break;
                    }
                default: throw new NotSupportedException();
                }
            }
        }
    }
}
