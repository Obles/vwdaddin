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

        public DslElement this[String Name]
        {
            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(XmlElement);

                Object[] obj = new Object[1];
                obj[0] = null;
                
                foreach (XmlElement Node in RootNode.ChildNodes)
                {
                    if(Node.GetAttribute("Name") == Name)
                    {
                        obj[0] = Node;
                        break;
                    }
                }
                return ElemType.GetConstructor(types).Invoke(obj) as DslElement;
            }
        }

        public DslElement Find(String Guid)
        {
            Type[] types = new Type[1];
            types[0] = typeof(XmlElement);

            Object[] obj = new Object[1];
            obj[0] = null;

            foreach (XmlElement Node in RootNode.ChildNodes)
            {
                if (Node.GetAttribute("Id") == Guid)
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

        public DslElement Append(DslElement Node)
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
            String Name = Node.Xml.GetAttribute("Name");
            String Type = Node.Xml.Name;
            Remove(Node);

            if (Name == String.Empty) return;

            MonikersCollection dcms = new MonikersCollection(Node.OwnerDocument, Type);
            foreach (DslElement el in dcms)
            {
                if (el.References(Name))
                {
                    el.OwnerElement.DisposeLinked();
                }
            }
        }
    }
}
