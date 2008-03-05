using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    class XmlPropertyData : DslElement
    {
        public XmlPropertyData(XmlElement Node)
            : base(Node)
        {
        }

        protected XmlPropertyData(DslDocument Doc)
            : base(Doc.CreateElement("XmlPropertyData"))
        {
        }

    }
}