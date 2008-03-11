using System;
using System.Collections.Generic;
using System.Text;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions.Associations
{
    class AssociationAdded : AssociationAction
    {
        public AssociationAdded(VisioConnector targetShape)
            : base(targetShape)
        {            
        }
    }
}
