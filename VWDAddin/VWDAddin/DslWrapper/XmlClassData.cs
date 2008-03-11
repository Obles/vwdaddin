using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class XmlClassData : DslElement
    {
        public XmlClassData(XmlElement Node)
            : base(Node)
        {
        }

        protected XmlClassData(DslDocument Doc)
            : base(Doc.CreateElement("XmlClassData"))
        {
        }

        public XmlClassData(DomainClass Class)
            : base(Class.OwnerDocument.CreateElement("XmlClassData"))
        {
            Update(Class);
        }

        public DslElementList ElementData
        {
            get { return new DslElementList(typeof(XmlPropertyData), GetChildNode("ElementData")); }
        }

        public String DomainClassMoniker
        {
            get { return (GetChildNode("DomainClassMoniker") as XmlElement).GetAttribute("Name"); }
            set { (GetChildNode("DomainClassMoniker") as XmlElement).SetAttribute("Name", value); }
        }

        public void Update(DomainClass Class)
        {
            String Name = Class.Xml.GetAttribute("Name");
            String subName = Name.Substring(0, 1).ToLower() + Name.Substring(1);
            Xml.SetAttribute("TypeName", Name);
            Xml.SetAttribute("MonikerAttributeName", "");
            Xml.SetAttribute("MonikerElementName", subName + "Moniker");
            Xml.SetAttribute("ElementName", subName);
            Xml.SetAttribute("MonikerTypeName", Name + "Moniker");

            DomainClassMoniker = Name;          
        }

        public XmlPropertyData GetPropertyData(DomainProperty Property)
        {
            String Name = DomainClassMoniker + "/" + Property.Xml.GetAttribute("Name");
            foreach (XmlPropertyData xpd in ElementData)
            {
                if (xpd.DomainPropertyMoniker == Name) return xpd;
            }
            return null;
        }
    }
}
