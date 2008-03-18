using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class ConnectionBuilder : DslElement
    {
        public ConnectionBuilder(XmlElement Node)
            : base(Node)
        {
        }

        public ConnectionBuilder(DslDocument Doc)
            : base(Doc.CreateElement("ConnectionBuilder"))
        {
        }

        public ConnectionBuilder(DomainRelationship Relationship)
            : base(Relationship.OwnerDocument.CreateElement("ConnectionBuilder"))
        {
            Update(Relationship);
        }

        public String LinkConnectDirective
        {
            get { return Moniker.Get(this, "LinkConnectDirective", "DomainRelationshipMoniker"); }
            set { Moniker.Set(this, "LinkConnectDirective", "DomainRelationshipMoniker", value); }
        }

        public DslElementList SourceDirectives
        {
            get { return new DslElementList(typeof(RolePlayerConnectDirective), GetChildNode("LinkConnectDirective/SourceDirectives")); }
        }

        public DslElementList TargetDirectives
        {
            get { return new DslElementList(typeof(RolePlayerConnectDirective), GetChildNode("LinkConnectDirective/TargetDirectives")); }
        }

        public void Update(DomainRelationship Relationship)
        {
            String Name = Relationship.Xml.GetAttribute("Name");
            Xml.SetAttribute("Name", Name + "Builder");
            LinkConnectDirective = Name;
        }
    }
}
