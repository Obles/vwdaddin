using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class Dsl : DslElement
    {
        public Dsl(XmlElement Node)
            : base(Node)
        {
            if (Node == null) throw new NullReferenceException();
        }

        protected Dsl(DslDocument Doc)
            : base(Doc.CreateElement("Dsl"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
        }

        public DslElementList Classes
        {
            get { return new DslElementList(typeof(DomainClass), GetChildNode("Classes")); }
        }

        public DslElementList Relationships
        {
            get { return new DslElementList(typeof(DomainRelationship), GetChildNode("Relationships")); }
        }

        public DslElementList ConnectionBuilders
        {
            get { return new DslElementList(typeof(ConnectionBuilder), GetChildNode("ConnectionBuilders")); }
        }

        public XmlSerializationBehavior XmlSerializationBehavior
        {
            get { return new XmlSerializationBehavior(GetChildNode("XmlSerializationBehavior") as XmlElement); } 
        }

        public Diagram Diagram
        {
            get { return new Diagram(GetChildNode("Diagram") as XmlElement); }
        }

        public Designer Designer
        {
            get { return new Designer(GetChildNode("Designer") as XmlElement); }
        }

        public DomainClass CreateDomainClass(String Name, String DisplayName)
        {
            return Classes.Add(new DomainClass(OwnerDocument, Name, DisplayName)) as DomainClass;
        }

        public DomainRelationship CreateRelationship(DomainClass Source, Multiplicity SourceMultiplicity, DomainClass Target, Multiplicity TargetMultiplicity, Boolean IsEmbedding)
        {
            DomainRelationship dr = new DomainRelationship(
                OwnerDocument,
                Source.Xml.GetAttribute("Name") + (IsEmbedding ? "Has" : "References") + Target.Xml.GetAttribute("Name"),
                Source.Xml.GetAttribute("DisplayName") + (IsEmbedding ? " Has " : " References ") + Target.Xml.GetAttribute("DisplayName"),
                IsEmbedding
            );
            dr.Source = new DomainRole(OwnerDocument,
                Source.Xml.GetAttribute("Name"),
                Source.Xml.GetAttribute("DisplayName"),
                Target.Xml.GetAttribute("Name"),
                Target.Xml.GetAttribute("DisplayName"),
                SourceMultiplicity
            );
            dr.Target = new DomainRole(OwnerDocument,
                Target.Xml.GetAttribute("Name"),
                Target.Xml.GetAttribute("DisplayName"),
                Source.Xml.GetAttribute("Name"),
                Source.Xml.GetAttribute("DisplayName"),
                TargetMultiplicity
            );
            Relationships.Add(dr);
            return dr;
        }
        public void Print(String t)
        {
            PrintNodeName("Dsl");
            PrintValue("Version", Xml.GetAttribute("dslVersion"));
            PrintValue("Name", Xml.GetAttribute("Name"));
            Console.WriteLine();
            PrintValue("Classses", "\n");
            foreach (DomainClass dc in Classes)
            {
                dc.Print(t);
                Console.WriteLine();
            }
            PrintValue("Relationships", "\n");
            foreach (DomainRelationship dr in Relationships)
            {
                dr.Print(t);
                Console.WriteLine();
            }
        }
        public ConnectionBuilder GetConnectionBuilder(DomainRelationship Relationship)
        {
            String Name = Relationship.Xml.GetAttribute("Name");
            foreach (ConnectionBuilder cb in ConnectionBuilders)
            {
                if (cb.LinkConnectDirective == Name) return cb;
            }
            return null;
        }

        public static String SubName(String Name)
        {
            try
            {
                return Name.Substring(0, 1).ToLower() + Name.Substring(1);
            }
            catch
            {
                return Name;
            }
        }

        public DomainClass GetRootClass()
        {
            if (Diagram.Class != null)
            {
                System.Diagnostics.Trace.WriteLine("Getting root class from Diagram");
                return Classes[Diagram.Class] as DomainClass;
            }
            else if (Designer.RootClass != null)
            {
                System.Diagnostics.Trace.WriteLine("Getting root class from Designer");
                return Classes[Designer.RootClass] as DomainClass;
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("Root class not found");
                return null;
            }
        }

        public void SetRootClass(String ClassName)
        {
            System.Diagnostics.Trace.WriteLine("Setting root class " + ClassName);
            Diagram.Class = ClassName;
            Designer.RootClass = ClassName;

            if(ClassName != null)
            {
                XmlSerializationBehavior.GetClassData(Classes[ClassName] as DomainClass).Xml.SetAttribute("SerializeId", "true");
            }
        }
    }
}