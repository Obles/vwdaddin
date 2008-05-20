using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;
using VWDAddin.DslWrapper;
using VWDAddin.VisioWrapper;

namespace VWDAddin.Synchronize
{
    class DslSync
    {
        private Logger Logger;

        public DslSync(Logger Logger)
        {
            this.Logger = Logger;
            this.Doc = Logger.DslDocument;
            this.Page = new VisioPage(Logger.Document.Pages[1]);
        }

        private DslDocument Doc;
        private VisioPage Page;

        public void Synchronize()
        {
            Trace.WriteLine("Synchronizing DSL");
            Trace.Indent();

            CreatingUniqueNames();
            DestroyStructure();
            CreateElements();
            SynchronizeElements();
            UpdateSerializationInfo();
            BuildStructure();

            Trace.Unindent();
            Trace.WriteLine("DSL Synchronized");
        }

        private void CreatingUniqueNames()
        {
            Trace.WriteLine("Creating Unique Names");
            Trace.Indent();

            foreach (VisioClass vc in Page.Classes)
            {
                vc.Name = UniqueNames.UniqueName(Page, vc);
            }

            foreach (VisioConnector vc in Page.Relationships)
            {
                vc.Name = UniqueNames.UniqueName(Page, vc);
            }

            Trace.Unindent();
        }

        /// <summary>Синхронизация свойств классов</summary>
        /// <param name="dc">Класс, который синхронизируем</param>
        /// <param name="vc">Класс, с которым синхронизируем</param>
        private void SyncProperties(DomainClass dc, VisioClass vc)
        {
            // Приводим в порядок атрибуты
            String attrstr = "\n";
            String[] attrs = vc.Attributes.Split('\n');
            for (int i = 0; i < attrs.Length; i++)
            {
                attrs[i] = attrs[i].Trim();
                attrstr += attrs[i] + "\n";
            }

            // Добавляем новые свойства
            foreach (String attr in attrs)
            {
                if (attr.Length == 0) continue;
                if (dc.Properties[attr].Xml == null)
                {
                    dc.CreateProperty("/System/String", attr, attr);
                }
            }

            // Удаляем ненужные свойства
            for (int i = 0; i < dc.Properties.Count; i++)
            {
                DomainProperty prop = dc.Properties[i] as DomainProperty;
                if (!attrstr.Contains("\n" + prop.Xml.GetAttribute("Name") + "\n"))
                {
                    dc.Properties.RemoveLinked(prop);
                    i--;
                }
            }
        }

        /// <summary>Создание недостающих классов\отношений</summary>
        private void CreateElements()
        {
            Trace.WriteLine("Creating DSL elements");
            Trace.Indent();

            // Создание недостающих классов
            foreach (VisioClass vc in Page.Classes)
            {
                DomainClass dc = Doc.Dsl.Classes.Find(vc.GUID) as DomainClass;
                if (!dc.IsValid)
                {
                    Trace.WriteLine(vc.Name);
                    dc = new DomainClass(Doc);
                    dc.GUID = vc.GUID;
                    Doc.Dsl.Classes.Append(dc);
                }

                // Синхронизация свойств
                SyncProperties(dc, vc);
            }

            // Создание недостающих ассоциаций\композиций
            foreach (VisioConnector vc in Page.Relationships)
            {
                DomainRelationship dr = Doc.Dsl.Relationships.Find(vc.GUID) as DomainRelationship;
                if (!dr.IsValid)
                {
                    Trace.WriteLine(vc.Name);
                    dr = new DomainRelationship(Doc);
                    dr.GUID = vc.GUID;
                    dr.Source = new DomainRole(Doc);
                    dr.Target = new DomainRole(Doc);
                    dr.IsEmbedding = vc.IsComposition;
                    Doc.Dsl.Relationships.Append(dr);
                }
            }
            Trace.Unindent();
        }

        /// <summary>Синхрнизация классов\отношений</summary>
        private void SynchronizeElements()
        {
            Trace.WriteLine("Synchronizing DSL elements");
            Trace.Indent();

            // Синхронизация классов
            foreach (VisioClass vc in Page.Classes)
            {
                Trace.WriteLine(vc.Name);
                DomainClass dc = Doc.Dsl.Classes.Find(vc.GUID) as DomainClass;

                dc.Xml.SetAttribute("DisplayName", vc.DisplayName);
                dc.FullRename(vc.Name);             
            }

            // Синхронизация ассоциаций\композиций
            foreach (VisioConnector vc in Page.Relationships)
            {
                Trace.WriteLine(vc.Name);
                DomainRelationship dr = Doc.Dsl.Relationships.Find(vc.GUID) as DomainRelationship;

                dr.Xml.SetAttribute("DisplayName", vc.DisplayName);
                dr.FullRename(vc.Name);
            }

            Trace.Unindent();
        }

