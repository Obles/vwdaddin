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
            if (Logger.DslDocument != null)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;

                switch (Connector.Type)
                {
                    case Constants.Association:
                        {
                            //TODO отсоединение ассоциации
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
