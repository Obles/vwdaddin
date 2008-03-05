using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    class DomainProperty : DslElement
    {
        public DomainProperty(XmlElement Node)
            : base(Node)
        {
        }

        public DomainProperty(DslDocument Doc)
            : base(Doc.CreateElement("DomainProperty"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
        }

        public DomainProperty(DslDocument Doc, String Type, String Name, String DisplayName)
            : base(Doc.CreateElement("DomainProperty"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Name", Name);
            this.Xml.SetAttribute("DisplayName", DisplayName);
            this.Type = Type;
        }

        public String Type
        {
            get { return Moniker.Get(this, "Type", "ExternalTypeMoniker"); }
            set { Moniker.Set(this, "Type", "ExternalTypeMoniker", value); }
        }

        public void Print(String t)
        {
            PrintNodeName(t + "Property");
            PrintValue("Type", Type);
            PrintValue("Name", Xml.GetAttribute("Name"));
            Console.WriteLine();
        }
    }
}
