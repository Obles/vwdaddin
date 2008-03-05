using Microsoft.Office.Interop.Visio;
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

        public static string GetShapeType(Shape shape)
        {
            return FromString(shape.get_Cells("user.type").Formula);
        }
    
        public static String ToString(String value)
        {
            if (value == null) value = String.Empty;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
 
        public static String FromString(String value)
        {
            return value.Substring(1, value.Length - 2).Replace("\"\"", "\"");
        }

        public static Cell GetDocumentCell(Document Document, String Property)
        {
            return Document.Pages[1].PageSheet.get_Cells(Property);
        }

        public static String GetDSLPath(Document Document)
        {
            try
            {
                return FromString(GetDocumentCell(Document, "User.DSL.Value").Formula);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static String GetWordPath(Document Document)
        {
            try
            {
                return FromString(GetDocumentCell(Document, "User.Word.Value").Formula);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static void SetDSLPath(Document Document, String Path)
        {
            GetDocumentCell(Document, "User.DSL.Value").Formula = ToString(Path);
        }

        public static void SetWordPath(Document Document, String Path)
        {
            GetDocumentCell(Document, "User.Word.Value").Formula = ToString(Path);
        }

        public static String GetTempDSLPath(Document Document)
        {
            return GetDSLPath(Document) + "~";
        }
    }
}