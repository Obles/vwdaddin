using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Visio;
using System.Text;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

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
                Dsl Dsl = Logger.DslDocument.Dsl;
                DomainClass dc = Dsl.CreateDomainClass(ClassShape.Name, ClassShape.Name);
                dc.GUID = ClassShape.GUID;
                Dsl.XmlSerializationBehavior.ClassData.Append(new XmlClassData(dc));                
            }
        }
    }
}
