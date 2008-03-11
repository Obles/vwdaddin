using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin
{
    class WordHelpers
    {
        public static ClassNode GetClassNodeByID(List<ClassNode> nodeList, string id)
        {
            foreach (ClassNode node in nodeList)
            {
                if (id.Equals(node.ClassID))
                {
                    return node;
                }
            }
            return null;
        }

        public static XmlNode GetCustomXmlPropertyNode(XmlNode classNode)
        {
            foreach (XmlNode node in classNode.ChildNodes)
            {
                if (node.Name.Equals("w:customXmlPr"))
                {
                    return node;
                }
            }
            return null;
        }

        public static string GetName(Microsoft.Office.Interop.Visio.Shape shape, string shapeName)
        {
            if (shape.Name == shapeName)
                return shape.Text;
            foreach (Microsoft.Office.Interop.Visio.Shape childShape in shape.Shapes)
            {
                if (childShape.Name == shapeName)
                    return childShape.Text;
            }
            return string.Empty;
        }

        public static XmlNode CreateCustomNode(WordDocument doc, string elementName, string id, string connectionType)
        {
            XmlNode customNode = doc.CreateNode(XmlNodeType.Element, "w:customXml", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attr = doc.CreateAttribute("w:element", Definitions.WORD_PROCESSING_ML);
            attr.Value = elementName;
            customNode.Attributes.Append(attr);
            XmlNode customPropertyNode = doc.CreateNode(XmlNodeType.Element, "w:customXmlPr", Definitions.WORD_PROCESSING_ML);
            
            XmlNode attrNode = doc.CreateNode(XmlNodeType.Element, "w:attr", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attrName = doc.CreateAttribute("w:name", Definitions.WORD_PROCESSING_ML);
            attrName.Value = "GUID";
            XmlAttribute attrID = doc.CreateAttribute("w:val", Definitions.WORD_PROCESSING_ML);
            attrID.Value = id;
            attrNode.Attributes.Append(attrName);
            attrNode.Attributes.Append(attrID);
            customPropertyNode.AppendChild(attrNode);
            
            XmlNode attrSecondNode = doc.CreateNode(XmlNodeType.Element, "w:attr", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attrSecondName = doc.CreateAttribute("w:name", Definitions.WORD_PROCESSING_ML);
            attrSecondName.Value = "Connection Type";
            XmlAttribute attrType = doc.CreateAttribute("w:val", Definitions.WORD_PROCESSING_ML);
            attrType.Value = connectionType;
            attrSecondNode.Attributes.Append(attrSecondName);
            attrSecondNode.Attributes.Append(attrType);
            customPropertyNode.AppendChild(attrSecondNode);

            customNode.AppendChild(customPropertyNode);
            return customNode;
        }

        public static XmlNode CreateCustomNode(WordDocument doc, string elementName, string id)
        {
            XmlNode customNode = doc.CreateNode(XmlNodeType.Element, "w:customXml", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attr = doc.CreateAttribute("w:element", Definitions.WORD_PROCESSING_ML);
            attr.Value = elementName;
            customNode.Attributes.Append(attr);
            XmlNode customPropertyNode = doc.CreateNode(XmlNodeType.Element, "w:customXmlPr", Definitions.WORD_PROCESSING_ML);
            XmlNode attrNode = doc.CreateNode(XmlNodeType.Element, "w:attr", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attrName = doc.CreateAttribute("w:name", Definitions.WORD_PROCESSING_ML);
            attrName.Value = "GUID";
            XmlAttribute attrID = doc.CreateAttribute("w:val", Definitions.WORD_PROCESSING_ML);
            attrID.Value = id;
            attrNode.Attributes.Append(attrName);
            attrNode.Attributes.Append(attrID);
            customPropertyNode.AppendChild(attrNode);
            customNode.AppendChild(customPropertyNode);
            return customNode;
        }

        public static XmlNode CreateCustomNode(WordDocument doc, string elementName)
        {
            XmlNode customNode = doc.CreateNode(XmlNodeType.Element, "w:customXml", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attr = doc.CreateAttribute("w:element", Definitions.WORD_PROCESSING_ML);
            attr.Value = elementName;
            customNode.Attributes.Append(attr);
            return customNode;
        }

        public static XmlNode GetCustomChild(XmlNode node, string customName)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Attributes.Count > 0 && child.Attributes[0].Value == customName)
                    return child;
            }
            return null;
        }

        public static XmlNode CreateTextChildNode(WordDocument doc, string text)
        {
            XmlElement tagParagraph = doc.CreateElement("w:p", Definitions.WORD_PROCESSING_ML);
            XmlElement tagRun = doc.CreateElement("w:r", Definitions.WORD_PROCESSING_ML);
            tagParagraph.AppendChild(tagRun);
            XmlElement tagText = doc.CreateElement("w:t", Definitions.WORD_PROCESSING_ML);
            tagRun.AppendChild(tagText);
            XmlNode nodeText = doc.CreateNode(XmlNodeType.Text, "w:t", Definitions.WORD_PROCESSING_ML);
            nodeText.Value = text;
            tagText.AppendChild(nodeText);
            return tagParagraph;
        }

        public static string[] ConvertListToArray(List<string> list)
        {
            string[] result = new string[list.Count];
            int curr = 0;
            foreach (string str in list)
            {
                result[curr++] = str;
            }
            return result;
        }

        public static List<string> ConvertArrayToList(string[] array)
        {
            List<string> result = new List<string>();
            foreach (string str in array)
            {
                result.Add(str);
            }
            return result;
        }

    }
}
