using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class Moniker
    {
        public static String Get(DslElement This, String Name, String Moniker)
        {
            try
            {
                return This.SelectSingleNode("p:" + Name).SelectSingleNode("p:" + Moniker, This.OwnerDocument.Manager).Attributes["Name"].Value;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public static void Set(DslElement This, String Name, String Moniker, String Value)
        {
            if (Value == null)
            {
                XmlNode node = This.SelectSingleNode("p:" + Name);
                if (node != null) This.Xml.RemoveChild(node);
            }
            else
            {
                XmlElement node = This.GetChildNode(Name + "/" + Moniker) as XmlElement;
                node.SetAttribute("Name", Value);
            }
        }
    }
}
