using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    class ClassNameChanged : ClassAction
    {
        public ClassNameChanged(VisioClass targetShape)
            : base(targetShape)
        {         
        }

        override public void Apply(Logger Logger)
        {
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.ChangeClassName(ClassShape.GUID, ClassShape.Name);
            }
        }
    }
}
