using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    class DomainRelationship : DslElement
    {
        public DomainRelationship(XmlElement Node)
            : base(Node)
        {
        }

        public DomainRelationship(DslDocument Doc)
            : base(Doc.CreateElement("DomainRelationship"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
        }

        public DomainRelationship(DslDocument Doc, String Name, String DisplayName, Boolean IsEmbedding)
            : base(Doc.CreateElement("DomainRelationship"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Name", Name);
            this.Xml.SetAttribute("DisplayName", DisplayName);
            this.Xml.SetAttribute("Namespace", this.OwnerDocument.Dsl.Xml.GetAttribute("Namespace"));
            this.Xml.SetAttribute("IsEmbedding", IsEmbedding.ToString());
        }

        public DslElementList Properties
        {
            get { return new DslElementList(typeof(DomainProperty), GetChildNode("Properties")); }
        }

        public DomainRole Source
        {
            get
            {
                DslElementList list = new DslElementList(typeof(DomainRole), GetChildNode("Source"));
                return list[0] as DomainRole;
            }
            set
            {
                XmlNode node = GetChildNode("Source");
                node.RemoveAll();
                (new DslElementList(typeof(DomainRole), node)).Append(value);
            }
        }

        public DomainRole Target
        {
            get
            {
                DslElementList list = new DslElementList(typeof(DomainRole), GetChildNode("Target"));
                return list[0] as DomainRole;
            }
            set
            {
                XmlNode node = GetChildNode("Target");
                node.RemoveAll();
                (new DslElementList(typeof(DomainRole), node)).Append(value);
            }
        }

        public DomainProperty CreateProperty(String Type, String Name, String DisplayName)
        {
            return Properties.Append(new DomainProperty(OwnerDocument, Type, Name, DisplayName)) as DomainProperty;
        }

        public void Print(String t)
        {
            PrintNodeName(t + "Relationship");
            PrintValue("Name", Xml.GetAttribute("Name"));
            PrintValue("IsEmbedding", Xml.GetAttribute("IsEmbedding"));
            Console.WriteLine();
            PrintValue(t + "Source", "\n");
            Source.Print(t + t);
            PrintValue(t + "Target", "\n");
            Target.Print(t + t);
        }
    }
}
