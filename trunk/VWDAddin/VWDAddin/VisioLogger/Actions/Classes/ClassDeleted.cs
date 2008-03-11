using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

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
            if (Logger.DslDocument != null)
            {
                Dsl Dsl = Logger.DslDocument.Dsl;
                DomainClass dc = Dsl.Classes.Find(ClassShape.GUID) as DomainClass;
                XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(dc);

                Dsl.Classes.Remove(dc);
                Dsl.XmlSerializationBehavior.ClassData.Remove(xcd);
            }
            if (Logger.WordDocument.IsAssociated)
            {
                Logger.WordDocument.DeleteClass(ClassShape.GUID);
            }
        }

    }
}
