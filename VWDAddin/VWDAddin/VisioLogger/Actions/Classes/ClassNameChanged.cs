using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

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
            if (Logger.DslDocument != null)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;
                DomainClass dc = Dsl.Classes.Find(ClassShape.GUID) as DomainClass;
                XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);

                dc.Xml.SetAttribute("Name", ClassShape.Name);
                xcd.Update(dc);
            }
            if (Logger.WordDocument.IsAssociated)
            {
                //Logger.WordDocument.ChangeClassName(ClassShape.GUID, ClassShape.Name);
            }
        }
    }
}
