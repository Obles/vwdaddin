using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class VisioPage
    {
        private Page Page;

        public VisioPage(Page Page)
        {
            this.Page = Page;
            this.classes = new VisioList<VisioClass>(
                Page.Shapes, 
                delegate(Shape shape) 
                {
                    return VisioHelpers.GetShapeType(shape) == Constants.Class;
                }
            );
            this.relationships = new VisioList<VisioConnector>(
                Page.Shapes,
                delegate(Shape shape)
                {
                    String type = VisioHelpers.GetShapeType(shape);
                    return type == Constants.Association 
                        || type == Constants.Composition;
                }
            );
            this.inheritances = new VisioList<VisioConnector>(
                Page.Shapes,
                delegate(Shape shape)
                {
                    return VisioHelpers.GetShapeType(shape) == Constants.Generalization;
                }
            );
            this.connectors = new VisioList<VisioConnector>(
                Page.Shapes,
                delegate(Shape shape)
                {
                    String type = VisioHelpers.GetShapeType(shape);
                    return type == Constants.Association
                        || type == Constants.Composition
                        || type == Constants.Generalization;
                }
            );
        }

        public Shape Find(String Guid)
        {
            foreach (Shape shape in Page.Shapes)
            {
                VisioShape Shape = new VisioShape(shape);
                if (Shape.GUID == Guid) return shape;
            }
            return null;
        }

        private VisioList<VisioClass> classes;
        public VisioList<VisioClass> Classes
        {
            get { return classes; }
        }

        private VisioList<VisioConnector> relationships;
        public VisioList<VisioConnector> Relationships
        {
            get { return relationships; }
        }

        private VisioList<VisioConnector> inheritances;
        public VisioList<VisioConnector> Inheritances
        {
            get { return inheritances; }
        }

        private VisioList<VisioConnector> connectors;
        public VisioList<VisioConnector> Connectors
        {
            get { return connectors; }
        }

        public Shapes Shapes
        {
            get { return Page.Shapes; }
        }

        public Document Document
        {
            get { return Page.Document; }
        }
    }
}
