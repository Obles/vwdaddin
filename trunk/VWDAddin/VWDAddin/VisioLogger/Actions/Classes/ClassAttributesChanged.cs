using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    class ClassAttributesChanged : ClassAction
    {
        public ClassAttributesChanged(VisioClass targetShape)
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

                // Приводим в порядок атрибуты
                String attrstr = "\n";
                String[] attrs = ClassShape.Attributes.Split('\n');
                for(int i = 0; i < attrs.Length; i++)
                {
                    attrs[i] = attrs[i].Trim();
                    attrstr += attrs[i] + "\n";
                }
                
                // Добавляем новые свойства
                foreach (String attr in attrs)
                {
                    if (attr.Length == 0) continue;
                    if (dc.Properties[attr].Xml == null)
                    {
                        DomainProperty dp = dc.CreateProperty("/System/String", attr, attr);
                        xcd.ElementData.Append(new XmlPropertyData(dp));
                    }
                }

                // Удаляем ненужные свойства
                for (int i = 0; i < dc.Properties.Count; i++)
                {
                    DomainProperty prop = dc.Properties[i] as DomainProperty;
                    if (!attrstr.Contains("\n" + prop.Xml.GetAttribute("Name") + "\n"))
                    {
                        xcd.ElementData.Remove(xcd.GetPropertyData(prop));
                        dc.Properties.Remove(prop);
                        i--;
                    }
                }
            }
        }
    }    
}
