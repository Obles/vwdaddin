using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Packaging;
using Microsoft.Office.Interop.Visio;
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
                _pkgOutputDoc = Package.Open(fileName, FileMode.Open, FileAccess.ReadWrite);
                Uri uri = new Uri("/word/document.xml", UriKind.Relative);
                _partDocumentXML = _pkgOutputDoc.GetPart(uri);
                this.Load(_partDocumentXML.GetStream(FileMode.Open, FileAccess.ReadWrite));
                Root = this.SelectSingleNode("//w:body/w:customXml[@w:element='body']", NamespaceManager);
                XmlNodeList nodeList = this.SelectNodes("//w:body/w:customXml/w:customXml[@w:element='class']", NamespaceManager);
                _classList.Clear();
                foreach (XmlNode node in nodeList)
                {
                    _classList.Add(new ClassNode(this, node));
                }
                IsAssociated = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
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
                    targetNode.AppendAssociation(sourceNode.GetAssociationNode(associationGuid, connectionType));
                    sourceNode.RemoveAssociation(associationGuid, connectionType);
                }
                else
                {
                    ChangeAssociationName(associationGuid, name);
                    ChangeAssociationEndName(associationGuid, endName, connectionType);
                    ChangeAssociationMP(associationGuid, endMP, connectionType);
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

        public void ChangeAssociationName(string associationGuid, string newName)
        {            
            foreach (ClassNode node in _classList)
            {
                if (node.CheckAssociation(associationGuid))
                {
                    node.ChangeAssociationName(associationGuid, newName);
                }
            }
        }

        public void DeleteAssociation(string associationGuid)
        {
            foreach (ClassNode node in _classList)
            {
                if (node.CheckAssociation(associationGuid))
                {
                    node.RemoveAssociation(associationGuid);
                }
            }
        }

        public void ChangeAssociationEndName(string associationGuid, string newName, string connectionType)
        {
            foreach (ClassNode node in _classList)
            {
                if (node.CheckAssociation(associationGuid, connectionType))
                {
                    node.ChangeAssociationEndName(associationGuid, newName, connectionType);
                    break;
                }
            }
        }

        public void ChangeAssociationMP(string associationGuid, string newName, string connectionType)
        {
            foreach (ClassNode node in _classList)
            {
                if (node.CheckAssociation(associationGuid, connectionType))
                {
                    node.ChangeAssociationMP(associationGuid, newName, connectionType);
                    break;
                }
            }
        }

        public void Syncronize(Document visioDocument, string pathToDoc)
        {
            try
            {
                if (IsAssociated)
                    CloseWordDocument();
                if (File.Exists(pathToDoc))
                {
                }
                else
                {
                    if (File.Exists("EmptyDoc.docx"))
                    {
                        File.Copy("EmptyDoc.docx", pathToDoc, true);
                    }
                    else
                    {
                        Debug.WriteLine("WORD_INTEROP.SYNCRONIZE : EmptyDoc not found");
                        return;
                    }
                }
                this.ParseDocx(pathToDoc);
                foreach (Shape shape in visioDocument.Pages[1].Shapes)
                {
                    switch (VisioHelpers.GetShapeType(shape))
                    {
                        case Constants.Class:
                            string classGUID = VisioHelpers.FromString(shape.get_Cells("User.GUID.Value").Formula);
                            string name = string.Empty;
                            string attributes = string.Empty;
                            foreach (Shape subshape in shape.Shapes)
                            {
                                if (VisioHelpers.GetShapeType(subshape) == "class_name")
                                    name = subshape.Text;
                                if (VisioHelpers.GetShapeType(subshape) == "attr_section")
                                    attributes = subshape.Text;
                            }
                            ClassNode targetNode = WordHelpers.GetClassNodeByID(_classList, classGUID);
                            if (targetNode == null)
                            {
                                AddClass(name, attributes, classGUID);
                            }
                            else
                            {
                                ChangeClassName(classGUID, name);
                                if (attributes.Length > 0)
                                    ChangeClassAttributes(classGUID, attributes);
                            }
                            break;
                        default:
                            break;
                    }
                }
                foreach (Shape shape in visioDocument.Pages[1].Shapes)
                {
                    switch (VisioHelpers.GetShapeType(shape))
                    {
                        case Constants.Association:
                        case Constants.Composition:
                            string assocGUID = VisioHelpers.FromString(shape.get_Cells("User.GUID.Value").Formula);
                            string name = shape.Text;
                            string sourceName = string.Empty;
                            string sourceMP = string.Empty;
                            string targetName = string.Empty;
                            string targetMP = string.Empty;
                            foreach (Shape subshape in shape.Shapes)
                            {
                                if (VisioHelpers.GetShapeType(subshape) == "end1_name")
                                    sourceName = subshape.Text;
                                if (VisioHelpers.GetShapeType(subshape) == "end1_mp")
                                    sourceMP = subshape.Text;
                                if (VisioHelpers.GetShapeType(subshape) == "end2_name")
                                    targetName = subshape.Text;
                                if (VisioHelpers.GetShapeType(subshape) == "end2_mp")
                                    targetMP = subshape.Text;
                            }
                            string sourceGUID = null;
                            string targetGUID = null;
                            Shape source = FindConnectedShape(shape, shape.get_Cells("BeginX").Formula);
                            Shape target = FindConnectedShape(shape, shape.get_Cells("EndX").Formula);
                            if (source != null && target != null)
                            {
                                sourceGUID = VisioHelpers.FromString(source.get_Cells("User.GUID.Value").Formula);
                                targetGUID = VisioHelpers.FromString(target.get_Cells("User.GUID.Value").Formula);
                            }
                            if (sourceGUID != null && targetGUID != null)
                            {
                                AddAssociation(sourceGUID, assocGUID, name, sourceName, sourceMP, VisioHelpers.GetShapeType(shape), Constants.ConnectionTypes.BeginConnected.ToString());
                                AddAssociation(targetGUID, assocGUID, name, targetName, targetMP, VisioHelpers.GetShapeType(shape), Constants.ConnectionTypes.EndConnected.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                DeleteClasses();
                CloseWordDocument();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);                
            }
        }

        public Shape FindConnectedShape(Shape shape, string connectionString)
        {
            string searchName = VisioHelpers.GetConnectedClassName(connectionString);
            foreach (Shape suspiciousShape in shape.Document.Pages[1].Shapes)
            {
                if (suspiciousShape.Name == searchName)
                {
                    return suspiciousShape;
                }
            }
            return null;
        }

        public void DeleteClasses()
        {
            List<string> deletingClasses = new List<string>();
            foreach (ClassNode node in _classList)
            {
                if (node.IsRemained)
                    node.DeleteAssociations();
                else
                    deletingClasses.Add(node.ClassID);                    
            }
            foreach(string id in deletingClasses)
                DeleteClass(id);
        }

        public void CloseWordDocument()
        {            
            try
            {
                if (IsAssociated)
                {                    
                    this.Save(_partDocumentXML.GetStream(FileMode.Create));
                    _pkgOutputDoc.Flush();
                    _pkgOutputDoc.Close();
                    IsAssociated = false;
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
