using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using ActionTypes = VWDAddin.Constants.ActionTypes;

namespace VWDAddin.VisioLogger.Actions
{
    public class AssociationAction : BaseAction
    {
        public AssociationAction(Shape targetShape)
            : base(targetShape)
        {
            // ToDo: add params here
        }
        #region Members
        private string _guid;
        private string _mainName;
        private string _end1Name;
        private string _end1MP;        //Multiplicity
        private string _end2Name;
        private string _end2MP;

        public string GUID
        {
            get { return _guid; }
            //set { m_guid = value; }
        }
        public string MainName
        {
            get { return _mainName; }
            //set { m_mainName = value; }
        }
        public string End1Name
        {
            get { return _end1Name; }
            //set { m_end1Name = value; }
        }
        public string End1MP
        {
            get { return _end1MP; }
            //set { m_end1MP = value; }
        }
        public string End2Name
        {
            get { return _end2Name; }
            //set { m_end2Name = value; }
        }
        public string End2MP
        {
            get { return _end2MP; }
            //set { m_end2MP = value; }
        }
        #endregion
    }
}
