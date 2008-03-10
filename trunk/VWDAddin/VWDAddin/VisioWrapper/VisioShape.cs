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
            get { return VisioHelpers.FromString(Shape.get_Cells("User.GUID.Value").Formula); }
            set { Shape.get_Cells("User.GUID.Value").Formula = VisioHelpers.ToString(value); }
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
    }
}
