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
    using EntitiesRelationshipsCollection = Dictionary<DomainClass, Dictionary<DomainClass, DomainRelationship>>;

    /// <summary>
    /// Dsl -> Visio
    /// </summary>
    public class VisioSync
    {

        private Logger Logger;

        /// <summary>
        /// Classes which are specific for dsl and therefore should not be synchronized
        /// </summary>
        private List<string> iplementationOnlyClasses;

        public VisioSync(Logger Logger)
        {
            this.Logger = Logger;
            this.Doc = new DslDocument();
            if (Logger.Document != null)
            {
                string documentPath = VisioHelpers.GetDSLPath(Logger.Document);
                if (!string.IsNullOrEmpty(documentPath))
                {
                    this.Doc.Load(documentPath);
                }
                if (Logger.Document.Pages.Count > 0)
                {
                    this.Page = new VisioPage(Logger.Document.Pages[1]);
                }
            }
        }

        private DslDocument Doc;
        private VisioPage Page;


        private EntitiesRelationshipsCollection CreateRelatedClassesDictionary()
        {
            EntitiesRelationshipsCollection relatedClasses = new EntitiesRelationshipsCollection();
            DslElementList dslClasses = Doc.Dsl.Classes;
            DslElementList dslRelationships = Doc.Dsl.Relationships;

            foreach (DomainClass dc in dslClasses)
            {
                if (!string.IsNullOrEmpty(dc.BaseClass))
                {
                    // inheritance
                    DomainClass baseClass = dslClasses.FindByName(dc.BaseClass) as DomainClass;
                    AddClassesRelationshipToCollection(relatedClasses, baseClass, dc, null);
                }
            }

            foreach (DomainRelationship dr in dslRelationships)
            {
                if (!string.IsNullOrEmpty(dr.Source.RolePlayer)
                    && !string.IsNullOrEmpty(dr.Target.RolePlayer))
                {
                    DomainClass targetClass = dslClasses.FindByName(dr.Target.RolePlayer) as DomainClass;
                    DomainClass sourceClass = dslClasses.FindByName(dr.Source.RolePlayer) as DomainClass;
                    if ((targetClass != null) && (sourceClass != null))
                    {
                        if (dr.IsEmbedding)
                        {
                            // composition
                            AddClassesRelationshipToCollection(relatedClasses, sourceClass, targetClass, dr);
                        }
                        else
                        {
                            // association
                            //AddClassesRelationshipToCollection(relatedClasses, sourceClass, targetClass, dr);
                            //AddClassesRelationshipToCollection(relatedClasses, targetClass, sourceClass, dr);
                        }
                    }
                }
            }

            return relatedClasses;
        }

        private void AddClassesRelationshipToCollection(EntitiesRelationshipsCollection erCollection, DomainClass dc, DomainClass relatedClass, DomainRelationship dr)
        {
            if (dc != null)
            {
                if (!erCollection.ContainsKey(dc))
                {
                    erCollection.Add(dc, new Dictionary<DomainClass, DomainRelationship>());
                }
                if (!erCollection[dc].ContainsKey(relatedClass))
                {
                    erCollection[dc].Add(relatedClass, dr);
                }
            }
        }

        private void MarkClassAsImplementationOnly(DomainClass dc)
        {
            DslAttribute dslAttribute = dc.DslAttributes.FindIfExist("Name", "EntityAttribute") as DslAttribute;
            if (dslAttribute != null)
            {
                dslAttribute["IsImplementationOnlyEntity"] = Boolean.TrueString;
            }
            else
            {
                dslAttribute = new DslAttribute(Doc, "EntityAttribute");
                dslAttribute["IsImplementationOnlyEntity"] = Boolean.TrueString;
                dc.DslAttributes.Add(dslAttribute);
            }
        }

        private void CreateImplementationOnlyClassesList(EntitiesRelationshipsCollection dependentClassesDictionary,
            ref List<DomainClass> implementationClassesResult,
            ref List<DomainRelationship> implementationRelationshipsResult,
            DomainClass domainClass)
        {
            if (implementationClassesResult.Contains(domainClass))
            {
                return;
            }

            implementationClassesResult.Add(domainClass);
            foreach (Dictionary<DomainClass, DomainRelationship> relatedClasses in dependentClassesDictionary.Values)
            {
                if (relatedClasses.ContainsKey(domainClass))
                {
                    implementationRelationshipsResult.Add(relatedClasses[domainClass]);
                }
            }

            if (dependentClassesDictionary.ContainsKey(domainClass))
            {
                foreach (DomainClass dc in dependentClassesDictionary[domainClass].Keys)
                {
                    if (!implementationClassesResult.Contains(dc))
                    {
                        CreateImplementationOnlyClassesList(dependentClassesDictionary,
                            ref implementationClassesResult,
                            ref implementationRelationshipsResult,
                            dc);
                    }
                }
            }
        }

        private void CreateImplementationOnlyRelationshipsList(Dictionary<string, List<string>> dependentClassesDictionary, ref List<string> dependentRelationshipsResult)
        {
            foreach (DomainRelationship dr in Doc.Dsl.Relationships)
            {
                //if (dr.IsEmbedding && dr.Source.RolePlayer
            }
        }

        public void Synchronize()
        {
            Trace.WriteLine("Synchronizing Visio");
            Trace.Indent();

            VisioMaster.count = 0;
            Logger.Active = false;

            // ManyDslEntities -> One Visio Entity...
            // Move the following code to a separate method

            EntitiesRelationshipsCollection relatedClassesDictionary = CreateRelatedClassesDictionary();
            List<DomainRelationship> implementationOnlyRelationships = new List<DomainRelationship>();
            List<DomainClass> implementationOnlyClasses = new List<DomainClass>();

            foreach (DomainClass dc in Doc.Dsl.Classes)
            {
                DslAttribute entityAttribute = dc.DslAttributes.FindIfExist("Name", "EntityAttribute") as DslAttribute;
                if (entityAttribute != null)
                {
                    bool implementationOnlyEntity;
                    if (Boolean.TryParse(entityAttribute["IsImplementationOnlyEntity"], out implementationOnlyEntity)
                        && implementationOnlyEntity)
                    {
                        CreateImplementationOnlyClassesList(relatedClassesDictionary, ref implementationOnlyClasses, ref implementationOnlyRelationships, dc);
                    }
                }
            }

            RemoveDisconnectedShapes();
            RemoveUnusedShapes(implementationOnlyClasses, implementationOnlyRelationships);
            SynchronizeClasses(implementationOnlyClasses);
            SynchronizeRelationships(implementationOnlyRelationships);
            SynchronizePage();

            Logger.Active = true;
            Logger = Logger.LoggerManager.ResetLogger(Logger.Document);
            Trace.Unindent();
            Trace.WriteLine("Visio Synchronized");
        }

        private void RemoveDisconnectedShapes()
        {
            Trace.WriteLine("Removing Disconnected Shapes");
            Trace.Indent();
            List<VisioConnector> removedShapes = new List<VisioConnector>();

            foreach (VisioConnector vc in Page.Connectors)
            {
                if (vc.Source == null || vc.Target == null)
                {
                    removedShapes.Add(vc);
                }
            }
            foreach (VisioConnector vc in removedShapes)
            {
                Trace.WriteLine(vc.Name);
                vc.Shape.Delete();
            }
            Trace.Unindent();
        }

        private void RemoveUnusedShapes(
            List<DomainClass> implementationOnlyClasses,
            List<DomainRelationship> implementationOnlyRelationships)
        {
            Trace.WriteLine("Removing Unused Shapes");
            Trace.Indent();
            List<VisioShape> removedShapes = new List<VisioShape>();

            foreach (VisioClass vc in Page.Classes)
            {
                DomainClass dc = Doc.Dsl.Classes.FindByGuid(vc.GUID) as DomainClass;
                if ((dc == null)
                    || !dc.IsValid
                    || implementationOnlyClasses.Contains(dc))
                {
                    removedShapes.Add(vc);
                }
            }
            foreach (VisioConnector vc in Page.Relationships)
            {
                DomainRelationship relationship = Doc.Dsl.Relationships.FindByGuid(vc.GUID) as DomainRelationship;

                //if (!VisioClass.IsDslRelationClassShape(vc.Source)
                //    && !VisioClass.IsDslRelationClassShape(vc.Target)
                if ((relationship == null) || !(relationship.IsValid)
                    || implementationOnlyRelationships.Contains(relationship))
                {
                    removedShapes.Add(vc);
                }

                // in case of we already have connector in visio for selected relationship
                // we have to remove connector and create the relationship class
                //if (relationship.Properties.Count > 0)
                //{
                //    removedShapes.Add(vc);
                //}

            }
            foreach (VisioConnector vc in Page.Inheritances)
            {
                DomainClass dc = Doc.Dsl.Classes.FindByGuid(new VisioShape(vc.Source).GUID) as DomainClass;
                DomainClass bc = Doc.Dsl.Classes.FindByGuid(new VisioShape(vc.Target).GUID) as DomainClass;

                if (!bc.Xml.GetAttribute("Name").Equals(dc.BaseClass))
                {
                    removedShapes.Add(vc);
                }
            }
            foreach (VisioShape vs in removedShapes)
            {
                String s = vs is VisioClass ? (vs as VisioClass).Name : vs is VisioConnector ? (vs as VisioConnector).Name : vs.GUID;
                Trace.WriteLine(s == String.Empty ? vs.GUID : s);
                vs.Shape.Delete();
            }
            Trace.Unindent();
        }

        private void SynchronizeClasses(List<DomainClass> implementationOnlyClasses)
        {
            Trace.WriteLine("Synchronizing Classes");
            Trace.Indent();
            foreach (DomainClass dc in Doc.Dsl.Classes)
            {
                if (!implementationOnlyClasses.Contains(dc))
                {
                    Shape shape = Page.Find(dc.GUID);
                    VisioClass vc = new VisioClass(shape == null ? VisioMaster.Drop(Page.Document, "Class") : shape);
                    vc.GUID = dc.GUID;
                    vc.Name = dc.Xml.GetAttribute("Name");
                    vc.DisplayName = dc.Xml.GetAttribute("DisplayName");
                    String attrs = "";
                    foreach (DomainProperty prop in dc.Properties)
                    {
                        attrs += prop.Xml.GetAttribute("Name") + "\n";
                    }
                    vc.Attributes = attrs.Trim();
                }
            }
            foreach (DomainClass dc in Doc.Dsl.Classes)
            {
                if (!implementationOnlyClasses.Contains(dc))
                {
                    if (dc.BaseClass != null)
                    {
                        Shape vc = Page.Find(dc.GUID);
                        Shape bc = Page.Find(Doc.Dsl.Classes[dc.BaseClass].GUID);

                        VisioList<VisioConnector> connectors = new VisioList<VisioConnector>(
                            Page.Shapes,
                            delegate(Shape shape)
                            {
                                VisioConnector conn = new VisioConnector(shape);
                                return conn.Source == vc && conn.Target == bc;
                            }
                        );
                        if (connectors.Count == 0)
                        {
                            VisioMaster.DropConnection(vc, bc, Constants.Generalization);
                        }
                    }
                }
            }
            Trace.Unindent();
        }

        private void SynchronizeRelationships(List<DomainRelationship> implementationOnlyRelationships)
        {
            Trace.WriteLine("Synchronizing Relationships");
            Trace.Indent();
            foreach (DomainRelationship dr in Doc.Dsl.Relationships)
            {
                Shape shape = Page.Find(dr.GUID);
                //if (dr.Properties.Count > 0)
                //{
                //    // Create a new class for this relationship
                //    // and associate it with an existing Dsl relationship
                //    VisioClass vc = new VisioClass(
                //        shape == null ? VisioMaster.Drop(Page.Document, "Class") : shape);
                //    vc.GUID = dr.GUID;
                //    vc.IsDslRelationClass = true;
                //    vc.Name = dr.Xml.GetAttribute("Name");
                //    vc.DisplayName = dr.Xml.GetAttribute("DisplayName");

                //    VisioConnector ingoingConnector = new VisioConnector(VisioMaster.Drop(Page.Document, (dr.IsEmbedding ? Constants.Composition : Constants.Association)));
                //    ingoingConnector.SetSourceMultiplicity(dr.Source.Multiplicity);
                //    ingoingConnector.SetTargetMultiplicity(Multiplicity.One);

                //    Shape sourceShape = Page.Find(Doc.Dsl.Classes[dr.Source.RolePlayer].GUID);
                //    ingoingConnector.Source = sourceShape;
                //    ingoingConnector.Target = vc.Shape;

                //    VisioConnector outgoingConnector = new VisioConnector(VisioMaster.Drop(Page.Document, (dr.IsEmbedding ? Constants.Composition : Constants.Association)));
                //    outgoingConnector.SetSourceMultiplicity(Multiplicity.One);
                //    outgoingConnector.SetTargetMultiplicity(dr.Target.Multiplicity);

                //    outgoingConnector.Source = vc.Shape;
                //    Shape targetShape = Page.Find(Doc.Dsl.Classes[dr.Target.RolePlayer].GUID);
                //    outgoingConnector.Target = targetShape;

                //}
                //else
                if (!implementationOnlyRelationships.Contains(dr))
                {
                    VisioConnector vc = new VisioConnector(
                        shape == null ? VisioMaster.Drop(Page.Document, (dr.IsEmbedding ? Constants.Composition : Constants.Association)) : shape
                    );
                    vc.GUID = dr.GUID;
                    vc.Name = dr.Xml.GetAttribute("Name");
                    vc.DisplayName = dr.Xml.GetAttribute("DisplayName");
                    vc.SourceText = dr.Source.Xml.GetAttribute("DisplayName");
                    vc.TargetText = dr.Target.Xml.GetAttribute("DisplayName");
                    vc.SetSourceMultiplicity(dr.Source.Multiplicity);
                    vc.SetTargetMultiplicity(dr.Target.Multiplicity);

                    shape = Page.Find(Doc.Dsl.Classes[dr.Source.RolePlayer].GUID);
                    if (vc.Source != shape) vc.Source = shape;

                    shape = Page.Find(Doc.Dsl.Classes[dr.Target.RolePlayer].GUID);
                    if (vc.Target != shape) vc.Target = shape;
                }
            }
            Trace.Unindent();
        }

        private void SynchronizePage()
        {
            Trace.WriteLine("Synchronizing Page");
            Trace.Indent();

            DomainClass dc = Doc.Dsl.GetRootClass();
            Page.RootClass = dc != null ? new VisioClass(Page.Find(dc.GUID)) : null;

            Trace.Unindent();
        }
    }
}
