using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin {
  public class AttributeNode {
    public AttributeNode(WordDocument doc, XmlNode node) {
      m_attributeNode = node;
      m_attrName = m_attributeNode.FirstChild.FirstChild.FirstChild.FirstChild.FirstChild.Value.Substring(Definitions.CLASS_ATTR_NAME_PREFIX.Length);
    }

    public AttributeNode(WordDocument doc, string attribute) {
      m_attrName = attribute;
      m_attributeNode = Helpers.CreateCustomNode(doc, "attr_section", 0);
      XmlNode attrNameNode = Helpers.CreateCustomNode(doc, "attr_name", 0);
      attrNameNode.AppendChild(Helpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_PREFIX + attribute));
      m_attributeNode.AppendChild(attrNameNode);

      XmlNode attrDescrNode = Helpers.CreateCustomNode(doc, "attr_descr", 0);
      attrDescrNode.AppendChild(Helpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_DESCR_PREFIX));
      m_attributeNode.AppendChild(attrDescrNode);
    }
     

    public XmlNode m_attributeNode;
    public string m_attrName;
  }
}
