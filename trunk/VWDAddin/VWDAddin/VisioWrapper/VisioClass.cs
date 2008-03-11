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
            get { return null; /*throw new NotImplementedException();*/ }
        }

        public StaticClass ToStaticClass()
        {
            return new StaticClass(Shape);
        }
    }
}
