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
            attrName.Value = Definitions.ATTR_GUID;
            XmlAttribute attrID = doc.CreateAttribute("w:val", Definitions.WORD_PROCESSING_ML);
            attrID.Value = id;
            attrNode.Attributes.Append(attrName);
            attrNode.Attributes.Append(attrID);
            customPropertyNode.AppendChild(attrNode);
            
            XmlNode attrSecondNode = doc.CreateNode(XmlNodeType.Element, "w:attr", Definitions.WORD_PROCESSING_ML);
            XmlAttribute attrSecondName = doc.CreateAttribute("w:name", Definitions.WORD_PROCESSING_ML);
            attrSecondName.Value = Definitions.ATTR_CONNECTION_TYPE;
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
            attrName.Value = Definitions.ATTR_GUID;
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

        private static XmlNode CreateCustomTextNode(WordDocument doc, string elementName, string text)
        {
            XmlNode customNode = CreateCustomNode(doc, elementName);
            XmlElement runSubNode = doc.CreateElement("w:r", Definitions.WORD_PROCESSING_ML);
            XmlElement textSubNode = doc.CreateElement("w:t", Definitions.WORD_PROCESSING_ML);
            textSubNode.InnerText = text;
            runSubNode.AppendChild(textSubNode);
            customNode.AppendChild(runSubNode);
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

        // TODO: replace contentType by enum.
        public static XmlNode CreateTextChildNode(WordDocument doc, string contentType, string content, string element)
        {
            XmlElement tagParagraph = CreateParagraphNode(doc, GetParagraphStyle(element));
            if (tagParagraph != null)
            {
                tagParagraph.AppendChild(CreateCustomTextNode(doc, Definitions.CONTENT_TYPE_NODE, contentType));
                tagParagraph.AppendChild(CreateCustomTextNode(doc, Definitions.CONTENT_NODE, content));
            }
            return tagParagraph;
        }

        public static void AppendTextChildNode(WordDocument doc, XmlNode parentNode, string text, string element)
        {
            if (parentNode != null)
            {
                XmlElement tagText = doc.CreateElement("w:t", Definitions.WORD_PROCESSING_ML);
                parentNode.AppendChild(tagText);
                XmlNode nodeText = doc.CreateNode(XmlNodeType.Text, "w:t", Definitions.WORD_PROCESSING_ML);
                nodeText.Value = text;
                tagText.AppendChild(nodeText);
            }
        }

        private static XmlElement CreateParagraphNode(WordDocument doc, string paragraphStyle)
        {
            XmlElement paragraphNode = doc.CreateElement("w:p", Definitions.WORD_PROCESSING_ML);
            XmlNode paragraphPropertyNode = doc.CreateNode(XmlNodeType.Element, "w:pPr", Definitions.WORD_PROCESSING_ML);
            XmlNode styleNode = doc.CreateNode(XmlNodeType.Element, "w:pStyle", Definitions.WORD_PROCESSING_ML);
            XmlAttribute styleName = doc.CreateAttribute("w:val", Definitions.WORD_PROCESSING_ML);
            styleName.Value = paragraphStyle;
            styleNode.Attributes.Append(styleName);
            paragraphPropertyNode.AppendChild(styleNode);
            paragraphNode.AppendChild(paragraphPropertyNode);

            return paragraphNode;
        }

        public static string GetParagraphStyle(string element)
        {
            switch(element)
            {
                case Definitions.CLASS_NAME:
                    return Definitions.CLASS_NAME_STYLE;
                case Definitions.CLASS_PARENT:
                    return Definitions.CLASS_PARENT_STYLE;
                case Definitions.CLASS_DESCR:
                    return Definitions.CLASS_NAME_DESCR_STYLE;
                case Definitions.CLASS_ATTR_NAME:
                    return Definitions.CLASS_ATTR_NAME_STYLE;
                case Definitions.CLASS_ATTR_DESCR:
                    return Definitions.CLASS_ATTR_NAME_DESCR_STYLE;
                case Definitions.CLASS_ASSOC_NAME:
                    return Definitions.CLASS_ASSOC_NAME_STYLE;
                case Definitions.CLASS_ASSOC_DESCR:
                    return Definitions.CLASS_ASSOC_DESCR_STYLE;
                case Definitions.CLASS_ASSOC_NAME_END:
                    return Definitions.CLASS_ASSOC_NAME_END_STYLE;
                case Definitions.CLASS_ASSOC_MULT:
                    return Definitions.CLASS_ASSOC_MULT_STYLE;
                case Definitions.CLASS_ASSOC_TYPE:
                    return Definitions.CLASS_ASSOC_TYPE_STYLE;
                case Definitions.CLASS_ATTR_PART:
                    return Definitions.CLASS_ATTR_PART_STYLE;
                case Definitions.CLASS_ASSOC_PART:
                    return Definitions.CLASS_ASSOC_PART_STYLE;
                default:
                    return Definitions.CLASS_DEFAULT_STYLE;
            }
        }

        public static string GetFirstTextString(XmlNode customNode)
        {
            foreach (XmlNode paragraphNode in customNode.ChildNodes)
            {
                if (paragraphNode.Name.Equals("w:p"))
                    foreach (XmlNode regionNode in paragraphNode.ChildNodes)
                    {
                        if (regionNode.Name.Equals("w:r"))
                            foreach (XmlNode textNode in regionNode.ChildNodes)
                            {
                                if (textNode.Name.Equals("w:t"))
                                    return textNode.Value;
                            }
                    }
            }
            return string.Empty;
        }

        public static string CalcText(XmlNode customNode, string prefix)
        {
            string result = string.Empty;
            foreach (XmlNode paragraphNode in customNode.ChildNodes)
            {
                if (paragraphNode.Name.Equals("w:p"))
                    foreach (XmlNode regionNode in paragraphNode.ChildNodes)
                    {
                        if (regionNode.Name.Equals("w:r"))
                            foreach (XmlNode textNode in regionNode.ChildNodes)
                            {
                                if (textNode.Name.Equals("w:t"))
                                    foreach (XmlNode text in textNode.ChildNodes)
                                    {
                                        result += text.Value;
                                    }
                            }
                    }
            }
            return result;
        }

        public static bool IsCustomNode(XmlNode node, string name)
        {
            if (node.Name.Equals("w:customXml"))
            {
                foreach(XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Value.Equals(name))
                        return true;
                }
            }
            return false;
        }

        public static XmlNode CreateBookmarkStart(WordDocument doc, string classID)
        {
            XmlNode bookmarkNode = doc.CreateNode(XmlNodeType.Element, "w:bookmarkStart", Definitions.WORD_PROCESSING_ML);
            XmlAttribute id = doc.CreateAttribute("w:id", Definitions.WORD_PROCESSING_ML);
            XmlAttribute name = doc.CreateAttribute("w:name", Definitions.WORD_PROCESSING_ML);
            id.Value = name.Value = classID;
            bookmarkNode.Attributes.Append(id);
            bookmarkNode.Attributes.Append(name);
            return bookmarkNode;
        }

        public static XmlNode CreateBookmarkEnd(WordDocument doc, string classID)
        {
            XmlNode bookmarkNode = doc.CreateNode(XmlNodeType.Element, "w:bookmarkEnd", Definitions.WORD_PROCESSING_ML);
            XmlAttribute id = doc.CreateAttribute("w:id", Definitions.WORD_PROCESSING_ML);
            id.Value = classID;
            bookmarkNode.Attributes.Append(id);
            return bookmarkNode;
        }

        public static XmlNode CreateHyperlinkNode(WordDocument doc, string hyperlinkedClassID)
        {
            XmlNode hyperlinkNode = doc.CreateNode(XmlNodeType.Element, "w:hyperlink", Definitions.WORD_PROCESSING_ML);
            XmlAttribute anchor = doc.CreateAttribute("w:anchor", Definitions.WORD_PROCESSING_ML);
            XmlAttribute history = doc.CreateAttribute("w:history", Definitions.WORD_PROCESSING_ML);
            history.Value = "1";
            anchor.Value = hyperlinkedClassID;
            hyperlinkNode.Attributes.Append(anchor);
            hyperlinkNode.Attributes.Append(history);
            return hyperlinkNode;
        }

        public static XmlNode CreateHyperlinkTextNode(WordDocument doc, string hyperlinkedClassID, string contentType, string content, string element)
        {
            XmlElement tagParagraph = doc.CreateElement("w:p", Definitions.WORD_PROCESSING_ML);
            XmlNode paragraphPropertyNode = doc.CreateNode(XmlNodeType.Element, "w:pPr", Definitions.WORD_PROCESSING_ML);
            XmlNode styleNode = doc.CreateNode(XmlNodeType.Element, "w:pStyle", Definitions.WORD_PROCESSING_ML);
            XmlAttribute styleName = doc.CreateAttribute("w:val", Definitions.WORD_PROCESSING_ML);
            styleName.Value = WordHelpers.GetParagraphStyle(element);
            styleNode.Attributes.Append(styleName);
            paragraphPropertyNode.AppendChild(styleNode);
            tagParagraph.AppendChild(paragraphPropertyNode);

            XmlNode hyperlinkNode = doc.CreateNode(XmlNodeType.Element, "w:hyperlink", Definitions.WORD_PROCESSING_ML);
            XmlAttribute anchor = doc.CreateAttribute("w:anchor", Definitions.WORD_PROCESSING_ML);
            //XmlAttribute history = doc.CreateAttribute("w:history", Definitions.WORD_PROCESSING_ML);
            //history.Value = "1";
            anchor.Value = hyperlinkedClassID;
            hyperlinkNode.Attributes.Append(anchor);
            //hyperlinkNode.Attributes.Append(history);
            tagParagraph.AppendChild(hyperlinkNode);

            hyperlinkNode.AppendChild(CreateCustomTextNode(doc, Definitions.CONTENT_TYPE_NODE, contentType));
            hyperlinkNode.AppendChild(CreateCustomTextNode(doc, Definitions.CONTENT_NODE, content));

            return tagParagraph;
        }

        public static string[] ConvertListToArray(List<string> list)
        {
            return list.ToArray();
        }

        public static List<string> ConvertArrayToList(string[] array)
        {
            return new List<string>(array);
        }

    }
}
