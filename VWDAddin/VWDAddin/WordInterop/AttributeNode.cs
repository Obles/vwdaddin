using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin
{
    public class AttributeNode
    {
        public AttributeNode(WordDocument doc, XmlNode node)
        {
            AttributeXMLNode = node;
            if (node.ChildNodes.Count > 0)
                AttrName = AttributeXMLNode.FirstChild.FirstChild.FirstChild.FirstChild.FirstChild.Value.Substring(Definitions.CLASS_ATTR_NAME_PREFIX.Length);
        }

        public AttributeNode(WordDocument doc, string attribute)
        {
            AttrName = attribute;
            AttributeXMLNode = WordHelpers.CreateCustomNode(doc, "attr_section");
            XmlNode attrNameNode = WordHelpers.CreateCustomNode(doc, "attr_name");
            attrNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_PREFIX + attribute));
            AttributeXMLNode.AppendChild(attrNameNode);

            XmlNode attrDescrNode = WordHelpers.CreateCustomNode(doc, "attr_descr");
            attrDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_DESCR_PREFIX));
            AttributeXMLNode.AppendChild(attrDescrNode);
        }


        public XmlNode AttributeXMLNode;
        public string AttrName;
    }
}
