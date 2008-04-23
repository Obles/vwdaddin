using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class StaticConnector : VisioConnector
    {
        public StaticConnector(Shape Shape)
            : base(Shape)
        {
            this.GUID = base.GUID;
            this.Type = base.Type;
            this.Name = base.Name;
            this.DisplayName = base.DisplayName;
            this.Source = base.Source == null ? null : new StaticClass(base.Source);
            this.Target = base.Target == null ? null : new StaticClass(base.Target);
            this.SourceMultiplicity = base.SourceMultiplicity;
            this.TargetMultiplicity = base.TargetMultiplicity;
            this.SourceText = base.SourceText;
            this.TargetText = base.TargetText;
        }

        new public String GUID;
        new public String Type;

        /// <summary>Физическое имя коннектора</summary>
        new public String Name;

        /// <summary>Логическое имя коннектора</summary>
        new public String DisplayName;

        /// <summary>Получение элемента от которого начинается коннектор</summary>
        /// *Это там, где ромбик - для композиции* 
        new public StaticClass Source;

        /// <summary>Получение элемента в котором заканчивается коннектор</summary>
        new public StaticClass Target;

        new public String SourceMultiplicity;
        new public String TargetMultiplicity;

        /// <summary>Подпись начала стрелки</summary>
        new public String SourceText;

        /// <summary>Подпись конца стрелки</summary>
        new public String TargetText;
    }
}
