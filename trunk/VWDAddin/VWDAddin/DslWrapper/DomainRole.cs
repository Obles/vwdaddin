using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace VWDAddin.DslWrapper
{
    class DomainRole : DslElement
    {
        public DomainRole(XmlElement Node)
            : base(Node)
        {
        }

        public DomainRole(DslDocument Doc)
            : base(Doc.CreateElement("DomainRole"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
        }

        public DomainRole(DslDocument Doc, String Name, String DisplayName, String PropertyName, String PropertyDisplayName, Multiplicity Multiplicity)
            : base(Doc.CreateElement("DomainRole"))
        {
            this.Xml.SetAttribute("Id", Guid.NewGuid().ToString());
            this.Xml.SetAttribute("Name", Name);
            this.Xml.SetAttribute("DisplayName", DisplayName);
            this.Xml.SetAttribute("PropertyName", PropertyName);
            this.Xml.SetAttribute("PropertyDisplayName", PropertyDisplayName);
            this.Xml.SetAttribute("Multiplicity", Multiplicity.ToString());
            this.RolePlayer = Name;
        }

        public String RolePlayer
        {
            get { return Moniker.Get(this, "RolePlayer", "DomainClassMoniker"); }
            set { Moniker.Set(this, "RolePlayer", "DomainClassMoniker", value); }
        }

        public void Print(String t)
        {
            PrintNodeName(t + "Role");
            PrintValue("Name", Xml.GetAttribute("Name"));
            PrintValue("Multiplicity", MultiplicityHelper.Parse(Xml.GetAttribute("Multiplicity")).ToString());
            PrintValue("RolePlayer", RolePlayer);
            Console.WriteLine();
        }
    }
}
