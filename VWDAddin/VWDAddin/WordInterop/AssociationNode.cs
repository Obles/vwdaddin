using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin {
  public class AssociationNode {
    public AssociationNode(WordDocument doc, XmlNode node) {
      //m_attributeNode = node;
      //m_attrName = m_attributeNode.FirstChild.FirstChild.FirstChild.FirstChild.FirstChild.Value.Substring(Definitions.CLASS_ATTR_NAME_PREFIX.Length);
    }

    //public AssociationNode(WordDocument doc, SingleAction action) {
    //  m_associationEndID = action.m_objectID;

    //  m_associationNode = WordHelpers.CreateCustomNode(doc, "assoc_section", 0);
    //  XmlNode assocNameNode = WordHelpers.CreateCustomNode(doc, "assoc_name", 0);
    //  assocNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_NAME_PREFIX + action.m_mainName));
    //  m_associationNode.AppendChild(assocNameNode);

    //  XmlNode assocDescrNode = WordHelpers.CreateCustomNode(doc, "assoc_descr", 0);
    //  assocDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_DESCR_PREFIX));
    //  m_associationNode.AppendChild(assocDescrNode);
      
    //  XmlNode assocNameEndNode = WordHelpers.CreateCustomNode(doc, "assoc_name_end", 0);
    //  assocNameEndNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_NAME_END_PREFIX + action.m_endName));
    //  m_associationNode.AppendChild(assocNameEndNode);

    //  XmlNode assocMultNode = WordHelpers.CreateCustomNode(doc, "assoc_mult", 0);
    //  assocMultNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_MULT_PREFIX + action.m_multiplicity));
    //  m_associationNode.AppendChild(assocMultNode);
      
    //  XmlNode assocTypeNode = WordHelpers.CreateCustomNode(doc, "assoc_type", 0);
    //  assocTypeNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_TYPE_PREFIX + action.m_associationType));
    //  m_associationNode.AppendChild(assocTypeNode);
    //}
     
    public int m_associationEndID;
    public XmlNode m_associationNode;
    //public string m_associationName;
    //public string m_associationNameEnd;
    //public string m_associationMult;
    //public Definitions.ASSOCIATION_TYPES m_associationType;
  }
}
