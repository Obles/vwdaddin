using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin {
  public class ClassNode {
    public ClassNode(WordDocument doc, XmlNode node) {
      Init();
      m_classNode = node;
      m_classID = Convert.ToInt32(node.Attributes[1].Value);
      XmlNode nodeAttrPart = Helpers.GetCustomChild(m_classNode, "attr_part");
      foreach (XmlNode attrNode in nodeAttrPart.ChildNodes) {
        AttributeNode attribute = new AttributeNode(doc, attrNode);
        m_attrList.Add(attribute);
        m_attributes.Add(attribute.m_attrName);
      }
    }
    
    public ClassNode(WordDocument doc, SingleAction action) {
      Init();
      m_classNode = Helpers.CreateCustomNode(doc, "class", action.m_objectID);
      doc.m_root.AppendChild(m_classNode);
      XmlNode classNameNode = Helpers.CreateCustomNode(doc, "class_name", action.m_objectID);
      classNameNode.AppendChild(Helpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_PREFIX + action.m_mainName));
      m_classNode.AppendChild(classNameNode);

      XmlNode classDescrNode = Helpers.CreateCustomNode(doc, "class_descr", action.m_objectID);
      classDescrNode.AppendChild(Helpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_DESCR_PREFIX));
      m_classNode.AppendChild(classDescrNode);

      XmlNode classAttrPartNode = Helpers.CreateCustomNode(doc, "attr_part", action.m_objectID);
      string[] attributes = action.m_attributes.Split(new Char[] {'\n'});
      foreach (string attribute in attributes) {
        AttributeNode attrNode = new AttributeNode(doc, attribute);
        m_attrList.Add(attrNode);
        m_attributes.Add(attribute);
        classAttrPartNode.AppendChild(attrNode.m_attributeNode);
      }
      m_classNode.AppendChild(classAttrPartNode);

      XmlNode classAssocNode = Helpers.CreateCustomNode(doc, "assoc_part", action.m_objectID);
      m_classNode.AppendChild(classAssocNode);

      m_classID = action.m_objectID;
    }

    public void ChangeName(string newName) {
      XmlNode nodeText = Helpers.GetCustomChild(m_classNode, "class_name");
      nodeText.FirstChild.FirstChild.FirstChild.FirstChild.Value = Definitions.CLASS_NAME_PREFIX + newName;
    }

    public void ChangeAttributes(WordDocument doc, string newAttrs) {
      List<string> newAttributes = Helpers.ConvertArrayToList(newAttrs.Split(new Char[] { '\n' }));
      List<string> toDelete = new List<string>();
      foreach (string attr in m_attributes) {
        if (newAttrs.Contains(attr)) {
          newAttributes.Remove(attr);
        } else {
          DeleteAttribute(attr);
          toDelete.Add(attr);
        }
      }
      foreach(string deliting in toDelete)
        m_attributes.Remove(deliting);
      foreach (string newAttr in newAttributes) {
        AttributeNode attrNode = new AttributeNode(doc, newAttr);
        m_attrList.Add(attrNode);
        m_attributes.Add(newAttr);
        Helpers.GetCustomChild(m_classNode, "attr_part").AppendChild(attrNode.m_attributeNode);
      }
    }

    public void DeleteAttribute(string name) {
      foreach (AttributeNode node in m_attrList) {
        if (node.m_attrName == name) {
          Helpers.GetCustomChild(m_classNode, "attr_part").RemoveChild(node.m_attributeNode);
          break;
        }
      }
    }

    public void AppendAssociation(AssociationNode assocNode) {
      Helpers.GetCustomChild(m_classNode, "assoc_part").AppendChild(assocNode.m_associationNode);
    }

    private void Init() {
      if (m_attrList == null)
        m_attrList = new List<AttributeNode>();
      if (m_attributes == null)
        m_attributes = new List<string>();
    }

    public XmlNode m_classNode;
    public int m_classID;
    public List<AttributeNode> m_attrList;
    public List<String> m_attributes;
    
  }
}