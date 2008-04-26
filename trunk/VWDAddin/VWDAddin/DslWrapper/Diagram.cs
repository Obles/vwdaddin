using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class Diagram : DslElement
    {
        public Diagram(XmlElement Node)
            : base(Node)
        {
        }

        public Diagram(DslDocument Doc)
            : base(Doc.CreateElement("Diagram"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Namespace", this.OwnerDocument.Dsl.Xml.GetAttribute("Namespace"));
        }

        public String Class
        {
            get { return Moniker.Get(this, "Class", "DomainClassMoniker"); }
            set { Moniker.Set(this, "Class", "DomainClassMoniker", value); }
        }
    }
}
