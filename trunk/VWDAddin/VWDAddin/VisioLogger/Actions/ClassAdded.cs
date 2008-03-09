using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Visio;
using System.Text;

namespace VWDAddin.VisioLogger.Actions
{
    public class ClassAdded : ClassAction
    {
        public ClassAdded(Shape targetShape, WordDocument wordDocument)
            :base(targetShape)
        {
            WordDocument = wordDocument;
        }

        override public void Apply(Document document) 
        {
            WordDocument.AddClass(ClassName, Attributes, GUID);
        }
        
        private WordDocument _wordDocument;
        public WordDocument WordDocument
        {
            get { return _wordDocument; }
            set { _wordDocument = value; }
        }
    }
}
