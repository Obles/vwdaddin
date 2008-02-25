using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Packaging;

namespace VWDAddin
{
  public class WordDocument : XmlDocument
  {

    public WordDocument()
      : base()
    {
      m_classList = new List<ClassNode>();
      m_FreeAssociationEnds = new List<AssociationNode>();
      m_namespaceManager = new XmlNamespaceManager(this.NameTable);
      m_namespaceManager.AddNamespace(Definitions.WORD_XML_PREFIX, Definitions.WORD_PROCESSING_ML);
    }

    public void ParseDocx(string fileName)
    {
      try
      {
        m_pkgOutputDoc = Package.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        Uri uri = new Uri("/word/document.xml", UriKind.Relative);
        m_partDocumentXML = m_pkgOutputDoc.GetPart(uri);
        this.Load(m_partDocumentXML.GetStream(FileMode.Open, FileAccess.Read));
        m_root = this.SelectSingleNode("//w:body/w:customXml[@w:element='body']", m_namespaceManager);
        XmlNodeList nodeList = this.SelectNodes("//w:body/w:customXml/w:customXml[@w:element='class']", m_namespaceManager);
        foreach (XmlNode node in nodeList)
        {
          m_classList.Add(new ClassNode(this, node));
        }
      }
      catch (Exception e)
      {
        int abc = 0;
      }

    }

    public void CommitChanges(List<SingleAction> actionLog)
    {
      try
      {
        foreach (SingleAction action in actionLog)
        {
          switch (action.m_actionType)
          {
            case Definitions.ACTION_TYPES.CLASS_ADDED:
              m_classList.Add(new ClassNode(this, action));
              break;
            case Definitions.ACTION_TYPES.CLASS_NAME_CHANGED:
              foreach (ClassNode node in m_classList)
              {
                if (node.m_classID == action.m_objectID)
                {
                  node.ChangeName(action.m_mainName);
                }
              }
              break;
            case Definitions.ACTION_TYPES.CLASS_DELETED:
              foreach (ClassNode node in m_classList)
              {
                if (node.m_classID == action.m_objectID)
                {
                  m_root.RemoveChild(node.m_classNode);
                  m_classList.Remove(node);
                  break;
                }
              }
              break;
            case Definitions.ACTION_TYPES.CLASS_ATTR_CHANGED:
              foreach (ClassNode node in m_classList)
              {
                if (node.m_classID == action.m_objectID)
                {
                  node.ChangeAttributes(this, action.m_attributes);
                }
              }
              break;
            case Definitions.ACTION_TYPES.ASSOCIATION_ADDED:
              m_FreeAssociationEnds.Add(new AssociationNode(this, action));
              break;
            case Definitions.ACTION_TYPES.ASSOCIATION_CONNECTED:
              AssociationNode assocNode = null;
              bool found = false;
              foreach (AssociationNode node in m_FreeAssociationEnds)
              {
                if (node.m_associationEndID == action.m_assocEndID && action.m_toEnd < 0.2)
                {
                  assocNode = node;
                  m_FreeAssociationEnds.Remove(node);
                  found = true;
                  break;
                }
              }
              if (found)
              {
                foreach (ClassNode node in m_classList)
                {
                  if (node.m_classID == action.m_objectID)
                  {
                    node.AppendAssociation(assocNode);
                    break;
                  }
                }
              }
              break;
          }
        }

        //this.Save(m_partDocumentXML.GetStream(FileMode.Create, FileAccess.Write));
        //m_pkgOutputDoc.Flush();
        //m_pkgOutputDoc.Close();
      }
      catch (Exception err)
      {

        int abc = 0;
      }
    }

    private XmlNamespaceManager m_namespaceManager;
    public XmlNamespaceManager Manager { get { return m_namespaceManager; } }
    private List<AssociationNode> m_FreeAssociationEnds;
    private List<ClassNode> m_classList;
    public XmlNode m_root;
    private PackagePart m_partDocumentXML;
    private Package m_pkgOutputDoc;
  }
}
