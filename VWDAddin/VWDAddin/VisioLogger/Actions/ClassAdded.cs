using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Visio;
using System.Text;

namespace VWDAddin.VisioLogger.Actions
{
    public class ClassAdded : ClassAction
    {
        public ClassAdded(Shape targetShape)
            :base(targetShape)
        {
            //WordDocument = wordDocument;
        }

        override public void Apply(Document document, WordDocument wordDocument) 
        {
            wordDocument.AddClass(ClassName, Attributes, GUID);
        }
        
        //private WordDocument _wordDocument;
        //public WordDocument WordDocument
        //{
        //    get { return _wordDocument; }
        //    set { _wordDocument = value; }
        //}
    }
}
