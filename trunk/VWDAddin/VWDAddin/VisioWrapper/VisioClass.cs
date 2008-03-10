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

        /// <summary>���������� ��� ������</summary>
        public String Name
        {
            get { return GetSubshape("class_name").Text; }
            set { GetSubshape("class_name").Text = value; }
        }

        /// <summary>��������� ������� ������������</summary>
        public Shape Generalization
        {
            get { throw new NotImplementedException(); }
        }
    }
}
