using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationTargetMPChanged : AssociationAction
    {
        public AssociationTargetMPChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }
    }
}
