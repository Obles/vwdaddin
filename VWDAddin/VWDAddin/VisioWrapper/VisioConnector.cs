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

        /// <summary>Физическое имя коннектора</summary>
        public String Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>Получение элемента от которого начинается коннектор</summary>
        public Shape Source
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>Получение элемента в котором заканчивается коннектор</summary>
        public Shape Target
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>Установка элемента от которого начинается коннектор</summary>
        public void SetSource(Shape source, ClassConnections sourcePoint)
        {
            String s = Connections.Create(source, sourcePoint);
            Shape.get_Cells("BeginX").Formula = s;
            Shape.get_Cells("BeginY").Formula = s;
        }

        /// <summary>Установка элемента в котором заканчивается коннектор</summary>
        public void SetTarget(Shape target, ClassConnections targetPoint)
        {
            String s = Connections.Create(target, targetPoint);
            Shape.get_Cells("EndX").Formula = s;
            Shape.get_Cells("EndY").Formula = s;
        }
    }
}
