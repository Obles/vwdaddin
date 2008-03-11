using System;
using System.Collections.Generic;
using VWDAddin.VisioWrapper;
using System.Text;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationSourceNameChanged : AssociationAction
    {
        public AssociationSourceNameChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }
    }
}
