using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;

namespace VWDAddin.VisioLogger.Actions
{
    class AssociationNameChanged : AssociationAction
    {
        public AssociationNameChanged(Shape targetShape)
            : base(targetShape)
        {
            // ToDo: add params here
        }
    }
}
