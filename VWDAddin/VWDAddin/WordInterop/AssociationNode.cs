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
            AssociationXmlNode = node;
            XmlNode property = WordHelpers.GetCustomXmlPropertyNode(node);
            if (property != null)
            {
                foreach (XmlNode attribute in property.ChildNodes)
                {
                    if (attribute.Attributes[0].Value.Equals("GUID"))
                    {
                        AssociationGUID = attribute.Attributes[1].Value;
                    }
                    else if (attribute.Attributes[0].Value.Equals("Connection Type"))
                    {
                        AssociationConnectionType = attribute.Attributes[1].Value;
                    }
                }
            }
        }

        public AssociationNode(WordDocument doc, string guid, string name, string endName, string endMP, string associationType, string connectionType)
        {
            AssociationGUID = guid;
            AssociationConnectionType = connectionType;

            AssociationXmlNode = WordHelpers.CreateCustomNode(doc, "assoc_section", guid, connectionType);
            XmlNode assocNameNode = WordHelpers.CreateCustomNode(doc, "assoc_name");
            assocNameNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_NAME_PREFIX + name));
            AssociationXmlNode.AppendChild(assocNameNode);

            XmlNode assocDescrNode = WordHelpers.CreateCustomNode(doc, "assoc_descr");
            assocDescrNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_DESCR_PREFIX));
            AssociationXmlNode.AppendChild(assocDescrNode);

            XmlNode assocNameEndNode = WordHelpers.CreateCustomNode(doc, "assoc_name_end");
            assocNameEndNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_NAME_END_PREFIX + endName));
            AssociationXmlNode.AppendChild(assocNameEndNode);

            XmlNode assocMultNode = WordHelpers.CreateCustomNode(doc, "assoc_mult");
            assocMultNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_MULT_PREFIX + endMP));
            AssociationXmlNode.AppendChild(assocMultNode);

            XmlNode assocTypeNode = WordHelpers.CreateCustomNode(doc, "assoc_type");
            assocTypeNode.AppendChild(WordHelpers.CreateTextChildNode(doc, Definitions.CLASS_ASSOC_TYPE_PREFIX + associationType));
            AssociationXmlNode.AppendChild(assocTypeNode);
        }

        public void ChangeAssociationName(string newName)
        {
            XmlNode nodeText = WordHelpers.GetCustomChild(AssociationXmlNode, "assoc_name");
            nodeText.FirstChild.FirstChild.FirstChild.FirstChild.Value = Definitions.CLASS_ASSOC_NAME_PREFIX + newName;
        }

        public void ChangeAssociationEndName(string newName)
        {
            XmlNode nodeText = WordHelpers.GetCustomChild(AssociationXmlNode, "assoc_name_end");
            nodeText.FirstChild.FirstChild.FirstChild.FirstChild.Value = Definitions.CLASS_ASSOC_NAME_END_PREFIX + newName;
        }

        public void ChangeAssociationMP(string newName)
        {
            XmlNode nodeText = WordHelpers.GetCustomChild(AssociationXmlNode, "assoc_mult");
            nodeText.FirstChild.FirstChild.FirstChild.FirstChild.Value = Definitions.CLASS_ASSOC_MULT_PREFIX + newName;
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
        public XmlNode AssociationXmlNode;
    }
}
