using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class XmlClassData : DslElement
    {
        public XmlClassData(XmlElement Node)
            : base(Node)
        {
        }

        protected XmlClassData(DslDocument Doc)
            : base(Doc.CreateElement("XmlClassData"))
        {
        }

        public DslElementList ElementData
        {
            get { return new DslElementList(typeof(XmlPropertyData), GetChildNode("ElementData")); }
        }
    }
}
