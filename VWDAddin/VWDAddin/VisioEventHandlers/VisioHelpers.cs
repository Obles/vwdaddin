using Application = Microsoft.Office.Interop.Visio.Application;
using Shape= Microsoft.Office.Interop.Visio.Shape;
using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin
{
    class VisioHelpers
    {
        public static Shape GetShapeByID(int ID, Application application)
        {
            try 
            {
                foreach (Shape shape in application.Documents[1].Pages[1].Shapes)
                {
                    if (shape.ID == ID)
                        return shape;
                }
            }
            catch(Exception)
            {
                int abc =0 ;
            }
            return null;
        }
    
    }
}
