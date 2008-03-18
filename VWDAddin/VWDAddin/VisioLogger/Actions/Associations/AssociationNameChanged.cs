using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

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
            if (Logger.DslDocument != null)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;

                switch (Connector.Type)
                {
                case Constants.Association:
                    {
                        DomainRelationship dr = Dsl.Relationships.Find(Connector.GUID) as DomainRelationship;
                        XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dr);
                        ConnectionBuilder cb = Dsl.GetConnectionBuilder(dr);

                        dr.Xml.SetAttribute("Name", Connector.Name);
                        dr.Xml.SetAttribute("DisplayName", Connector.Name);

                        xcd.Update(dr);
                        cb.Update(dr);

                        //TODO еще надо править моникеры Source, Target классов
                        break;
                    }
                case Constants.Composition:
                    {
                        //TODO переименовывание композиции
                        break;
                    }
                case Constants.Generalization:
                    {
                        //TODO переименовывание наследования
                        break;
                    }
                default: throw new NotSupportedException();
                }
            }
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeAssociationName(Connector.GUID, Connector.Name);
            }
        }

    }
}
