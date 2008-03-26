using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    class DomainPath : DslElement
    {
        public DomainPath(XmlElement Node)
            : base(Node)
        {
        }

        protected DomainPath(DslDocument Doc)
            : base(Doc.CreateElement("DomainPath"))
        {
        }

        public DomainPath(DslDocument Doc, String path)
            : base(Doc.CreateElement("DomainPath"))
        {
            Value = path;
        }

        public String Value
        {
            get { return Xml.InnerText; }
            set { Xml.InnerText = value; }
        }
    }
}
