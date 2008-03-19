using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin
{
    public class AssociationNode
    {
        public AssociationNode(WordDocument doc, XmlNode node)
        {
            IsRemained = false;
            AssociationXmlNode = node;
            XmlNode property = WordHelpers.GetCustomXmlPropertyNode(node);
            if (property != null)
            {
                foreach (XmlNode attribute in property.ChildNodes)
                {
                    if (attribute.Attributes[0].Value.Equals(Definitions.ATTR_GUID))
                    {
                        AssociationGUID = attribute.Attributes[1].Value;
                    }
                    else if (attribute.Attributes[0].Value.Equals(Definitions.ATTR_CONNECTION_TYPE))
                    {
                        AssociationConnectionType = attribute.Attributes[1].Value;
                    }
                }
            }
        }

        public AssociationNode(WordDocument doc, string guid, string name, string endName, string endMP, string associationType, string connectionType)
        {
            IsRemained = true;
            AssociationGUID = guid;
            AssociationConnectionType = connectionType;
            _type = associationType;

            AssociationXmlNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_SECTION, guid, connectionType);
            XmlNode assocNameNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_NAME);
            if (associationType.Equals("Association"))
                assocNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_NAME_PREFIX + name, Definitions.CLASS_ASSOC_NAME));
            else
                assocNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_COMPOSITION_NAME_PREFIX + name, Definitions.CLASS_ASSOC_NAME));
            AssociationXmlNode.AppendChild(assocNameNode);

            XmlNode assocDescrNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_DESCR);
            assocDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_DESCR_PREFIX, Definitions.CLASS_ASSOC_DESCR));
            AssociationXmlNode.AppendChild(assocDescrNode);

            XmlNode assocNameEndNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_NAME_END);
            assocNameEndNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_NAME_END_PREFIX + endName, Definitions.CLASS_ASSOC_NAME_END));
            AssociationXmlNode.AppendChild(assocNameEndNode);

            XmlNode assocMultNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_MULT);
            assocMultNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_MULT_PREFIX + endMP, Definitions.CLASS_ASSOC_MULT));
            AssociationXmlNode.AppendChild(assocMultNode);

            XmlNode assocTypeNode = WordHelpers.CreateCustomNode(doc, Definitions.CLASS_ASSOC_TYPE);
            assocTypeNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_TYPE_PREFIX + associationType, Definitions.CLASS_ASSOC_TYPE));
            AssociationXmlNode.AppendChild(assocTypeNode);
        }

        public void ChangeAssociationName(string newName)
        {
            XmlNode nodeText = WordHelpers.GetFirstTextNode(WordHelpers.GetCustomChild(AssociationXmlNode, Definitions.CLASS_ASSOC_NAME));
            _type = Constants.Association;
            if (nodeText.Value.Substring(0, 10).Equals("Композиция"))
                _type = Constants.Composition;
            if(_type.Equals(Constants.Association))
                nodeText.Value = Definitions.CLASS_ASSOC_NAME_PREFIX + newName;
            else
                nodeText.Value = Definitions.CLASS_COMPOSITION_NAME_PREFIX + newName;
            IsRemained = true;
        }

        public void ChangeAssociationEndName(string newName)
        {
            XmlNode nodeText = WordHelpers.GetFirstTextNode(WordHelpers.GetCustomChild(AssociationXmlNode, Definitions.CLASS_ASSOC_NAME_END));
            nodeText.Value = Definitions.CLASS_ASSOC_NAME_END_PREFIX + newName;
            IsRemained = true;
        }

        public void ChangeAssociationMP(string newName)
        {
            XmlNode nodeText = WordHelpers.GetFirstTextNode(WordHelpers.GetCustomChild(AssociationXmlNode, Definitions.CLASS_ASSOC_MULT));
            nodeText.Value = Definitions.CLASS_ASSOC_MULT_PREFIX + newName;
            IsRemained = true;
        }        

        private string _associationGUID;
        public string AssociationGUID
        {
            get { return _associationGUID; }
            set { _associationGUID = value; }
        }
        private string _associationConnectionType;
        public string AssociationConnectionType
        {
            get { return _associationConnectionType; }
            set { _associationConnectionType = value; }
        }
        private string _type;
        public bool IsRemained;
        public XmlNode AssociationXmlNode;
    }
}
