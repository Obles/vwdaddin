using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class DomainRelationship : DslElement
    {
        public DomainRelationship(XmlElement Node)
            : base(Node)
        {
        }

        public DomainRelationship(DslDocument Doc)
            : base(Doc.CreateElement("DomainRelationship"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Namespace", this.OwnerDocument.Dsl.Xml.GetAttribute("Namespace"));
        }

        public DomainRelationship(DslDocument Doc, String Name, String DisplayName, Boolean IsEmbedding)
            : base(Doc.CreateElement("DomainRelationship"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Name", Name);
            this.Xml.SetAttribute("DisplayName", DisplayName);
            this.Xml.SetAttribute("Namespace", this.OwnerDocument.Dsl.Xml.GetAttribute("Namespace"));
            this.Xml.SetAttribute("IsEmbedding", IsEmbedding.ToString());
        }

        public DslElementList Properties
        {
            get { return new DslElementList(typeof(DomainProperty), GetChildNode("Properties")); }
        }

        public DomainRole Source
        {
            get
            {
                DslElementList list = new DslElementList(typeof(DomainRole), GetChildNode("Source"));
                return list[0] as DomainRole;
            }
            set
            {
                XmlNode node = GetChildNode("Source");
                node.RemoveAll();
                (new DslElementList(typeof(DomainRole), node)).Append(value);
            }
        }

        public DomainRole Target
        {
            get
            {
                DslElementList list = new DslElementList(typeof(DomainRole), GetChildNode("Target"));
                return list[0] as DomainRole;
            }
            set
            {
                XmlNode node = GetChildNode("Target");
                node.RemoveAll();
                (new DslElementList(typeof(DomainRole), node)).Append(value);
            }
        }

        public DomainProperty CreateProperty(String Type, String Name, String DisplayName)
        {
            return Properties.Append(new DomainProperty(OwnerDocument, Type, Name, DisplayName)) as DomainProperty;
        }

        public bool IsEmbedding
        {
            get
            {
                try
                {
                    return bool.Parse(Xml.GetAttribute("IsEmbedding"));
                }
                catch
                {
                    return false;
                }
            }
            set { Xml.SetAttribute("IsEmbedding", value.ToString().ToUpper()); }
        }

        public void Print(String t)
        {
            PrintNodeName(t + "Relationship");
            PrintValue("Name", Xml.GetAttribute("Name"));
            PrintValue("IsEmbedding", Xml.GetAttribute("IsEmbedding"));
            Console.WriteLine();
            PrintValue(t + "Source", "\n");
            Source.Print(t + t);
            PrintValue(t + "Target", "\n");
            Target.Print(t + t);
        }

        /// <summary>Уничтожить всю дополнительную информацию о соединении</summary>
        public void Disconnect()
        {
            Dsl Dsl = OwnerDocument.Dsl;

            DomainClass source = Dsl.Classes[Source.RolePlayer] as DomainClass;
            DomainClass target = Dsl.Classes[Target.RolePlayer] as DomainClass;

            XmlClassData xcd = Dsl.XmlSerializationBehavior.GetClassData(source);
            xcd.ElementData.Remove(xcd.GetRelationshipData(this));

            if (this.IsEmbedding)
            {
                source.ElementMergeDirectives.Remove(
                    source.GetElementMergeDirective(target.Xml.GetAttribute("Name"))
                );
            }
            else
            {
                xcd = Dsl.XmlSerializationBehavior.GetClassData(target);
                xcd.Xml.RemoveAttribute("SerializeId");

                ConnectionBuilder cb = Dsl.GetConnectionBuilder(this);
                cb.SourceDirectives.Remove(cb.GetSourceConnectDirective(source));
                cb.TargetDirectives.Remove(cb.GetTargetConnectDirective(target));
            }
        }

        /// <summary>Построить дополнительную информацию о соединении между классами</summary>
        public void Connect(DomainClass source, DomainClass target)
        {
            Dsl Dsl = this.OwnerDocument.Dsl;
            Dsl.XmlSerializationBehavior.GetClassData(source).ElementData.Append(
                new XmlRelationshipData(this)
            );

            this.Source.RolePlayer = source.Xml.GetAttribute("Name");
            this.Target.RolePlayer = target.Xml.GetAttribute("Name");

            if (this.IsEmbedding)
            {
                source.ElementMergeDirectives.Append(new ElementMergeDirective(this));
            }
            else
            {
                Dsl.XmlSerializationBehavior.GetClassData(target).Xml.SetAttribute("SerializeId", "true");

                ConnectionBuilder cb = Dsl.GetConnectionBuilder(this);
                cb.SourceDirectives.Append(new RolePlayerConnectDirective(source));
                cb.TargetDirectives.Append(new RolePlayerConnectDirective(target));
            }
        }
    }
}
