using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{

    public class DslElement
    {
        private XmlElement XmlElement;
        public XmlElement Xml { get { return XmlElement; } }

        public DslElement(XmlElement xml)
        {
            XmlElement = xml;
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
    }
}
