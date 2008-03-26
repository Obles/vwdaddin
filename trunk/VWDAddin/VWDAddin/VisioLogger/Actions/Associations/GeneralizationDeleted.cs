using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class GeneralizationDeleted : AssociationAction
    {
        public GeneralizationDeleted(VisioConnector targetShape)
            : base(targetShape)
        {            
        }

        override public void Apply(Logger Logger)
        {
            Dsl Dsl = Logger.DslDocument.Dsl;

            //TODO удаление наследования
        }
    }
}