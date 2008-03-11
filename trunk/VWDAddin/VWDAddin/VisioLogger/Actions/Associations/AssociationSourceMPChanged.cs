using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationSourceMPChanged : AssociationAction
    {
        public AssociationSourceMPChanged(VisioConnector targetShape)
            : base(targetShape)
        {
        }
    }
}
