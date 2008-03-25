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

        public DslElementList LinkCreationPaths
        {
            get { return new DslElementList(typeof(DomainPath), GetChildNode("LinkCreationPaths")); }
        }

        public void ChangePaths(String from, String to)
        {
            String prefix = from + ".";
            foreach (DomainPath path in LinkCreationPaths)
            {
                if (path.Value.StartsWith(prefix))
                {
                    path.Value = to + "." + path.Value.Substring(prefix.Length);
                }
            }
        }
    }
}
