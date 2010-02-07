using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class DslElementList
    {
        private Type ElemType;
        private XmlNode RootNode;

        public DslElementList(Type ElemType, XmlNode RootNode)
        {
            this.ElemType = ElemType;
            this.RootNode = RootNode;
        }

        public int Count
        {
            get { return RootNode.ChildNodes.Count; }
        }

        public DslElement this[int index]
        {
            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(XmlElement);

                Object[] obj = new Object[1];
                obj[0] = RootNode.ChildNodes[index];

                return ElemType.GetConstructor(types).Invoke(obj) as DslElement;
            }
        }

        public DslElement this[string name]
        {
            get
            {
                return FindByName(name);
            }
        }

        public DslElement FindByGuid(string guid)
        {
            return Find("Id", guid);
        }

        public DslElement FindByName(string elementName)
        {
            return Find("Name", elementName);
        }

        public DslElement FindIfExist(string propertyName, string propertyValue)
        {
            foreach (XmlElement Node in RootNode.ChildNodes)
            {
                if (Node.GetAttribute(propertyName) == propertyValue)
                {
                    return ElemType.GetConstructor(
                        new Type[] { typeof(XmlElement) }).Invoke(new Object[] { Node }) as DslElement;
                }
            }
            return null;
        }

        private DslElement Find(string parameterName, string parameterValue)
        {
            Type[] types = new Type[1];
            types[0] = typeof(XmlElement);

            Object[] obj = new Object[1];
            obj[0] = null;

            foreach (XmlElement Node in RootNode.ChildNodes)
            {
                if (Node.GetAttribute(parameterName) == parameterValue)
                {
                    obj[0] = Node;
                    break;
                }
            }
            return ElemType.GetConstructor(types).Invoke(obj) as DslElement;
        }

        public IEnumerator<DslElement> GetEnumerator()
        {
            Type[] types = new Type[1];
            types[0] = typeof(XmlElement);
            Object[] obj = new Object[1];

            foreach (XmlNode node in RootNode.ChildNodes)
            {
                obj[0] = node;
                yield return ElemType.GetConstructor(types).Invoke(obj) as DslElement;
            }
        }

        public DslElement Add(DslElement Node)
        {
            RootNode.AppendChild(Node.Xml);
            return Node;
        }

        public void Remove(DslElement Node)
        {
            RootNode.RemoveChild(Node.Xml);
        }

        public void RemoveAll()
        {
            RootNode.RemoveAll();
        }

        public void RemoveLinked(DslElement Node)
        {
            if (Node.Xml.HasAttribute("Name"))
            {
                MonikersCollection dcms = new MonikersCollection(Node.OwnerDocument, Node.Xml.Name);
                foreach (DslElement el in dcms)
                {
                    if (el.References(Node))
                    {
                        el.OwnerElement.DisposeLinked();
                    }
                }
            }
            Remove(Node);
        }
    }
}
