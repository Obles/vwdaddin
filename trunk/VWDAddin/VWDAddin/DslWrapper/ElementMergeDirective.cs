using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class ElementMergeDirective : DslElement
    {
        public ElementMergeDirective(XmlElement Node)
            : base(Node)
        {
        }

        protected ElementMergeDirective(DslDocument Doc)
            : base(Doc.CreateElement("ElementMergeDirective"))
        {
        }

        public String Index
        {
            get { return Moniker.Get(this, "Index", "DomainClassMoniker"); }
            set { Moniker.Set(this, "Index", "DomainClassMoniker", value); }
        }

    }
}
