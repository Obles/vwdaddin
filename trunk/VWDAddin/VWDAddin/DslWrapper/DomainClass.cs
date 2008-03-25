using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    public class DomainClass : DslElement
    {
        public DomainClass(XmlElement Node)
            : base(Node)
        {
            if (this.Xml != null)
            {
                XmlNode x = this.SelectSingleNode("p:ElementMergeDirectives");
                if (x != null) Node.RemoveChild(x);
            }
        }

        public DomainClass(DslDocument Doc)
            : base(Doc.CreateElement("DomainClass"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
        }

        public DomainClass(DslDocument Doc, String Name, String DisplayName)
            : base(Doc.CreateElement("DomainClass"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Name", Name);
            this.Xml.SetAttribute("DisplayName", DisplayName);
            this.Xml.SetAttribute("Namespace", this.OwnerDocument.Dsl.Xml.GetAttribute("Namespace"));
        }

        public DslElementList Properties
        {
            get { return new DslElementList(typeof(DomainProperty), GetChildNode("Properties")); }
        }

        public String BaseClass
        {
            get { return Moniker.Get(this, "BaseClass", "DomainClassMoniker"); }
            set { Moniker.Set(this, "BaseClass", "DomainClassMoniker", value); }
        }

        public DomainProperty CreateProperty(String Type, String Name, String DisplayName)
        {
            return Properties.Append(new DomainProperty(OwnerDocument, Type, Name, DisplayName)) as DomainProperty;
        }

        public DslElementList ElementMergeDirectives
        {
            get { return new DslElementList(typeof(ElementMergeDirective), GetChildNode("ElementMergeDirectives")); }
        }

        public void Print(String t)
        {
            PrintNodeName(t + "Class");
            PrintValue("Name", Xml.GetAttribute("Name"));
            if (BaseClass != null) PrintValue("Base", BaseClass);
            Console.WriteLine();

            foreach (DomainProperty dp in Properties)
            {
                dp.Print(t + t);
            }
        }

        public ElementMergeDirective GetElementMergeDirective(String Name)
        {
            foreach (ElementMergeDirective emd in ElementMergeDirectives)
            {
                if (emd.Index == Name) return emd;
            }
            return null;
        }
    }
}
