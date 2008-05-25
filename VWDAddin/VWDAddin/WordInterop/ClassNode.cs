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
            IsRemained = false;
            ParentRemained = false;
            _doc = doc;
            XmlNode property = WordHelpers.GetCustomXmlPropertyNode(node);
            if (property == null)
                throw new BadCustomXml();        
            foreach (XmlNode attribute in property.ChildNodes)
            {
                if (attribute.Attributes[0].Value.Equals(Definitions.ATTR_GUID))
                {
                    ClassID = attribute.Attributes[1].Value;
                }
            }
            XmlNode nodeAttrPart = WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ATTR_PART);
            if (nodeAttrPart == null)
                throw new BadCustomXml();
            foreach (XmlNode attrNode in nodeAttrPart.ChildNodes)
            {
                if (WordHelpers.IsCustomNode(attrNode, Definitions.CLASS_ATTR_SECTION))
                {
                    AttributeNode attribute = new AttributeNode(doc, attrNode);
                    _attrList.Add(attribute);
                    if (attribute.AttrName != null)
                        _attributes.Add(attribute.AttrName);
                }
            }
            XmlNode nodeAssocPart = WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ASSOC_PART);
            if (nodeAssocPart == null)
                throw new BadCustomXml();
            foreach (XmlNode assocNode in nodeAssocPart.ChildNodes)
            {
                if (WordHelpers.IsCustomNode(assocNode, Definitions.CLASS_ASSOC_SECTION))
                {
                    AssociationNode association = new AssociationNode(doc, assocNode);
                    _assocList.Add(association);
                }
            }
            if (WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_NAME) == null)
                throw new BadCustomXml();
            if (WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_PARENT) == null)
                throw new BadCustomXml();
            if (WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_DESCR) == null)
                throw new BadCustomXml();
        }

        public ClassNode(WordDocument doc, string name, string classAttributes, string id)
        {
            Init();
            _doc = doc;
            ClassXmlNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS, id);
            doc.Root.AppendChild(ClassXmlNode);

            ClassXmlNode.AppendChild(WordHelpers.CreateBookmarkStart(doc, id));
            XmlNode classNameNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_NAME);
            classNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_PREFIX + name, Definitions.CLASS_NAME));
            ClassXmlNode.AppendChild(classNameNode);
            ClassXmlNode.AppendChild(WordHelpers.CreateBookmarkEnd(doc, id));

            XmlNode classParentNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_PARENT);
            ClassXmlNode.AppendChild(classParentNode);

            XmlNode classDescrNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_DESCR);
            classDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_NAME_DESCR_PREFIX, Definitions.CLASS_DESCR));
            ClassXmlNode.AppendChild(classDescrNode);

            XmlNode classAttrPartNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ATTR_PART);
            classAttrPartNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ATTR_PART_PREFIX, Definitions.CLASS_ATTR_PART));
            if (!classAttributes.Equals(""))
            {
                string[] attributes = classAttributes.Split(new Char[] { '\n' });
                foreach (string attribute in attributes)
                {
                    AttributeNode attrNode = new AttributeNode(doc, attribute);
                    if (!attribute.Equals(""))
                    {                    
                        _attrList.Add(attrNode);
                        _attributes.Add(attribute);                        
                    }
                    classAttrPartNode.AppendChild(attrNode.AttributeXMLNode);
                }
            }
            ClassXmlNode.AppendChild(classAttrPartNode);

            XmlNode classAssocNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_PART);
            classAssocNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_PART_PREFIX, Definitions.CLASS_ASSOC_PART));
            ClassXmlNode.AppendChild(classAssocNode);

            ClassID = id;
            IsRemained = true;
        }

        public void ChangeName(string newName)
        {
            XmlNode classNameNode = WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_NAME);
            foreach (XmlNode child in classNameNode.ChildNodes)
                classNameNode.RemoveChild(child);
            classNameNode.AppendChild(WordHelpers.CreateTextChildNode(_doc, Definitions.CLASS_NAME_PREFIX + newName, Definitions.CLASS_NAME));
            IsRemained = true;
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
                WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ATTR_PART).AppendChild(attrNode.AttributeXMLNode);
            }
            IsRemained = true;
        }

        public void DeleteAttribute(string name)
        {
            foreach (AttributeNode node in _attrList)
            {
                if (node.AttrName == name)
                {
                    WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ATTR_PART).RemoveChild(node.AttributeXMLNode);
                    break;
                }
            }
        }

        public void AppendAssociation(AssociationNode assocNode)
        {
            assocNode.IsRemained = true;
            WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ASSOC_PART).AppendChild(assocNode.AssociationXmlNode);
            _assocList.Add(assocNode);
        }

        public void AppendAssociation(WordDocument doc, string associationGuid, string name, string endName, string endMP, string associationType, string connectionType)
        {
            AssociationNode newNode = new AssociationNode(doc, associationGuid, name, endName, endMP, associationType, connectionType);
            WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ASSOC_PART).AppendChild(newNode.AssociationXmlNode);
            _assocList.Add(newNode);
        }

        public AssociationNode GetAssociationNode(string associationGuid, string connectionType)
        {
            foreach (AssociationNode node in _assocList)
            {                
                if (node.AssociationGUID == associationGuid && node.AssociationConnectionType == connectionType)
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
                    WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ASSOC_PART).RemoveChild(node.AssociationXmlNode);
                    break;
                }
            }
        }

        public void RemoveAssociation(string associationGuid, string connectionType)
        {
            foreach (AssociationNode node in _assocList)
            {
                if (node.AssociationGUID == associationGuid && node.AssociationConnectionType == connectionType)
                {
                    _assocList.Remove(node);
                    WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_ASSOC_PART).RemoveChild(node.AssociationXmlNode);
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

        public bool CheckAssociation(string associationGuid)
        {
            foreach (AssociationNode node in _assocList)
            {
                if (node.AssociationGUID == associationGuid)
                {
                    return true;
                }
            }
            return false;
        }

        public void ChangeAssociationName(string associationGuid, string newName, string associationType)
        {
            foreach (AssociationNode node in _assocList)
            {
                if (node.AssociationGUID == associationGuid)
                {
                    node.ChangeAssociationName(newName, associationType);
                }
            }
        }

        public void ChangeAssociationEndName(string associationGuid, string newName, string connectionType)
        {
            foreach (AssociationNode node in _assocList)
            {
                if (node.AssociationGUID == associationGuid && node.AssociationConnectionType == connectionType)
                {
                    node.ChangeAssociationEndName(newName);
                }
            }
        }

        public void ChangeAssociationMP(string associationGuid, string newName, string connectionType)
        {
            foreach (AssociationNode node in _assocList)
            {
                if (node.AssociationGUID == associationGuid && node.AssociationConnectionType == connectionType)
                {
                    node.ChangeAssociationMP(newName);
                }
            }
        }

        public void AddParent(string parentGUID, string parentName)
        {
            XmlNode classParentNode = WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_PARENT);
            if (classParentNode == null)
            {
                classParentNode = WordHelpers.CreateCustomNode(_doc, Definitions.CLASS_PARENT);
                ClassXmlNode.InsertAfter(classParentNode, WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_NAME));
            }
            RemoveParent();
            XmlNode hyperLinkTextNode = WordHelpers.CreateHyperlinkTextNode(_doc, parentGUID, Definitions.CLASS_PARENT_PREFIX + parentName, Definitions.CLASS_PARENT);
            //XmlNode hyperlinkNode = WordHelpers.CreateHyperlinkNode(_doc, parentGUID);
            //XmlNode textNode = WordHelpers.CreateTextChildNode(_doc, Definitions.CLASS_PARENT_PREFIX + parentName, Definitions.CLASS_PARENT);
            //hyperlinkNode.AppendChild(textNode);
            classParentNode.AppendChild(hyperLinkTextNode);
            //classParentNode.AppendChild(textNode);
            IsRemained = ParentRemained = true;
        }

        public void RenameParent(string parentGUID, string newName)
        {
            XmlNode classParentNode = WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_PARENT);
            if (classParentNode == null)
            {
                return;
                //classParentNode = WordHelpers.CreateCustomNode(_doc, Definitions.CLASS_PARENT);
                //ClassXmlNode.InsertAfter(classParentNode, WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_NAME));
            }
            foreach (XmlNode child in classParentNode.ChildNodes)
            {
                if (child.Attributes.Count > 0 && child.Name == "w:hyperlink" && child.Attributes[0].Value == parentGUID)
                    AddParent(parentGUID, newName);
            }
        }

        public void RemoveParent()
        {
            XmlNode classParentNode = WordHelpers.GetCustomChild(ClassXmlNode, Definitions.CLASS_PARENT);
            //classParentNode.RemoveAll();
            int count = classParentNode.ChildNodes.Count;
            for (int i = 0; i < count; ++i)
                classParentNode.RemoveChild(classParentNode.ChildNodes[0]);
        }

        public void DeleteParent()
        {
            if (!ParentRemained)
                RemoveParent();
        }

        public void DeleteAssociations()
        {
            List<string> deletingAssocs = new List<string>();
            foreach (AssociationNode node in _assocList)
            {
                if (!node.IsRemained)
                {
                    deletingAssocs.Add(node.AssociationGUID);
                }
            }
            foreach (string id in deletingAssocs)
                RemoveAssociation(id);
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
        public bool IsRemained;
        public bool ParentRemained;
        private List<AttributeNode> _attrList;
        private List<String> _attributes;
        private List<AssociationNode> _assocList;
        private WordDocument _doc;
    }
}