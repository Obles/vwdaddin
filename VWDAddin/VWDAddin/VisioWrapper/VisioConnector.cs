using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    class VisioConnector : VisioShape
    {
        public VisioConnector(Shape Shape)
            : base(Shape)
        {
        }

        /// <summary>���������� ��� ����������</summary>
        public String Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>��������� �������� �� �������� ���������� ���������</summary>
        public Shape Source
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>��������� �������� � ������� ������������� ���������</summary>
        public Shape Target
        {
            get { throw new NotImplementedException(); }
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
    }
}
