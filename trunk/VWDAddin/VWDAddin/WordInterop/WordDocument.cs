using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Packaging;
using System.Diagnostics;

namespace VWDAddin
{
    public class WordDocument : XmlDocument
    {
        public WordDocument()
          : base()
        {
            _classList = new List<ClassNode>();
            _freeAssociationEnds = new List<AssociationNode>();
            _namespaceManager = new XmlNamespaceManager(this.NameTable);
            _namespaceManager.AddNamespace(Definitions.WORD_XML_PREFIX, Definitions.WORD_PROCESSING_ML);
            IsAssociated = false;
        }

        public void ParseDocx(string fileName)
        {
            try
            {
                _pkgOutputDoc = Package.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Uri uri = new Uri("/word/document.xml", UriKind.Relative);
                _partDocumentXML = _pkgOutputDoc.GetPart(uri);
                this.Load(_partDocumentXML.GetStream(FileMode.Open, FileAccess.Read));
                Root = this.SelectSingleNode("//w:body/w:customXml[@w:element='body']", NamespaceManager);
                XmlNodeList nodeList = this.SelectNodes("//w:body/w:customXml/w:customXml[@w:element='class']", NamespaceManager);
                foreach (XmlNode node in nodeList)
                {
                    _classList.Add(new ClassNode(this, node));
                }
                IsAssociated = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void AddClass(string name, string attributes, string id)
        {
            _classList.Add(new ClassNode(this, name, attributes, id));
        }

        public void DeleteClass(string id)
        {
            ClassNode node = WordHelpers.GetClassNodeByID(_classList, id);
            if (null != node)
            {
                Root.RemoveChild(node.ClassXmlNode);
                _classList.Remove(node);
            }
            else
            {
                Debug.WriteLine("WORD_INTEROP.DELETE CLASS : UNKNOWN CLASS ID");
            }            
        }

        public void ChangeClassName(string id, string newName)
        {
            ClassNode node = WordHelpers.GetClassNodeByID(_classList, id);
            if (null != node)
            {
                node.ChangeName(newName);
            }
            else
            {
                Debug.WriteLine("WORD_INTEROP.CHANGE CLASS NAME : UNKNOWN CLASS ID");
            }
        }

        public void ChangeClassAttributes(string id, string newAttributes)
        {
            ClassNode node = WordHelpers.GetClassNodeByID(_classList, id);
            if (null != node)
            {
                node.ChangeAttributes(this, newAttributes);
            }
            else
            {
                Debug.WriteLine("WORD_INTEROP.CHANGE CLASS ATTRIBUTES : UNKNOWN CLASS ID");
            }
        }

        public void CloseWordDocument()
        {
            try
            {
                if (IsAssociated)
                {
                    this.Save(_partDocumentXML.GetStream(FileMode.Create, FileAccess.Write));
                    _pkgOutputDoc.Flush();
                    _pkgOutputDoc.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                _pkgOutputDoc.Close();
            }
        }

        //public void CommitChanges(List<SingleAction> actionLog)
        //{
        //  try
        //  {
        //    foreach (SingleAction action in actionLog)
        //    {
        //      switch (action.m_actionType)
        //      {
        //        case Definitions.ACTION_TYPES.ASSOCIATION_ADDED:
        //          m_FreeAssociationEnds.Add(new AssociationNode(this, action));
        //          break;
        //        case Definitions.ACTION_TYPES.ASSOCIATION_CONNECTED:
        //          AssociationNode assocNode = null;
        //          bool found = false;
        //          foreach (AssociationNode node in m_FreeAssociationEnds)
        //          {
        //            if (node.m_associationEndID == action.m_assocEndID && action.m_toEnd < 0.2)
        //            {
        //              assocNode = node;
        //              m_FreeAssociationEnds.Remove(node);
        //              found = true;
        //              break;
        //            }
        //          }
        //          if (found)
        //          {
        //            foreach (ClassNode node in m_classList)
        //            {
        //              if (node.m_classID == action.m_objectID)
        //              {
        //                node.AppendAssociation(assocNode);
        //                break;
        //              }
        //            }
        //          }
        //          break;
        //      }
        //    }

        //    //this.Save(m_partDocumentXML.GetStream(FileMode.Create, FileAccess.Write));
        //    //m_pkgOutputDoc.Flush();
        //    //m_pkgOutputDoc.Close();
        //  }
        //  catch (Exception err)
        //  {

        //    int abc = 0;
        //  }
        //}

        private XmlNamespaceManager _namespaceManager;
        public XmlNamespaceManager NamespaceManager 
        { 
            get { return _namespaceManager; } 
        }
        private List<AssociationNode> _freeAssociationEnds;
        private List<ClassNode> _classList;
        public XmlNode Root;
        private PackagePart _partDocumentXML;
        private Package _pkgOutputDoc;
        private bool _isAssociated;
        public bool IsAssociated
        {
            get { return _isAssociated; }
            set { _isAssociated = value; }
        }
    }
}
