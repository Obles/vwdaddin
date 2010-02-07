using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.Windows.Forms;
using VWDAddin.VisioLogger;
using VWDAddin.DslWrapper;
using VWDAddin.VisioWrapper;

namespace VWDAddin.Synchronize
{
    /// <summary>
    /// Visio -> Dsl
    /// </summary>
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

            TestRootClass();
            CreatingUniqueNames();
            DeleteRemovedDslElements();
            CreateElements();
            SynchronizeElements();
            UpdateSerializationInfo();
            BuildStructure();

            Trace.Unindent();
            Trace.WriteLine("DSL Synchronized");
        }

        private void TestRootClass()
        {
            Trace.WriteLine("Testing Root Class");
            Trace.Indent();

            if (Page.RootClass == null)
            {
                MessageBox.Show("�� ����� �������� �����", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Trace.Unindent();
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

        /// <summary>������������� ������� �������</summary>
        /// <param name="dc">�����, ������� ��������������</param>
        /// <param name="vc">�����, � ������� ��������������</param>
        private void SynchronizeProperties(DomainClass dc, VisioClass vc)
        {
            // �������� � ������� ��������
            String attrstr = "\n";
            String[] attrs = vc.Attributes.Split('\n');
            for (int i = 0; i < attrs.Length; i++)
            {
                attrs[i] = attrs[i].Trim();
                attrstr += attrs[i] + "\n";
            }

            // ��������� ����� ��������
            foreach (String attr in attrs)
            {
                if (attr.Length == 0) continue;
                if (dc.Properties[attr].Xml == null)
                {
                    dc.CreateProperty("/System/String", attr, attr);
                }
            }

            // ������� �������� ��������
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

        private void SynchronizeProperties(DomainRelationship dr, VisioClass vc)
        {
            // �������� � ������� ��������
            String attrstr = "\n";
            String[] attrs = vc.Attributes.Split('\n');
            for (int i = 0; i < attrs.Length; i++)
            {
                attrs[i] = attrs[i].Trim();
                attrstr += attrs[i] + "\n";
            }

            // ��������� ����� ��������
            foreach (String attr in attrs)
            {
                if (attr.Length == 0) continue;
                if (dr.Properties[attr].Xml == null)
                {
                    dr.CreateProperty("/System/String", attr, attr);
                }
            }

            // ������� �������� ��������
            for (int i = 0; i < dr.Properties.Count; i++)
            {
                DomainProperty prop = dr.Properties[i] as DomainProperty;
                if (!attrstr.Contains("\n" + prop.Xml.GetAttribute("Name") + "\n"))
                {
                    dr.Properties.RemoveLinked(prop);
                    i--;
                }
            }
        }

        /// <summary>�������� ����������� �������\���������</summary>
        private void CreateElements()
        {
            Trace.WriteLine("Creating DSL elements");
            Trace.Indent();

            // �������� ����������� �������
            foreach (VisioClass vc in Page.Classes)
            {
                if (vc.IsDslRelationClass)
                {
                    DomainRelationship dr = Doc.Dsl.Relationships.FindByGuid(vc.GUID) as DomainRelationship;
                    
                    // We can only create such VisioClass during synch with Dsl - it should not be possible to add such class from Visio. 
                    Debug.Assert(dr.IsValid);

                    SynchronizeProperties(dr, vc);
                }
                else
                {
                    DomainClass dc = Doc.Dsl.Classes.FindByGuid(vc.GUID) as DomainClass;
                    if (!dc.IsValid)
                    {
                        Trace.WriteLine(vc.Name);
                        dc = new DomainClass(Doc);
                        dc.GUID = vc.GUID;
                        Doc.Dsl.Classes.Add(dc);
                    }

                    // ������������� �������
                    SynchronizeProperties(dc, vc);
                }
            }

            // �������� ����������� ����������\����������
            foreach (VisioConnector vc in Page.Relationships)
            {
                DomainRelationship dr = Doc.Dsl.Relationships.FindByGuid(vc.GUID) as DomainRelationship;
                if (!dr.IsValid)
                {
                    Trace.WriteLine(vc.Name);
                    dr = new DomainRelationship(Doc);
                    dr.GUID = vc.GUID;
                    dr.Source = new DomainRole(Doc);
                    dr.Target = new DomainRole(Doc);
                    dr.IsEmbedding = vc.IsComposition;
                    Doc.Dsl.Relationships.Add(dr);
                }
            }
            Trace.Unindent();
        }

        /// <summary>������������ �������\���������</summary>
        private void SynchronizeElements()
        {
            Trace.WriteLine("Synchronizing DSL elements");
            Trace.Indent();

            // ������������� �������
            foreach (VisioClass vc in Page.Classes)
            {
                Trace.WriteLine(vc.Name);
                DomainClass dc = Doc.Dsl.Classes.FindByGuid(vc.GUID) as DomainClass;

                if (dc != null)
                {
                    dc.Xml.SetAttribute("DisplayName", vc.DisplayName);
                    dc.FullRename(vc.Name);
                }
            }

            // ������������� ����������\����������
            foreach (VisioConnector vc in Page.Relationships)
            {
                Trace.WriteLine(vc.Name);
                DomainRelationship dr = Doc.Dsl.Relationships.FindByGuid(vc.GUID) as DomainRelationship;

                if (dr != null)
                {
                    dr.Xml.SetAttribute("DisplayName", vc.DisplayName);

                    dr.FullRename(vc.Name);
                }
            }

            Trace.Unindent();
        }

        private bool IsImplementationRelationship(DomainRelationship dr)
        {
            DomainClass sourceClass = Doc.Dsl.Classes.FindIfExist("Name", dr.Source.RolePlayer) as DomainClass;
            DomainClass targetClass = Doc.Dsl.Classes.FindIfExist("Name", dr.Target.RolePlayer) as DomainClass;
            return IsImplementationClass(sourceClass) || IsImplementationClass(targetClass);
        }

        private bool IsImplementationClass(DomainClass dc)
        {
            if (dc == null)
            {
                return false;
            }
            
            DslAttribute entityAttribute = dc.DslAttributes.FindIfExist("Name", "EntityAttribute") as DslAttribute;
            if (entityAttribute != null)
            {
                bool implementationOnlyEntity;
                if (Boolean.TryParse(entityAttribute["IsImplementationOnlyEntity"], out implementationOnlyEntity)
                    && implementationOnlyEntity)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>���������� ��������� ��� ���������� � ������ �������\���������</summary>
        private void DeleteRemovedDslElements()
        {
            Trace.WriteLine("Destroying DSL structure");

            List<DomainRelationship> relationshipsToRemove = new List<DomainRelationship>();
            foreach (DomainRelationship dr in Doc.Dsl.Relationships)
            {
                if (!Page.Relationships.Contains(dr.GUID) && !IsImplementationRelationship(dr))
                {
                    relationshipsToRemove.Add(dr);
                }
            }

            foreach (DomainRelationship elem in relationshipsToRemove)
            {
                elem.Disconnect();
                XmlClassData xcd = Doc.Dsl.XmlSerializationBehavior.GetClassData(elem);
                Doc.Dsl.XmlSerializationBehavior.ClassData.RemoveLinked(xcd);
                Doc.Dsl.Relationships.RemoveLinked(elem);
            }

            List<DomainClass> classesToRemove = new List<DomainClass>();
            foreach (DomainClass dc in Doc.Dsl.Classes)
            {
                if (!Page.Classes.Contains(dc.GUID) && !IsImplementationClass(dc))
                {
                    classesToRemove.Add(dc);
                }
            }
            foreach (DomainClass elem in classesToRemove)
            {
                XmlClassData xcd = Doc.Dsl.XmlSerializationBehavior.GetClassData(elem);
                Doc.Dsl.XmlSerializationBehavior.ClassData.RemoveLinked(xcd);
                Doc.Dsl.Classes.RemoveLinked(elem);
            }
        }

        private void UpdateSerializationInfo()
        {
            Trace.WriteLine("Updating Serialization Info");
            Trace.Indent();
            foreach (VisioClass vc in Page.Classes)
            {
                Trace.WriteLine(vc.Name);
                DomainClass dc = Doc.Dsl.Classes.FindByGuid(vc.GUID) as DomainClass;
                XmlClassData xcd = Doc.Dsl.XmlSerializationBehavior.GetClassData(dc);
                if(xcd == null)
                {
                    Doc.Dsl.XmlSerializationBehavior.ClassData.Add(xcd = new XmlClassData(dc));
                }
                else xcd.Update(dc);

                foreach (DomainProperty dp in dc.Properties)
                {
                    XmlPropertyData xpd = xcd.GetPropertyData(dp);
                    if (xpd == null)
                    {
                        xcd.ElementData.Add(new XmlPropertyData(dp));
                    }
                    else xpd.Update(dp);
                }
            }

            foreach (VisioConnector vc in Page.Relationships)
            {
                Trace.WriteLine(vc.Name);
                DomainRelationship dr = Doc.Dsl.Relationships.FindByGuid(vc.GUID) as DomainRelationship;
                XmlClassData xcd = Doc.Dsl.XmlSerializationBehavior.GetClassData(dr);
                if (xcd == null)
                {
                    Doc.Dsl.XmlSerializationBehavior.ClassData.Add(xcd = new XmlClassData(dr));
                }
                else xcd.Update(dr);

                if (!dr.IsEmbedding)
                {
                    ConnectionBuilder cb = Doc.Dsl.GetConnectionBuilder(dr);
                    if(cb == null)
                    {
                        Doc.Dsl.ConnectionBuilders.Add(new ConnectionBuilder(dr));
                    }
                    else cb.Update(dr);
                }
            }

            Trace.Unindent();
        }

        /// <summary>������ ����� ��������� ������ �������\���������</summary>
        private void BuildStructure()
        {
            Trace.WriteLine("Building DSL structure");
            Trace.Indent();
            foreach (VisioConnector vc in Page.Inheritances)
            {
                DomainClass dc = Doc.Dsl.Classes.FindByGuid(new VisioClass(vc.Source).GUID) as DomainClass;
                dc.BaseClass = new VisioClass(vc.Target).Name;
            }
            foreach (VisioConnector vc in Page.Relationships)
            {
                DomainRelationship dr = Doc.Dsl.Relationships.FindByGuid(vc.GUID) as DomainRelationship;
                DomainClass src = Doc.Dsl.Classes.FindByGuid(new VisioClass(vc.Source).GUID) as DomainClass;
                DomainClass dst = Doc.Dsl.Classes.FindByGuid(new VisioClass(vc.Target).GUID) as DomainClass;


                FixRoles(vc, dr);

                if (string.Compare(dr.Source.GUID, src.GUID, StringComparison.OrdinalIgnoreCase) != 0
                    || string.Compare(dr.Target.GUID, dst.GUID, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    dr.Connect(src, dst);
                }
            }
            VisioClass root = Page.RootClass;
            Doc.Dsl.SetRootClass(root != null ? root.Name : null);
            Trace.Unindent();
        }

        private static void FixRoles(VisioConnector Connector, DomainRelationship Relationship)
        {
            String SourceName = new VisioClass(Connector.Source).Name;
            String TargetName = new VisioClass(Connector.Target).Name;

            //if (Relationship.Source.RolePlayer == SourceName &&
            //    Relationship.Target.RolePlayer == TargetName) return;

            String SourceText = Connector.SourceText == String.Empty ? SourceName : Connector.SourceText;
            String TargetText = Connector.TargetText == String.Empty ? TargetName : Connector.TargetText;

            DomainRole source = Relationship.Source;
            source.SetAttribute("Name", "s" + SourceName + "Name");
            source.SetAttribute("DisplayName", SourceText);
            source.SetAttribute("PropertyName", "s" + TargetName + "Prop");
            source.SetAttribute("PropertyDisplayName", TargetText);
            source.Multiplicity = MultiplicityHelper.Compatible(Connector.SourceMultiplicity);

            DomainRole target = Relationship.Target;
            target.SetAttribute("Name", "t" + TargetName + "Name");
            target.SetAttribute("DisplayName", TargetText);
            target.SetAttribute("PropertyName", "t" + SourceName + "Prop");
            target.SetAttribute("PropertyDisplayName", SourceText);
            target.Multiplicity = MultiplicityHelper.Compatible(Connector.TargetMultiplicity);
        }
    }
}
