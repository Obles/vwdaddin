using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin {
  public class ClassNode {
    public ClassNode(WordDocument doc, XmlNode node) {
      Init();
      _classNode = node;
      _classID = node.Attributes[1].Value;
      XmlNode nodeAttrPart = WordHelpers.GetCustomChild(_classNode, "attr_part");
      foreach (XmlNode attrNode in nodeAttrPart.ChildNodes) {
        AttributeNode attribute = new AttributeNode(doc, attrNode);
        _attrList.Add(attribute);
        _attributes.Add(attribute.m_attrName);
      }
    }
    
    public ClassNode(WordDocument doc, string name, string classAttributes, string id) {
      Init();
      _classNode = WordHelpers.CreateCustomNode(doc, "class", id);
      doc.Root.AppendChild(_classNode);
      XmlNode classNameNode = WordHelpers.CreateCustomNode(doc, "class_name", id);
      classNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_PREFIX + name));
      _classNode.AppendChild(classNameNode);

      XmlNode classDescrNode = WordHelpers.CreateCustomNode(doc, "class_descr", id);
      classDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_DESCR_PREFIX));
      _classNode.AppendChild(classDescrNode);

      XmlNode classAttrPartNode = WordHelpers.CreateCustomNode(doc, "attr_part", id);
      string[] attributes = classAttributes.Split(new Char[] { '\n' });
      foreach (string attribute in attributes) {
        AttributeNode attrNode = new AttributeNode(doc, attribute);
        _attrList.Add(attrNode);
        _attributes.Add(attribute);
        classAttrPartNode.AppendChild(attrNode.m_attributeNode);
      }
      _classNode.AppendChild(classAttrPartNode);

      XmlNode classAssocNode = WordHelpers.CreateCustomNode(doc, "assoc_part", id);
      _classNode.AppendChild(classAssocNode);

      _classID = id;
    }

    public void ChangeName(string newName) {
      XmlNode nodeText = WordHelpers.GetCustomChild(_classNode, "class_name");
      nodeText.FirstChild.FirstChild.FirstChild.FirstChild.Value = Definitions.CLASS_NAME_PREFIX + newName;
    }

    public void ChangeAttributes(WordDocument doc, string newAttrs) {
      List<string> newAttributes = WordHelpers.ConvertArrayToList(newAttrs.Split(new Char[] { '\n' }));
      List<string> toDelete = new List<string>();
      foreach (string attr in _attributes) {
        if (newAttrs.Contains(attr)) {
          newAttributes.Remove(attr);
        } else {
          DeleteAttribute(attr);
          toDelete.Add(attr);
        }
      }
      foreach(string deliting in toDelete)
        _attributes.Remove(deliting);
      foreach (string newAttr in newAttributes) {
        AttributeNode attrNode = new AttributeNode(doc, newAttr);
        _attrList.Add(attrNode);
        _attributes.Add(newAttr);
        WordHelpers.GetCustomChild(_classNode, "attr_part").AppendChild(attrNode.m_attributeNode);
      }
    }

    public void DeleteAttribute(string name) {
      foreach (AttributeNode node in _attrList) {
        if (node.m_attrName == name) {
          WordHelpers.GetCustomChild(_classNode, "attr_part").RemoveChild(node.m_attributeNode);
          break;
        }
      }
    }

    public void AppendAssociation(AssociationNode assocNode) {
      WordHelpers.GetCustomChild(_classNode, "assoc_part").AppendChild(assocNode.m_associationNode);
    }

    private void Init() {
      if (_attrList == null)
        _attrList = new List<AttributeNode>();
      if (_attributes == null)
        _attributes = new List<string>();
    }

    private XmlNode _classNode;
    private string _classID;
    private List<AttributeNode> _attrList;
    private List<String> _attributes;
    
  }
}