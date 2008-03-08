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
            VisioHelpers.ParseClassShape(targetShape, out m_guid, out m_className, out m_attributes);
        }

        #region Members
        private string m_guid;
        private string m_className;
        private string m_attributes;

        public string GUID
        {
            get { return m_guid; }
            set { m_guid = value; }
        }
        public string className
        {
            get { return m_className; }
            set { m_className = value; }
        }
        public string attributes
        {
            get { return m_attributes; }
            set { m_attributes = value; }
        }        
        #endregion
    }
}
