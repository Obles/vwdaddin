using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class StaticShape : VisioShape
    {
        public StaticShape(Shape Shape)
            : base(Shape)
        {
            this.GUID = base.GUID;
            this.Type = base.Type;
        }

        new public String GUID;
        new public String Type;
    }
}
