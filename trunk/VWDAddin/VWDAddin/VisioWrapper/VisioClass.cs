using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class VisioClass : VisioShape
    {
        public VisioClass(Shape Shape)
            : base(Shape)
        {
        }

        /// <summary>Физическое имя класса</summary>
        public String Name
        {
            get { return GetSubshape("class_name").Text; }
            set { GetSubshape("class_name").Text = value; }
        }

        public String Attributes
        {
            get { return Shape.Shapes[2].Text; }
            set { Shape.Shapes[2].Text = value; }
        }

        /// <summary>Исходящая стрелка наследования</summary>
        public Shape Generalization
        {
            get 
            {
                VisioPage page = new VisioPage(Shape.Document.Pages[1]);
                foreach (VisioConnector vc in page.Inheritances)
                {
                    if (vc.Source.ID == Shape.ID) 
                        return vc.Shape;
                }
                return null;
            }
        }

        public StaticClass ToStaticClass()
        {
            return new StaticClass(Shape);
        }
    }
}
