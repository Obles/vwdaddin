using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioWrapper;

namespace VWDAddin.DslWrapper
{
    public class DslCompare
    {
        public Document document;
        public DslCompare(Document document)
        {
            this.document = document;
        }

        protected delegate void CompareAction(DslElement e1, DslElement e2);

        protected void CompareLists(DslElementList list1, DslElementList list2, CompareAction CompareAction)
        {
            foreach (DslElement de1 in list1)
            {
                DslElement de2 = list2.Find(de1.GUID);
                if (de2.Xml == null)
                {
                    CompareAction(de1, null);
                }
                else CompareAction(de1, de2);
            }
            foreach (DslElement de2 in list2)
            {
                DslElement de1 = list1.Find(de2.GUID);
                if (de1.Xml == null)
                {
                    CompareAction(null, de2);
                }
            }
        }

        public void Compare(DslDocument dsl1, DslDocument dsl2)
        {
            CompareLists(
                dsl1.Dsl.Classes,
                dsl2.Dsl.Classes,
                CompareClasses
            );
            CompareLists(
                dsl1.Dsl.Relationships,
                dsl2.Dsl.Relationships,
                CompareRelationships
            );
        }

        protected void CompareClasses(DslElement de1, DslElement de2)
        {
            if(de2 == null)
            {
                Trace.WriteLine("Delete Class " + de1.GUID);
                VisioHelpers.GetShapeByGUID(de1.GUID, document).Delete();
                return;
            }
            VisioClass Class;
            if(de1 == null)
            {
                Trace.WriteLine("Create Class " + de2.GUID);
                Class = new VisioClass(VisioMaster.Drop(document, Constants.Class));
                de1 = new DomainClass(de2.OwnerDocument);
            }
            else Class = new VisioClass(VisioHelpers.GetShapeByGUID(de1.GUID, document));

            // Переносим изменения
            Class.GUID = de2.GUID;
            Class.Name = de2.Xml.GetAttribute("Name");
            //Class.DisplayName = de2.Xml.GetAttribute("DisplayName");

            // Переносим изменения наследования
            String b1 = (de1 as DomainClass).BaseClass;
            String b2 = (de2 as DomainClass).BaseClass;
            if (b1 != b2)
            {
                if (b1 != null)
                {
                    Trace.WriteLine("Delete Generalization");
                    Shape shape = Class.Generalization;
                    if(shape != null) shape.Delete();
                }
                if (b2 != null)
                {
                    Trace.WriteLine("Create Generalization");
                    VisioMaster.DropConnection(
                        Class.Shape,
                        VisioHelpers.GetShapeByGUID(de2.OwnerDocument.Dsl.Classes[b2].GUID, document),
                        Constants.Generalization
                    );
                }
            }

            CompareLists(
                (de1 as DomainClass).Properties,
                (de2 as DomainClass).Properties,
                CompareProperties
            );
        }

        protected void CompareRelationships(DslElement de1, DslElement de2)
        {
            if(de2 == null)
            {
                Trace.WriteLine("Delete Relationship " + de1.GUID);
                VisioHelpers.GetShapeByGUID(de1.GUID, document).Delete();
                return;
            }
            VisioConnector Conn;
            if(de1 == null)
            {
                Trace.WriteLine("Create Relationship " + de2.GUID);
                DomainRelationship dr = de2 as DomainRelationship;
                Conn = new VisioConnector(VisioMaster.DropConnection(
                    VisioHelpers.GetShapeByGUID(dr.OwnerDocument.Dsl.Classes[dr.Source.RolePlayer].GUID, document),
                    VisioHelpers.GetShapeByGUID(dr.OwnerDocument.Dsl.Classes[dr.Target.RolePlayer].GUID, document),
                    (dr.IsEmbedding ? Constants.Composition : Constants.Association)
                ));
                de1 = new DomainRelationship(de2.OwnerDocument);
            }
            else Conn = new VisioConnector(VisioHelpers.GetShapeByGUID(de1.GUID, document));

            // Переносим изменения
            Conn.GUID = de2.GUID;
            Conn.Name = de2.Xml.GetAttribute("Name");
            //Conn.DisplayName = de2.Xml.GetAttribute("DisplayName");

            String s = CompareMultiplicity(
                (de1 as DomainRelationship).Source, 
                (de2 as DomainRelationship).Source
            );
            if (s != null) Conn.SourceMultiplicity = s;
            s = CompareMultiplicity(
                (de1 as DomainRelationship).Target, 
                (de2 as DomainRelationship).Target
            );
            if (s != null) Conn.TargetMultiplicity = s;
        }

        protected void CompareProperties(DslElement de1, DslElement de2)
        {
            DomainClass dc;
            VisioClass Class;
            if (de1 == null)
            {
                Trace.WriteLine("Create Class Property " + de2.GUID);

                dc = (de2 as DomainProperty).Parent;
                Class = new VisioClass(VisioHelpers.GetShapeByGUID(dc.GUID, document));

                Class.Attributes = Class.Attributes.Trim() + "\n" + de2.Xml.GetAttribute("Name");
                Class.Attributes = Class.Attributes.Trim();
                return;
            }

            if (de2 != null && de2.Xml.GetAttribute("Name") == de1.Xml.GetAttribute("Name")) return;

            dc = (de1 as DomainProperty).Parent;
            Class = new VisioClass(VisioHelpers.GetShapeByGUID(dc.GUID, document));

            String[] str = new String[1];
            str[0] = de1.Xml.GetAttribute("Name");
            str = Class.Attributes.Split(str, 2, StringSplitOptions.None);

            if (de2 == null)
            {
                Trace.WriteLine("Delete Class Property " + de1.GUID);

                Class.Attributes = str[0].Trim() + "\n" + str[1].Trim();
            }
            else
            {
                Trace.WriteLine("Rename Class Property " + de1.GUID);

                Class.Attributes = str[0] + de1.Xml.GetAttribute("Name") + str[1];
            }
        }

        protected String CompareMultiplicity(DomainRole dr1, DomainRole dr2)
        {
            try
            {
                if (dr1.Xml.GetAttribute("Multiplicity") != dr2.Xml.GetAttribute("Multiplicity"))
                {
                    Trace.WriteLine("Change Multiplicity");
                    return MultiplicityHelper.AsDigits(dr2.Multiplicity);
                }
                else return null;
            }
            catch
            {
                return MultiplicityHelper.AsDigits(dr2.Multiplicity);
            }
        }

        public static void ApplyChanges(Document Document)
        {
            Trace.WriteLine("Applying Changes from DSL");
            Trace.Indent();
            try
            {
                String TempDslPath = VisioHelpers.GetTempDSLPath(Document);
                if (System.IO.File.Exists(TempDslPath))
                {
                    DslDocument dslOrig = new DslDocument();
                    dslOrig.Load(TempDslPath);

                    DslDocument dslNew = new DslDocument();
                    dslNew.Load(VisioHelpers.GetDSLPath(Document));

                    DslCompare comparer = new DslCompare(Document);
                    comparer.Compare(dslOrig, dslNew);

                    System.IO.File.Delete(TempDslPath);
                    Document.Save();
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.TargetSite + ": " + e.Message);
                Debug.WriteLine(e.StackTrace);
            }
            Trace.Unindent();
        }
    }
}
