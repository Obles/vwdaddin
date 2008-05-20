using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class VisioShape
    {
        public Shape Shape;

        public VisioShape(Shape Shape)
        {
            this.Shape = Shape;
        }

        public String GUID
        {
            get { return VisioHelpers.FromString(Shape.get_Cells("User.GUID.Value").FormulaU); }
            set { Shape.get_Cells("User.GUID.Value").FormulaU = VisioHelpers.ToString(value); }
        }

        protected Shape GetSubshape(String type)
        {
            foreach (Shape subshape in Shape.Shapes)
            {
                if (VisioHelpers.GetShapeType(subshape) == type)
                    return subshape;
            }
            return null;
        }

        public Shape this[String type]
        {
            get { return GetSubshape(type); }
        }

        public StaticShape ToStaticShape()
        {
            return new StaticShape(Shape);
        }

        public String Type
        {
            get { return VisioHelpers.GetShapeType(Shape); }
        }

        public bool Equals(VisioShape vs)
        {
            return vs.Shape == Shape;
        }
    }
}
