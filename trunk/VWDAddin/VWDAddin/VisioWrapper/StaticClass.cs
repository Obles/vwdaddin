using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class StaticClass : VisioClass
    {
        public StaticClass(Shape Shape)
            : base(Shape)
        {
            this.GUID = base.GUID;
            this.Type = base.Type;
            this.Name = base.Name;
            this.Attributes = base.Attributes;
            this.Generalization = base.Generalization == null ? -1 : base.Generalization.ID;
        }

        new public String GUID;
        new public String Type;

        /// <summary>Физическое имя класса</summary>
        new public String Name;

        new public String Attributes;

        /// <summary>Исходящая стрелка наследования</summary>
        new public int Generalization;
    }
}
