using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class XmlRelationshipData : DslElement
    {
        public XmlRelationshipData(XmlElement Node)
            : base(Node)
        {
        }

        protected XmlRelationshipData(DslDocument Doc)
            : base(Doc.CreateElement("XmlRelationshipData"))
        {
        }

        public XmlRelationshipData(DomainRelationship Relationship)
            : base(Relationship.OwnerDocument.CreateElement("XmlRelationshipData"))
        {
            Update(Relationship);
        }

        public String DomainRelationshipMoniker
        {
            get { return (GetChildNode("DomainRelationshipMoniker") as XmlElement).GetAttribute("Name"); }
            set { (GetChildNode("DomainRelationshipMoniker") as XmlElement).SetAttribute("Name", value); }
        }

        public void Update(DomainRelationship Relationship)
        {
            String Name = Relationship.Target.RolePlayer;
            String subName = Dsl.SubName(Name);

            Xml.SetAttribute("RoleElementName", subName);

            DomainRelationshipMoniker = Relationship.Xml.GetAttribute("Name");
        }

        new public bool IsValid
        {
            get { return base.IsValid && Xml.Name == "XmlRelationshipData"; }
        }
    }
}