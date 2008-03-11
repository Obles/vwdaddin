using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin
{
    public class ClassNode
    {
        public ClassNode(WordDocument doc, XmlNode node)
        {
            Init();
            ClassXmlNode = node;
            XmlNode property = WordHelpers.GetCustomXmlPropertyNode(node);
            if (property !=null)
            {
                foreach (XmlNode attribute in property.ChildNodes)
                {
                    if (attribute.Attributes[0].Value.Equals("GUID"))
                    {
                        ClassID = attribute.Attributes[1].Value;
                    }
                }
            }
            XmlNode nodeAttrPart = WordHelpers.GetCustomChild(ClassXmlNode, "attr_part");
            foreach (XmlNode attrNode in nodeAttrPart.ChildNodes)
            {
                AttributeNode attribute = new AttributeNode(doc, attrNode);
                _attrList.Add(attribute);
                if (attribute.AttrName != null)
                    _attributes.Add(attribute.AttrName);
            }
            XmlNode nodeAssocPart = WordHelpers.GetCustomChild(ClassXmlNode, "assoc_part");
            foreach (XmlNode assocNode in nodeAssocPart.ChildNodes)
            {
                AssociationNode association = new AssociationNode(doc, assocNode);
                _assocList.Add(association);                
            }
        }

        public ClassNode(WordDocument doc, string name, string classAttributes, string id)
        {
            Init();
            ClassXmlNode = WordHelpers.CreateCustomNode(doc, "class", id);
            doc.Root.AppendChild(ClassXmlNode);
            XmlNode classNameNode = WordHelpers.CreateCustomNode(doc, "class_name");
            classNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_PREFIX + name));
            ClassXmlNode.AppendChild(classNameNode);

            XmlNode classDescrNode = WordHelpers.CreateCustomNode(doc, "class_descr");
            classDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_DESCR_PREFIX));
            ClassXmlNode.AppendChild(classDescrNode);

            XmlNode classAttrPartNode = WordHelpers.CreateCustomNode(doc, "attr_part");
            if (!classAttributes.Equals(""))
            {
                string[] attributes = classAttributes.Split(new Char[] { '\n' });
                foreach (string attribute in attributes)
                {
                    if (!attribute.Equals(""))
                    {
                        AttributeNode attrNode = new AttributeNode(doc, attribute);
                        _attrList.Add(attrNode);
                        _attributes.Add(attribute);
                        classAttrPartNode.AppendChild(attrNode.AttributeXMLNode);
                    }
                }
            }
            ClassXmlNode.AppendChild(classAttrPartNode);

            XmlNode classAssocNode = WordHelpers.CreateCustomNode(doc, "assoc_part");
            ClassXmlNode.AppendChild(classAssocNode);

            ClassID = id;
        }

        public void ChangeName(string newName)
        {
            XmlNode nodeText = WordHelpers.GetCustomChild(ClassXmlNode, "class_name");
            nodeText.FirstChild.FirstChild.FirstChild.FirstChild.Value = Definitions.CLASS_NAME_PREFIX + newName;
        }

        public void ChangeAttributes(WordDocument doc, string newAttrs)
        {
            List<string> newAttributes = WordHelpers.ConvertArrayToList(newAttrs.Split(new Char[] { '\n' }));
            List<string> toDelete = new List<string>();
            foreach (string attr in _attributes)
            {
                if (newAttrs.Contains(attr))
                {
                    newAttributes.Remove(attr);
                }
                else
                {
                    DeleteAttribute(attr);
                    toDelete.Add(attr);
                }
            }
            foreach (string deliting in toDelete)
                _attributes.Remove(deliting);
            foreach (string newAttr in newAttributes)
            {
                AttributeNode attrNode = new AttributeNode(doc, newAttr);
                _attrList.Add(attrNode);
                _attributes.Add(newAttr);
                WordHelpers.GetCustomChild(ClassXmlNode, "attr_part").AppendChild(attrNode.AttributeXMLNode);
            }
        }

        public void DeleteAttribute(string name)
        {
            foreach (AttributeNode node in _attrList)
            {
                if (node.AttrName == name)
                {
                    WordHelpers.GetCustomChild(ClassXmlNode, "attr_part").RemoveChild(node.AttributeXMLNode);
                    break;
                }
            }
        }

        public void AppendAssociation(AssociationNode assocNode)
        {
            WordHelpers.GetCustomChild(ClassXmlNode, "assoc_part").AppendChild(assocNode.AssociationXmlNode);
            _assocList.Add(assocNode);
        }

        public void AppendAssociation(WordDocument doc, string associationGuid, string name, string endName, string endMP, string associationType, string connectionType)
        {
            AssociationNode newNode = new AssociationNode(doc, associationGuid, name, endName, endMP, associationType, connectionType);
            WordHelpers.GetCustomChild(ClassXmlNode, "assoc_part").AppendChild(newNode.AssociationXmlNode);
            _assocList.Add(newNode);
        }

        public AssociationNode GetAssociationNode(string associationGuid)
        {
            foreach (AssociationNode node in _assocList)
            {                
                if (node.AssociationGUID == associationGuid)
                {
                    _assocList.Remove(node);
                    return node;
                }
            }
            return null;
        }

        public void RemoveAssociation(string associationGuid)
        {
            foreach (AssociationNode node in _assocList)
            {                
                if (node.AssociationGUID == associationGuid)
                {
                    _assocList.Remove(node);
                    WordHelpers.GetCustomChild(ClassXmlNode, "assoc_part").RemoveChild(node.AssociationXmlNode);
                    break;
                }
            }
        }

        public bool CheckAssociation(string associationGuid, string connectionType)
        {
            foreach (AssociationNode node in _assocList)
            {
                if (node.AssociationGUID == associationGuid && node.AssociationConnectionType == connectionType)
                {
                    return true;
                }
            }
            return false;
        }

        private void Init()
        {
            if (_attrList == null)
                _attrList = new List<AttributeNode>();
            if (_attributes == null)
                _attributes = new List<string>();
            if (_assocList == null)
                _assocList = new List<AssociationNode>();
        }

        public XmlNode ClassXmlNode;
        public string ClassID;
        private List<AttributeNode> _attrList;
        private List<String> _attributes;
        private List<AssociationNode> _assocList;

    }
}