using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    /// <summary>Номера точек соединения у классов</summary>
    public enum ClassConnections
    {
        Undef = 1,
        LeftBottom = 1,
        Left,
        LeftTop,
        RightBottom,
        Right,
        RightTop,
        BottomLeft,
        Bottom,
        BottomRight,
        TopLeft,
        Top,
        TopRight,
    }

    public class Connections
    {
        public static String Create(Shape shape, ClassConnections con)
        {
            return "PAR(PNT(" + shape.Name + "!Connections.X" + (int)con + "," 
                + shape.Name + "!Connections.Y" + (int)con +"))";
        }        
    }
}
