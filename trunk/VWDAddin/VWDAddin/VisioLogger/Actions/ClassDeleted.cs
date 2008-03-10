using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;

namespace VWDAddin.VisioLogger.Actions
{
    class ClassDeleted : ClassAction
    {
        public ClassDeleted(Shape targetShape)
            :base(targetShape)
        {            
        }

        override public void Apply(Logger Logger)
        {
            Logger.WordDocument.DeleteClass(GUID);
        }

    }
}
