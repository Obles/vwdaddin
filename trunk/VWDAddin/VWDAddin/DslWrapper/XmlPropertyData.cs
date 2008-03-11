using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class XmlPropertyData : DslElement
    {
        public XmlPropertyData(XmlElement Node)
            : base(Node)
        {
        }

        protected XmlPropertyData(DslDocument Doc)
            : base(Doc.CreateElement("XmlPropertyData"))
        {
        }

        public XmlPropertyData(DomainProperty Property)
            : base(Property.OwnerDocument.CreateElement("XmlPropertyData"))
        {
            Update(Property);
        }

        public String DomainPropertyMoniker
        {
            get { return (GetChildNode("DomainPropertyMoniker") as XmlElement).GetAttribute("Name"); }
            set { (GetChildNode("DomainPropertyMoniker") as XmlElement).SetAttribute("Name", value); }
        }

        public void Update(DomainProperty Property)
        {
            String Name = Property.Xml.GetAttribute("Name");
            String subName = Name.Substring(0, 1).ToLower() + Name.Substring(1);
            String Parent = Property.Parent.Xml.GetAttribute("Name");
            Xml.SetAttribute("XmlName", subName);

            DomainPropertyMoniker = Parent + "/" + Name;
          }
    }
}