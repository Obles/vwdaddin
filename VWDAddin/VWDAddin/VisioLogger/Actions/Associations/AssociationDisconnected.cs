using System;
using System.Collections.Generic;
using System.Text;
using ConnectionTypes = VWDAddin.Constants.ConnectionTypes;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationDisconnected : AssociationAction
    {
        public AssociationDisconnected(VisioConnector targetShape, ConnectionTypes connectType)
            : base(targetShape)
        {
            ConnectType = connectType;
        }

        override public void Apply(Logger Logger)
        {
            return;
            if (Logger.DslDocument != null)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;

                switch (Connector.Type)
                {
                    case Constants.Association:
                        {
                            DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
                            ConnectionBuilder cb = Dsl.GetConnectionBuilder(dr);

                            if (ConnectType == ConnectionTypes.Begin)
                            {
                                DomainClass dc = Dsl.Classes[dr.Source.RolePlayer] as DomainClass;
                                dr.Source.RolePlayer = null;

                                cb.SourceDirectives.Remove(
                                    cb.GetSourceConnectDirective(dc)
                                );

                                XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                                xcd.ElementData.Remove(xcd.GetRelationshipData(dr));
                            }
                            else
                            {
                                DomainClass dc = Dsl.Classes[dr.Target.RolePlayer] as DomainClass;
                                dr.Target.RolePlayer = null;

                                XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);
                                xcd.Xml.RemoveAttribute("SerializeId");

                                cb.TargetDirectives.Remove(
                                    cb.GetTargetConnectDirective(dc)
                                );

                                if (dr.Source.RolePlayer != null)
                                {
                                    Dsl.XmlSerializationBehavior.GetClassData(
                                        Dsl.Classes[dr.Source.RolePlayer] as DomainClass
                                    ).GetRelationshipData(dr).Update(dr);
                                }
                            }
                            FixRolePropertyNames(dr);
                            break;
                        }
                    case Constants.Composition:
                        {
                            //TODO отсоединение композиции
                            break;
                        }
                    case Constants.Generalization:
                        {
                            //TODO отсоединение наследования
                            break;
                        }
                    default: throw new NotSupportedException();
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