        /// <summary>Уничтожаем известную нам информацию о связях классов\отношений</summary>
        private void DestroyStructure()
        {
            Trace.WriteLine("Destroying DSL structure");
            foreach (DomainClass dc in Doc.Dsl.Classes)
            {
                dc.BaseClass = null;
            }
            foreach (DomainRelationship dr in Doc.Dsl.Relationships)
            {
                dr.Disconnect();
            }
        }

        private void UpdateSerializationInfo()
        {
            Trace.WriteLine("Updating Serialization Info");
            Trace.Indent();
            foreach (VisioClass vc in Page.Classes)
            {
                Trace.WriteLine(vc.Name);
                DomainClass dc = Doc.Dsl.Classes.Find(vc.GUID) as DomainClass;
                XmlClassData xcd = Doc.Dsl.XmlSerializationBehavior.GetClassData(dc);
                if(xcd == null)
                {
                    Doc.Dsl.XmlSerializationBehavior.ClassData.Append(xcd = new XmlClassData(dc));
                }
                else xcd.Update(dc);

                foreach (DomainProperty dp in dc.Properties)
                {
                    XmlPropertyData xpd = xcd.GetPropertyData(dp);
                    if (xpd == null)
                    {
                        xcd.ElementData.Append(new XmlPropertyData(dp));
                    }
                    else xpd.Update(dp);
                }
            }
            foreach (VisioConnector vc in Page.Relationships)
            {
                Trace.WriteLine(vc.Name);
                DomainRelationship dr = Doc.Dsl.Relationships.Find(vc.GUID) as DomainRelationship;
                XmlClassData xcd = Doc.Dsl.XmlSerializationBehavior.GetClassData(dr);
                if (xcd == null)
                {
                    Doc.Dsl.XmlSerializationBehavior.ClassData.Append(xcd = new XmlClassData(dr));
                }
                else xcd.Update(dr);

                if (!dr.IsEmbedding)
                {
                    ConnectionBuilder cb = Doc.Dsl.GetConnectionBuilder(dr);
                    if(cb == null)
                    {
                        Doc.Dsl.ConnectionBuilders.Append(new ConnectionBuilder(dr));
                    }
                    else cb.Update(dr);
                }
            }
            Trace.Unindent();
        }

        /// <summary>Строим новую структуру связей классов\отношений</summary>
        private void BuildStructure()
        {
            Trace.WriteLine("Building DSL structure");
            Trace.Indent();
            foreach (VisioConnector vc in Page.Inheritances)
            {
                DomainClass dc = Doc.Dsl.Classes.Find(new VisioClass(vc.Source).GUID) as DomainClass;
                dc.BaseClass = new VisioClass(vc.Target).Name;
            }
            foreach (VisioConnector vc in Page.Relationships)
            {
                DomainRelationship dr = Doc.Dsl.Relationships.Find(vc.GUID) as DomainRelationship;
                DomainClass src = Doc.Dsl.Classes.Find(new VisioClass(vc.Source).GUID) as DomainClass;
                DomainClass dst = Doc.Dsl.Classes.Find(new VisioClass(vc.Target).GUID) as DomainClass;

                FixRoles(vc, dr);

                dr.Connect(src, dst);
            }
            VisioClass root = Page.RootClass;
            Doc.Dsl.SetRootClass(root != null ? root.Name : null);
            Trace.Unindent();
        }

        private static void FixRoles(VisioConnector Connector, DomainRelationship Relationship)
        {
            String SourceName = new VisioClass(Connector.Source).Name;
            String TargetName = new VisioClass(Connector.Target).Name;

            if (Relationship.Source.RolePlayer == SourceName &&
                Relationship.Target.RolePlayer == TargetName) return;

            String SourceText = Connector.SourceText == String.Empty ? SourceName : Connector.SourceText;
            String TargetText = Connector.TargetText == String.Empty ? TargetName : Connector.TargetText;

            DomainRole source = Relationship.Source;
            source.SetAttributeIfEmpty("Name", SourceName + "Name");
            source.SetAttributeIfEmpty("DisplayName", SourceText);
            source.SetAttributeIfEmpty("PropertyName", TargetName + "Prop");
            source.SetAttributeIfEmpty("PropertyDisplayName", TargetText);
            source.Multiplicity = MultiplicityHelper.Compatible(Connector.SourceMultiplicity);

            DomainRole target = Relationship.Target;
            target.SetAttributeIfEmpty("Name", TargetName + "Name");
            target.SetAttributeIfEmpty("DisplayName", TargetText);
            target.SetAttributeIfEmpty("PropertyName", SourceName + "Prop");
            target.SetAttributeIfEmpty("PropertyDisplayName", SourceText);
            target.Multiplicity = MultiplicityHelper.Compatible(Connector.TargetMultiplicity);
        }
    }
}
