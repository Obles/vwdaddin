using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using ActionTypes = VWDAddin.Constants.ActionTypes;

namespace VWDAddin.VisioLogger.Actions
{
    public class ClassAction : BaseAction
    {
        public ClassAction(Shape targetShape)
            : base(targetShape)
        {
            VisioHelpers.ParseClassShape(targetShape, out _guid, out _className, out _attributes);
        }

        #region Members
        private string _guid;
        private string _className;
        private string _attributes;

        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }        
        #endregion
    }
}
