using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Visio;
using System.Text;

namespace VWDAddin.VisioLogger.Actions
{
    public class ClassAdded : ClassAction
    {
        public ClassAdded(Shape targetShape, WordDocument wordDocument)
            :base(targetShape)
        {
            // ToDo
        }

        new public void Apply(Document document) 
        { 
        }
    }
}
