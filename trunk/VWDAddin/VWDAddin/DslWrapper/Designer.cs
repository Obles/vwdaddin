using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class Designer : DslElement
    {
        public Designer(XmlElement Node)
            : base(Node)
        {
        }

        public Designer(DslDocument Doc)
            : base(Doc.CreateElement("Designer"))
        {
            this.Xml.SetAttribute("EditorGuid", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("FileExtension", "mydsl3");
        }

        public String RootClass
        {
            get { return Moniker.Get(this, "RootClass", "DomainClassMoniker"); }
            set { Moniker.Set(this, "RootClass", "DomainClassMoniker", value); }
        }
    }
}
