using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.DslWrapper
{
    class DslCompare
    {
        public Document document;
        public DslCompare(Document document)
        {
            this.document = document;
        }

        protected delegate void DeleteAction(DslElement e);
        protected delegate void CreateAction(DslElement e);
        protected delegate void CompareAction(DslElement e1, DslElement e2);

        protected void CompareLists(DslElementList list1, DslElementList list2, DeleteAction DeleteAction, CreateAction CreateAction, CompareAction CompareAction)
        {
            foreach (DslElement de1 in list1)
            {
                DslElement de2 = list2.Find(de1.GUID);
                if (de2.Xml == null)
                {
                    DeleteAction(de1);
                }
                else CompareAction(de1, de2);
            }
            foreach (DslElement de2 in list2)
            {
                DslElement de1 = list1.Find(de2.GUID);
                if (de1.Xml == null)
                {
                    CreateAction(de2);
                }
            }
        }

        protected bool CompareAttributes(String attr, DslElement de1, DslElement de2)
        {
            return de1.Xml.GetAttribute(attr) != de2.Xml.GetAttribute(attr);
        }

        public void Compare(DslDocument dsl1, DslDocument dsl2)
        {
            CompareLists(
                dsl1.Dsl.Classes,
                dsl2.Dsl.Classes,
                delegate(DslElement e)
                {
                    Trace.WriteLine("Delete Class " + e.GUID);
                    VisioHelpers.GetShapeByGUID(e.GUID, document).Delete();
                },
                delegate(DslElement e)
                {
                    Trace.WriteLine("Create Class " + e.GUID);
                    Shape shape = VisioMaster.Drop(document, Constants.Class);
                    shape.get_Cells("User.GUID.Value").Formula = VisioHelpers.ToString(e.GUID);
                    //TODO при сравнении, если надо создать класс, отношение итп
                    // то надо его не просто создавать, а создавать со всеми параметрами
                    // м.б. надо вызывать сравнение с new domainclass
                },
                CompareClasses
            );
            CompareLists(
                dsl1.Dsl.Relationships,
                dsl2.Dsl.Relationships,
                delegate(DslElement e)
                {
                    Trace.WriteLine("Delete Relationship " + e.GUID);
                    VisioHelpers.GetShapeByGUID(e.GUID, document).Delete();
                },
                delegate(DslElement e)
                {
                    Trace.WriteLine("Create Relationship " + e.GUID);
                    DomainRelationship dr = e as DomainRelationship;
                    VisioMaster.DropConnection(
                        VisioHelpers.GetShapeByGUID(dr.OwnerDocument.Dsl.Classes[dr.Source.RolePlayer].GUID, document),
                        VisioHelpers.GetShapeByGUID(dr.OwnerDocument.Dsl.Classes[dr.Target.RolePlayer].GUID, document),
                        (dr.IsEmbedding ? Constants.Composition : Constants.Association),
                        ClassConnections.Undef,
                        ClassConnections.Undef
                    );
                    //TODO аналогично сравнению классов
                },
                CompareRelationships
            );
        }

        protected void CompareClasses(DslElement de1, DslElement de2)
        {
            //CompareAttributes(ActionType.EditDomainClass, "Name", de1, de2);
            //CompareAttributes(ActionType.EditDomainClass, "DisplayName", de1, de2);

            String b1 = (de1 as DomainClass).BaseClass;
            String b2 = (de2 as DomainClass).BaseClass;
            if (b1 != b2)
            {
                if (b1 == null) // b1 == null, b2 != null
                {
                    Trace.WriteLine("Create Generalization");
                    VisioMaster.DropConnection(
                        VisioHelpers.GetShapeByGUID(de2.GUID, document),
                        VisioHelpers.GetShapeByGUID(de2.OwnerDocument.Dsl.Classes[b2].GUID, document),
                        Constants.Generalization,
                        ClassConnections.Undef,
                        ClassConnections.Undef
                    );
                }
                else if (b2 == null) // b1 != null, b2 == null
                {
                    Trace.WriteLine("Delete Generalization");
                    //TODO Удаление наследования
                }
                else // b1 != null, b2 != null
                {
                    Trace.WriteLine("Change Generalization");
                    //TODO Перевешивание наследования
                }
            }

            CompareLists(
                (de1 as DomainClass).Properties,
                (de2 as DomainClass).Properties,
                delegate(DslElement e)
                {
                    //TODO Удаление Property
                    Trace.WriteLine("Delete Class Property " + e.GUID);
                },
                delegate(DslElement e)
                {
                    //TODO Создание Property
                    Trace.WriteLine("Create Class Property " + e.GUID);
                    //TODO аналогично сравнению классов
                },
                CompareProperties
            );
        }

        protected void CompareRelationships(DslElement de1, DslElement de2)
        {
            //CompareAttributes(ActionType.EditRelationship, "Name", de1, de2);
            //CompareAttributes(ActionType.EditRelationship, "DisplayName", de1, de2);
            //CompareAttributes(ActionType.EditRelationship, "IsEmbedding", de1, de2);

            CompareMultiplicity((de1 as DomainRelationship).Source, (de2 as DomainRelationship).Source);
            CompareMultiplicity((de1 as DomainRelationship).Target, (de2 as DomainRelationship).Target);
        }

        protected void CompareProperties(DslElement de1, DslElement de2)
        {
            //CompareAttributes(ActionType.EditProperty, "Name", de1, de2);
            //CompareAttributes(ActionType.EditProperty, "DisplayName", de1, de2);
        }

        protected void CompareMultiplicity(DomainRole dr1, DomainRole dr2)
        {
            if (dr1.Xml.GetAttribute("Multiplicity") != dr2.Xml.GetAttribute("Multiplicity"))
            {
                Trace.WriteLine("Change Multiplicity");
                //TODO изменение множественности
            }
        }

        public static void ApplyChanges(Document document)
        {
            Trace.WriteLine("Applying Changes from DSL");
            Trace.Indent();
            try
            {
                DslDocument dslOrig = new DslDocument();
                dslOrig.Load(VisioHelpers.GetTempDSLPath(document));

                DslDocument dslNew = new DslDocument();
                dslNew.Load(VisioHelpers.GetDSLPath(document));

                DslCompare comparer = new DslCompare(document);
                comparer.Compare(dslOrig, dslNew);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            Trace.Unindent();
        }
    }
}
