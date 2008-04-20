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
    public class VisioSync
    {
        private Logger Logger;

        public VisioSync(Logger Logger)
        {
            this.Logger = Logger;
            this.Doc = new DslDocument();
            this.Doc.Load(VisioHelpers.GetDSLPath(Logger.Document));
            this.Page = new VisioPage(Logger.Document.Pages[1]);
        }

        private DslDocument Doc;
        private VisioPage Page;

        public void Synchronize()
        {
            Trace.WriteLine("Synchronizing Visio");
            Trace.Indent();

            VisioMaster.count = 0;
            Logger.Active = false;

            RemoveDisconnectedShapes();
            RemoveUnusedShapes();
            SynchronizeClasses();
            SynchronizeRelationships();

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

        private void RemoveUnusedShapes()
        {
            Trace.WriteLine("Removing Unused Shapes");
            Trace.Indent();
            List<VisioShape> removedShapes = new List<VisioShape>();

            foreach (VisioClass vc in Page.Classes)
            {
                if (!Doc.Dsl.Classes.Find(vc.GUID).IsValid)
                {
                    removedShapes.Add(vc);
                }
            }
            foreach (VisioConnector vc in Page.Relationships)
            {
                if (!Doc.Dsl.Relationships.Find(vc.GUID).IsValid)
                {
                    removedShapes.Add(vc);
                }
            }
            foreach (VisioConnector vc in Page.Inheritances)
            {
                DomainClass dc = Doc.Dsl.Classes.Find(new VisioShape(vc.Source).GUID) as DomainClass;
                DomainClass bc = Doc.Dsl.Classes.Find(new VisioShape(vc.Target).GUID) as DomainClass;

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

        private void SynchronizeClasses()
        {
            Trace.WriteLine("Synchronizing Classes");
            Trace.Indent();
            foreach (DomainClass dc in Doc.Dsl.Classes)
            {
                Shape shape = Page.Find(dc.GUID);
                VisioClass vc = new VisioClass(shape == null ? VisioMaster.Drop(Logger.Document, "Class") : shape);
                vc.GUID = dc.GUID;
                vc.Name = dc.Xml.GetAttribute("Name");
                String attrs = "";
                foreach (DomainProperty prop in dc.Properties)
                {
                    attrs += prop.Xml.GetAttribute("Name") + "\n";
                }
                vc.Attributes = attrs.Trim();
            }
            foreach (DomainClass dc in Doc.Dsl.Classes)
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
            Trace.Unindent();
        }

        private void SynchronizeRelationships()
        {
            Trace.WriteLine("Synchronizing Relationships");
            Trace.Indent();
            foreach (DomainRelationship dr in Doc.Dsl.Relationships)
            {
                Shape shape = Page.Find(dr.GUID);
                VisioConnector vc = new VisioConnector(
                    shape == null ? VisioMaster.Drop(Page.Document, (dr.IsEmbedding ? Constants.Composition : Constants.Association)) : shape
                );
                vc.GUID = dr.GUID;
                vc.Name = dr.Xml.GetAttribute("Name");
                vc.SourceText = dr.Source.Xml.GetAttribute("DisplayName");
                vc.TargetText = dr.Target.Xml.GetAttribute("DisplayName");
                vc.SetSourceMultiplicity(dr.Source.Multiplicity);
                vc.SetTargetMultiplicity(dr.Target.Multiplicity);

                shape = Page.Find(Doc.Dsl.Classes[dr.Source.RolePlayer].GUID);
                if (vc.Source != shape) vc.Source = shape;

                shape = Page.Find(Doc.Dsl.Classes[dr.Target.RolePlayer].GUID);
                if (vc.Target != shape) vc.Target = shape;
            }
            Trace.Unindent();
        }
    }
}
