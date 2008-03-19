using System;
using System.Collections.Generic;
using System.Text;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationConnected : AssociationAction
    {
        public AssociationConnected(VisioConnector targetShape, ConnectionTypes connectType)
            : base(targetShape)
        {
            ConnectType = connectType;
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
                        ConnectionBuilder cb = Dsl.GetConnectionBuilder(dr);

                        if (ConnectType == ConnectionTypes.BeginConnected)
                        {
                            DomainClass dc = Dsl.Classes.Find(Connector.Source.GUID) as DomainClass;
                            dr.Source.RolePlayer = dc.Xml.GetAttribute("Name");
                            cb.SourceDirectives.Append(new RolePlayerConnectDirective(dc));
                            
                            XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                            xcd.ElementData.Append(new XmlRelationshipData(dr));
                        }
                        else
                        {
                            DomainClass dc = Dsl.Classes.Find(Connector.Target.GUID) as DomainClass;
                            dr.Target.RolePlayer = dc.Xml.GetAttribute("Name");
                            cb.TargetDirectives.Append(new RolePlayerConnectDirective(dc));

                            XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                            xcd.Xml.SetAttribute("SerializeId", "true");

                            if (Connector.Source != null)
                            {
                                Dsl.XmlSerializationBehavior.GetClassData(
                                    Dsl.Classes.Find(Connector.Source.GUID) as DomainClass
                                ).GetRelationshipData(dr).Update(dr);
                            }
                        }
                        FixRolePropertyNames(dr);
                        break;
                    }
                case Constants.Composition:
                    {
                        //TODO перевешивание композиции
                        break;
                    }
                case Constants.Generalization:
                    {
                        //TODO перевешивание наследования
                        break;
                    }
                default: throw new NotSupportedException();
                }
            }
            if (Logger.WordDocument.IsAssociated)
            {
                if (ConnectType == ConnectionTypes.BeginConnected)
                {
                    Logger.WordDocument.AddAssociation(Connector.Source.GUID, Connector.GUID, Connector.Name, Connector.SourceText, Connector.SourceMultiplicity, "association", ConnectType.ToString());
                }
                else
                {
                    Logger.WordDocument.AddAssociation(Connector.Target.GUID, Connector.GUID, Connector.Name, Connector.TargetText, Connector.TargetMultiplicity, "association", ConnectType.ToString());
                }
            }
        }

        private ConnectionTypes _connectType;
        public ConnectionTypes ConnectType
        {
            get { return _connectType; }
            set { _connectType = value; }
        }
    }
}
