using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioWrapper
{
    public class VisioConnector : VisioShape
    {
        public VisioConnector(Shape Shape)
            : base(Shape)
        {
        }

        /// <summary>Логическое имя коннектора</summary>
        public String DisplayName
        {
            get { return Shape.Text; }
            set { Shape.Text = value; }
        }

        /// <summary>Физическое имя коннектора</summary>
        public String Name
        {
            get
            {
                String s = VisioHelpers.FromString(Shape.get_Cells("User.RelName.Value").FormulaU);
                if (s == String.Empty)
                {
                    s = Translit.Encode(this.DisplayName);
                    this.Name = s;
                }
                return s;
            }
            set { Shape.get_Cells("User.RelName.Value").FormulaU = VisioHelpers.ToString(value); }
        }

        /// <summary>Получение элемента от которого начинается коннектор</summary>
        /// *Это там, где ромбик - для композиции* 
        public Shape Source
        {
            get { return FindConnectedShape(Shape.get_Cells("BegTrigger").FormulaU); }
            set { SetSource(value, ClassConnections.Undef); }
        }

        /// <summary>Получение элемента в котором заканчивается коннектор</summary>
        public Shape Target
        {
            get { return FindConnectedShape(Shape.get_Cells("EndTrigger").FormulaU); }
            set { SetTarget(value, ClassConnections.Undef); }
        }

        public Shape FindConnectedShape(string connectionString)
        {
            string searchName = VisioHelpers.GetConnectedClassName(connectionString);
            foreach (Shape suspiciousShape in Shape.Document.Pages[1].Shapes)
            {
                if (suspiciousShape.Name == searchName)
                {
                    return VisioHelpers.GetShapeType(suspiciousShape) 
                        == Constants.Class ? suspiciousShape : null;
                }
            }
            return null;
        }

        /// <summary>Установка элемента от которого начинается коннектор</summary>
        public void SetSource(Shape source, ClassConnections sourcePoint)
        {
            String s = Connections.Create(source, sourcePoint);
            Shape.get_Cells("BeginX").FormulaU = s;
            Shape.get_Cells("BeginY").FormulaU = s;
            Shape.get_Cells("BegTrigger").FormulaU = Connections.CreateTrigger(source);
        }

        /// <summary>Установка элемента в котором заканчивается коннектор</summary>
        public void SetTarget(Shape target, ClassConnections targetPoint)
        {
            String s = Connections.Create(target, targetPoint);
            Shape.get_Cells("EndX").FormulaU = s;
            Shape.get_Cells("EndY").FormulaU = s;
            Shape.get_Cells("EndTrigger").FormulaU = Connections.CreateTrigger(target);
        }

        public String SourceMultiplicity
        {
            get { return GetSubshape("end1_mp").Text; }
            set { GetSubshape("end1_mp").Text = value; }
        }

        public String TargetMultiplicity
        {
            get { return GetSubshape("end2_mp").Text; }
            set { GetSubshape("end2_mp").Text = value; }
        }

        /// <summary>Подпись начала стрелки</summary>
        public String SourceText
        {
            get { return GetSubshape("end1_name").Text; }
            set { GetSubshape("end1_name").Text = value; }
        }

        /// <summary>Подпись конца стрелки</summary>
        public String TargetText
        {
            get { return GetSubshape("end2_name").Text; }
            set { GetSubshape("end2_name").Text = value; }
        }

        public StaticConnector ToStaticConnector()
        {
            return new StaticConnector(Shape);
        }

        public bool IsComposition
        {
            get { return this.Type == Constants.Composition; }
        }

        public void SetSourceMultiplicity(Multiplicity multiplicity)
        {
            if (!MultiplicityHelper.Equals(SourceMultiplicity, multiplicity))
            {
                SourceMultiplicity = MultiplicityHelper.AsDigits(multiplicity);
            }
        }

        public void SetTargetMultiplicity(Multiplicity multiplicity)
        {
            if (!MultiplicityHelper.Equals(TargetMultiplicity, multiplicity))
            {
                TargetMultiplicity = MultiplicityHelper.AsDigits(multiplicity);
            }
        }
    }
}
