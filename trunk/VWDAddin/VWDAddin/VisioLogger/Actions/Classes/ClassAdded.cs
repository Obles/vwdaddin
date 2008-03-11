using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Visio;
using System.Text;
using VWDAddin.VisioWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    public class ClassAdded : ClassAction
    {
        public ClassAdded(VisioClass targetShape)
            : base(targetShape)
        {
        }

        override public void Apply(Logger Logger) 
        {
            if (Logger.DslDocument != null)
            {
                Logger.DslDocument.Dsl.CreateDomainClass(ClassShape.Name, ClassShape.Name);
            }
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.AddClass(ClassShape.Name, ClassShape.Attributes, ClassShape.GUID);
            }
        }
    }
}
