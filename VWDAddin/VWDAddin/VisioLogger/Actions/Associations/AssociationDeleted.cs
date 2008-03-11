using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationDeleted : AssociationAction
    {
        public AssociationDeleted(VisioConnector targetShape)
            : base(targetShape)
        {            
        }
    }
}
