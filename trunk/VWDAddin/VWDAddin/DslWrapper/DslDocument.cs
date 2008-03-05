using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    class DslDocument : XmlDocument
    {
        private XmlNamespaceManager nsManager;
        public XmlNamespaceManager Manager { get { return nsManager; } }
        private new const String NamespaceURI = "http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel";

        public DslDocument()
            : base()
        {
            nsManager = new XmlNamespaceManager(this.NameTable);
            nsManager.AddNamespace("p", NamespaceURI);
        }

        public Dsl Dsl
        {
            get { return new Dsl(SelectSingleNode("/p:Dsl", Manager) as XmlElement); }
        }
        
        public new XmlElement CreateElement(String ElementName)
        {
            return this.CreateElement(ElementName, NamespaceURI);
        }

        public DslElement CreateDslElement(String ElementName)
        {
            return new DslElement(this.CreateElement(ElementName));
        }
    }
}
