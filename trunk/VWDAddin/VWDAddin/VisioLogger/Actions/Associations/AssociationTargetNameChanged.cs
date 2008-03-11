using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationTargetNameChanged : AssociationAction
    {
        public AssociationTargetNameChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }
    }
}
