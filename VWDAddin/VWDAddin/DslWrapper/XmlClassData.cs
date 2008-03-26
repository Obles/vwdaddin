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

        public XmlClassData(DomainRelationship Relationship)
            : base(Relationship.OwnerDocument.CreateElement("XmlClassData"))
        {
            Update(Relationship);
        }

        public DslElementList ElementData
        {
            get { return new DslElementList(typeof(DslElement), GetChildNode("ElementData")); }
        }

        public String DomainClassMoniker
        {
            get { return (GetChildNode("DomainClassMoniker") as XmlElement).GetAttribute("Name"); }
            set { (GetChildNode("DomainClassMoniker") as XmlElement).SetAttribute("Name", value); }
        }

        public String DomainRelationshipMoniker
        {
            get { return (GetChildNode("DomainRelationshipMoniker") as XmlElement).GetAttribute("Name"); }
            set { (GetChildNode("DomainRelationshipMoniker") as XmlElement).SetAttribute("Name", value); }
        }

        public void Update(DomainClass Class)
        {
            String Name = Class.Xml.GetAttribute("Name");
            String subName = Dsl.SubName(Name);
            Xml.SetAttribute("TypeName", Name);
            Xml.SetAttribute("MonikerAttributeName", Xml.GetAttribute("MonikerAttributeName"));
            Xml.SetAttribute("MonikerElementName", subName + "Moniker");
            Xml.SetAttribute("ElementName", subName);
            Xml.SetAttribute("MonikerTypeName", Name + "Moniker");

            DomainClassMoniker = Name;          
        }

        public void Update(DomainRelationship Relationship)
        {
            String Name = Relationship.Xml.GetAttribute("Name");
            String subName = Dsl.SubName(Name);
            Xml.SetAttribute("TypeName", Name);
            Xml.SetAttribute("MonikerAttributeName", Xml.GetAttribute("MonikerAttributeName"));
            Xml.SetAttribute("MonikerElementName", subName + "Moniker");
            Xml.SetAttribute("ElementName", subName);
            Xml.SetAttribute("MonikerTypeName", Name + "Moniker");

            DomainRelationshipMoniker = Name;
        }

        public XmlPropertyData GetPropertyData(DomainProperty Property)
        {
            String Name = DomainClassMoniker + "/" + Property.Xml.GetAttribute("Name");
            foreach (DslElement element in ElementData)
            {
                XmlPropertyData xpd = new XmlPropertyData(element.Xml);
                if (xpd.IsValid &&
                    xpd.DomainPropertyMoniker == Name) return xpd;
            }
            return null;
        }
        public XmlRelationshipData GetRelationshipData(DomainRelationship Relationship)
        {
            String Name = Relationship.Xml.GetAttribute("Name");
            foreach (DslElement element in ElementData)
            {
                XmlRelationshipData xrd = new XmlRelationshipData(element.Xml);
                if (xrd.IsValid &&
                    xrd.DomainRelationshipMoniker == Name) return xrd;
            }
            return null;
        }
    }
}
