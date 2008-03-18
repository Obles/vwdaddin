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
            get { return new DslElementList(typeof(XmlPropertyData), GetChildNode("ElementData")); }
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
            Xml.SetAttribute("MonikerAttributeName", "");
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
            Xml.SetAttribute("MonikerAttributeName", "");
            Xml.SetAttribute("MonikerElementName", subName + "Moniker");
            Xml.SetAttribute("ElementName", subName);
            Xml.SetAttribute("MonikerTypeName", Name + "Moniker");

            DomainRelationshipMoniker = Name;
        }

        public XmlPropertyData GetPropertyData(DomainProperty Property)
        {
            //TODO этот метод работает неверно, тк в ElementData могут находиться не только XmlPropertyData
            String Name = DomainClassMoniker + "/" + Property.Xml.GetAttribute("Name");
            foreach (XmlPropertyData xpd in ElementData)
            {
                if (xpd.DomainPropertyMoniker == Name) return xpd;
            }
            return null;
        }
        public XmlRelationshipData GetRelationshipData(DomainRelationship Relationship)
        {
            //TODO реализовать метод GetRelationshipData
            throw new NotImplementedException();
        }
    }
}
