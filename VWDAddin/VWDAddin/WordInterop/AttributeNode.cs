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
            {
                string attrString = WordHelpers.GetFirstTextNode(WordHelpers.GetCustomChild(AttributeXMLNode, Definitions.CLASS_ATTR_NAME)).Value;
                if(attrString != null)
                    AttrName = attrString.Substring(Definitions.CLASS_ATTR_NAME_PREFIX.Length);
            }
        }

        public AttributeNode(WordDocument doc, string attribute)
        {
            AttrName = attribute;
            AttributeXMLNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ATTR_SECTION);
            XmlNode attrNameNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ATTR_NAME);
            attrNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_PREFIX + attribute, Definitions.CLASS_ATTR_NAME));
            AttributeXMLNode.AppendChild(attrNameNode);

            XmlNode attrDescrNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ATTR_DESCR);
            attrDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_DESCR_PREFIX, Definitions.CLASS_ATTR_DESCR));
            AttributeXMLNode.AppendChild(attrDescrNode);
        }

        public XmlNode AttributeXMLNode;
        public string AttrName;
    }
}
