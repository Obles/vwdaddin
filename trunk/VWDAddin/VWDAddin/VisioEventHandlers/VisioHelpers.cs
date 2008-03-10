using Microsoft.Office.Interop.Visio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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
            catch(Exception e)
            {
                Debug.WriteLine(e.Message /* + "Possible cause: Unknown" */);
            }
            return null;
        }

        public static Shape GetShapeByGUID(String Guid, Document Document)
        {
            try
            {
                Guid = ToString(Guid);
                foreach (Shape shape in Document.Pages[1].Shapes)
                {
                    if (shape.get_Cells("User.GUID.Value").Formula == Guid)
                        return shape;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public static String GetShapeType(Shape shape)
        {
            return FromString(shape.get_Cells("user.type").Formula);
        }

        public static String GetShapeCell(Shape shape, String cellName)
        {
            return FromString(shape.get_Cells(cellName).Formula);
        }

        public static void ParseClassShape(Shape shape, out String guid, out String className, out String attributes)
        {
            try
            {
                switch (GetShapeType(shape))
                {
                    case "class":
                        guid = GetShapeCell(shape, "user.guid.value");
                        className = shape.Shapes[1].Text;
                        attributes = shape.Shapes[2].Text;
                        break;
                    case "class_name":
                    case "attr_section":
                        guid = GetShapeCell(shape.Parent as Shape, "user.guid.value");
                        className = (shape.Parent as Shape).Shapes[1].Text;
                        attributes = (shape.Parent as Shape).Shapes[2].Text;
                        break;
                    default:
                        guid = className = attributes = String.Empty;
                        break;
                }
            }
            catch (Exception e)
            {
                guid = className = attributes = String.Empty;
                Debug.WriteLine(e.Message /* + "Possible cause: Unknown" */);
            }
        }
    
        public static String ToString(String value)
        {
            if (value == null) value = String.Empty;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
 
        public static String FromString(String value)
        {
            //if (value.Length < 2) return string.Empty;
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