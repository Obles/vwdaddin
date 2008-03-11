using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    class ClassDeleted : ClassAction
    {
        public ClassDeleted(VisioClass targetShape)
            :base(targetShape)
        {            
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.DeleteClass(ClassShape.GUID);
            }
        }

    }
}
