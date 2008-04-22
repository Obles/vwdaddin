using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class MonikersCollection
    {
        public MonikersCollection(DslDocument OwnerDocument, String MonikerType)
        {
            this.ownerDocument = OwnerDocument;
            this.monikerType = MonikerType + "Moniker";
        }

        private DslDocument ownerDocument;
        public DslDocument OwnerDocument { get { return ownerDocument; } }

        private String monikerType;
        private String MonikerType { get { return monikerType; } }

        public IEnumerator<DslElement> GetEnumerator()
        {
            Queue<DslElement> Queue = new Queue<DslElement>();
            Queue.Enqueue(OwnerDocument.Dsl);

            while (Queue.Count > 0)
            {
                DslElement root = Queue.Dequeue();
                if (root.IsValid)
                {
                    if (root.Xml.Name == monikerType)
                    {
                        yield return root;
                    }
                 
                    foreach (XmlNode node in root.Xml.ChildNodes)
                    {
                        Queue.Enqueue(new DslElement(node as XmlElement));
                    }
                }
            }
        }
    }
}
