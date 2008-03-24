using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class VisioConnector : VisioShape
    {
        public VisioConnector(Shape Shape)
            : base(Shape)
        {
        }

        /// <summary>���������� ��� ����������</summary>
        public String Name
        {
            get { return Shape.Text; }
            set { Shape.Text = value; }
        }

        /// <summary>��������� �������� �� �������� ���������� ���������</summary>
        /// *��� ���, ��� ������ - ��� ����������* 
        public Shape Source
        {
            get { return FindConnectedShape(Shape.get_Cells("BegTrigger").Formula); }
            set { SetSource(value, ClassConnections.Undef); }
        }

        /// <summary>��������� �������� � ������� ������������� ���������</summary>
        public Shape Target
        {
            get { return FindConnectedShape(Shape.get_Cells("EndTrigger").Formula); }
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

        /// <summary>��������� �������� �� �������� ���������� ���������</summary>
        public void SetSource(Shape source, ClassConnections sourcePoint)
        {
            String s = Connections.Create(source, sourcePoint);
            Shape.get_Cells("BeginX").Formula = s;
            Shape.get_Cells("BeginY").Formula = s;
        }

        /// <summary>��������� �������� � ������� ������������� ���������</summary>
        public void SetTarget(Shape target, ClassConnections targetPoint)
        {
            String s = Connections.Create(target, targetPoint);
            Shape.get_Cells("EndX").Formula = s;
            Shape.get_Cells("EndY").Formula = s;
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

        /// <summary>������� ������ �������</summary>
        public String SourceText
        {
            get { return GetSubshape("end1_name").Text; }
            set { GetSubshape("end1_name").Text = value; }
        }

        /// <summary>������� ����� �������</summary>
        public String TargetText
        {
            get { return GetSubshape("end2_name").Text; }
            set { GetSubshape("end2_name").Text = value; }
        }

        public StaticConnector ToStaticConnector()
        {
            return new StaticConnector(Shape);
        }
    }
}
