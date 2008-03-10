using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class RolePlayerConnectDirective : DslElement
    {
        public RolePlayerConnectDirective(XmlElement Node)
            : base(Node)
        {
        }

        public RolePlayerConnectDirective(DslDocument Doc)
            : base(Doc.CreateElement("RolePlayerConnectDirective"))
        {
        }

        public String AcceptingClass
        {
            get { return Moniker.Get(this, "AcceptingClass", "DomainClassMoniker"); }
            set { Moniker.Set(this, "AcceptingClass", "DomainClassMoniker", value); }
        }
    }
}
