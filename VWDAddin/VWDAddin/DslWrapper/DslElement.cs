using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace VWDAddin.DslWrapper
{

    public class DslElement
    {
        private XmlElement xmlElement;
        public XmlElement Xml { get { return xmlElement; } }

        public DslElement(XmlElement xml)
        {
            xmlElement = xml;
//            if (xml == null) throw new NullReferenceException();
        }

        public DslDocument OwnerDocument { get { return Xml.OwnerDocument as DslDocument; } }

        public XmlNode SelectSingleNode(String xpath)
        {
            return Xml.SelectSingleNode(xpath, OwnerDocument.Manager);
        }

        public XmlNodeList SelectNodes(String xpath)
        {
            return Xml.SelectNodes(xpath, OwnerDocument.Manager);
        }

        private XmlNode GetChildNodeSimple(String Name)
        {
            XmlNode node = SelectSingleNode("p:" + Name);
            if (node == null)
            {
                return Xml.AppendChild(OwnerDocument.CreateElement(Name));
            }
            else return node;
        }

        public XmlNode GetChildNode(String xPath)
        {
            XmlNode node = null;
            DslElement e = this;
            foreach (String name in xPath.Split('/'))
            {
                node = e.GetChildNodeSimple(name);
                e = new DslElement(node as XmlElement);
            }
            return node;
        }

        protected void PrintNodeName(String s)
        {
            ConsoleColor col = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(s);
            Console.ForegroundColor = col;
        }
        protected void PrintValue(String name, String value)
        {
            ConsoleColor col = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" " + name + ": ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(value);
            Console.ForegroundColor = col;
        }

        public String GUID
        {
            get { return Xml.GetAttribute("Id"); }
            set { Xml.SetAttribute("Id", value); }
        }

        public bool IsValid
        {
            get { return Xml != null; }
        }

        public void FullRename(String Name)
        {
            String oldName = Xml.GetAttribute("Name");
            if (oldName == Name) return;
            if (oldName == String.Empty)
            {
                Xml.SetAttribute("Name", Name);
                return;
            }
            Regex regex = new Regex("([^a-zA-Z0-9])" + oldName + "([^a-zA-Z0-9])");

            OwnerDocument.InnerXml = regex.Replace(
                OwnerDocument.InnerXml, 
                new MatchEvaluator(
                    delegate(Match m) { return NameReplacer(m, Name); }
                )
            );
        }

        private static String NameReplacer(Match match, String name)
        {
            return match.Groups[1].Value + name + match.Groups[2].Value;
        }

        /// <summary>Изменить значение атрибута, если оно пусто</summary>
        public void SetAttributeIfEmpty(String name, String value)
        {
            if (Xml.GetAttribute(name) == String.Empty)
            {
                Xml.SetAttribute(name, value);
            }
        }

        /// <summary>Конструкция из некоторой коллекции, которая содержит внутри себя текущий элемент</summary>
        public DslElement OwnerElement
        {
            get 
            {
                XmlElement node = this.Xml;
                while (node != null)
                {
                    if (IsCollection(node.ParentNode))
                    {
                        return new DslElement(node);
                    }
                    else node = node.ParentNode as XmlElement;
                }
                return null; 
            }
        }

        /// <summary>Проверяем, является ли узел коллекцией</summary>
        /// <param name="xmlNode">Проверяемый узел</param>
        /// <returns>Да, если узел является коллекцией</returns>
        private bool IsCollection(XmlNode xmlNode)
        {
            if (xmlNode.Name.EndsWith("s") && !xmlNode.Name.EndsWith("ss")) return true;
            return xmlNode.Name.EndsWith("Data") && !xmlNode.Name.StartsWith("Xml");
        }
    }
}
