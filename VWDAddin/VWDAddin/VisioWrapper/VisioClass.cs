using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using Color = System.Drawing.Color;

namespace VWDAddin.VisioWrapper
{
    public class VisioClass : VisioShape
    {
        public VisioClass(Shape Shape)
            : base(Shape)
        {
        }

        
        /// <summary>Логическое имя класса</summary>
        public String DisplayName
        {
            get { return GetSubshape("class_name").Text; }
            set { GetSubshape("class_name").Text = value; }
        }

        /// <summary>Физическое имя класса</summary>
        public String Name
        {
            get 
            {
                String s = VisioHelpers.FromString(Shape.get_Cells("User.RelName.Value").FormulaU);
                if(s == String.Empty)
                {
                    s = Translit.Encode(this.DisplayName);
                    this.Name = s;
                }
                return s;
            }
            set { Shape.get_Cells("User.RelName.Value").FormulaU = Translit.Encode(VisioHelpers.ToString(value)); }
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

        public Color Color
        {
            get
            {
                Regex regex = new Regex(@"RGB\(\s*([0-9]+)\s*,\s*([0-9]+)\s*,\s*([0-9]+)\s*\)");
                Match m = regex.Match(this["class_name"].get_Cells("FillForegnd").FormulaU);
                if (m.Success)
                {
                    int r = int.Parse(m.Groups[1].Value);
                    int g = int.Parse(m.Groups[2].Value);
                    int b = int.Parse(m.Groups[3].Value);
                    return Color.FromArgb(r, g, b);
                }
                else return Color.White;
            }
            set
            {
                this["class_name"].get_Cells("FillForegnd").FormulaU =
                    "RGB(" + value.R + "," + value.G + "," + value.B + ")";
            }
        }
    }
}
