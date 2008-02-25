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
                foreach (Shape shape in application.ActivePage.Shapes)
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
<<<<<<< .mine

        public static string GetShapeType(Shape shape)
        {
            string cellFormula = shape.get_Cells("user.type").Formula;
            return cellFormula.Substring(1, cellFormula.Length - 2);
        }
    
=======

        public static String ToString(String value)
        {
            if (value == null) value = String.Empty;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
>>>>>>> .r18
    }
}
