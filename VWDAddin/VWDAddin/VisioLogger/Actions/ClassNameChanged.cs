using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;

namespace VWDAddin.VisioLogger.Actions
{
    class ClassNameChanged : ClassAction
    {
        public ClassNameChanged(Shape targetShape)
            : base(targetShape)
        {         
        }
    }
}
