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

        public void AddAssociation(string classID, string associationGuid, string name, string endName, string endMP, string associationType, string connectionType)
        {
            ClassNode targetNode = WordHelpers.GetClassNodeByID(_classList, classID);
            ClassNode sourceNode = CheckAssociation(associationGuid, connectionType);
            if (null != sourceNode && null != targetNode)
            {
                if (sourceNode.ClassID != targetNode.ClassID)
                {
                    targetNode.AppendAssociation(sourceNode.GetAssociationNode(associationGuid));
                    sourceNode.RemoveAssociation(associationGuid);
                }
            }
            else if (null == sourceNode && null != targetNode)
            {
                targetNode.AppendAssociation(this, associationGuid, name, endName, endMP, associationType, connectionType);
            }
            else
            {
                Debug.WriteLine("WORD_INTEROP.ADD ASSOCIATION : UNKNOWN CLASS ID");
            }
        }

        public ClassNode CheckAssociation(string associationGuid, string connectionType)
        {
            foreach (ClassNode node in _classList)
            {
                if (node.CheckAssociation(associationGuid, connectionType))
                {
                    return node;
                }
            }
            return null;
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


        private XmlNamespaceManager _namespaceManager;
        public XmlNamespaceManager NamespaceManager 
        { 
            get { return _namespaceManager; } 
        }
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
