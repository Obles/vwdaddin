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
                        XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dr);
                        ConnectionBuilder cb = Dsl.GetConnectionBuilder(dr);

                        Dsl.Relationships.Remove(dr);
                        Dsl.XmlSerializationBehavior.ClassData.Remove(xcd);
                        Dsl.ConnectionBuilders.Remove(cb);
                        break;
                    }
                case Constants.Composition:
                    {
                        //TODO �������� ����������
                        break;
                    }
                case Constants.Generalization:
                    {
                        //TODO �������� ������������
                        break;
                    }
                default: throw new NotSupportedException();
                }
            }

            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.DeleteAssociation(Connector.GUID);
            }
        }
    }
}
