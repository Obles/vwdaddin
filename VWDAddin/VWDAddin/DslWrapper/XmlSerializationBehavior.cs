using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class XmlSerializationBehavior : DslElement
    {
        public XmlSerializationBehavior(XmlElement Node)
            : base(Node)
        {
        }

        protected XmlSerializationBehavior(DslDocument Doc)
            : base(Doc.CreateElement("XmlSerializationBehavior"))
        {
        }

        public DslElementList ClassData
        {
            get { return new DslElementList(typeof(XmlClassData), GetChildNode("ClassData")); }
        }

        public XmlClassData GetClassData(DomainClass Class)
        {
            String Name = Class.Xml.GetAttribute("Name");
            foreach (XmlClassData xcd in ClassData)
            {
                if (xcd.DomainClassMoniker == Name) return xcd;
            }
            return null;
        }

        public XmlClassData Find(String Name)
        {
            foreach (XmlClassData xcd in ClassData)
            {
                XmlElement xe = xcd.SelectSingleNode("p:DomainRelationshipMoniker") as XmlElement;
                if(xe == null) xe = xcd.SelectSingleNode("p:DomainClassMoniker") as XmlElement;

                if (xe != null && xe.GetAttribute("Name") == Name) return xcd;
            }
            return null;
        }
    }
}
