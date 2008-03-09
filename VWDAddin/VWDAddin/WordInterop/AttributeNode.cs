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
      m_attributeNode = WordHelpers.CreateCustomNode(doc, "attr_section", string.Empty);
      XmlNode attrNameNode = WordHelpers.CreateCustomNode(doc, "attr_name", string.Empty);
      attrNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_PREFIX + attribute));
      m_attributeNode.AppendChild(attrNameNode);

      XmlNode attrDescrNode = WordHelpers.CreateCustomNode(doc, "attr_descr", string.Empty);
      attrDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_NAME_DESCR_PREFIX));
      m_attributeNode.AppendChild(attrDescrNode);
    }
     

    public XmlNode m_attributeNode;
    public string m_attrName;
  }
}
