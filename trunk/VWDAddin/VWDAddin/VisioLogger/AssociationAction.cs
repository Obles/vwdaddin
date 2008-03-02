using System;
using System.Collections.Generic;
using System.Text;
using ActionTypes = VWDAddin.Constants.ActionTypes;

namespace VWDAddin.VisioLogger
{
    public class AssociationAction : Action
    {
        public AssociationAction(ActionTypes type)
            : base(type)
        {
            // ToDo: add params here
        }
        #region Members
        private string m_guid;
        private string m_mainName;
        private string m_end1Name;
        private string m_end1MP;        //Multiplicity
        private string m_end2Name;
        private string m_end2MP;

        public string GUID
        {
            get { return m_guid; }
            //set { m_guid = value; }
        }
        public string mainName
        {
            get { return m_mainName; }
            //set { m_mainName = value; }
        }
        public string end1Name
        {
            get { return m_end1Name; }
            //set { m_end1Name = value; }
        }
        public string end1MP
        {
            get { return m_end1MP; }
            //set { m_end1MP = value; }
        }
        public string end2Name
        {
            get { return m_end2Name; }
            //set { m_end2Name = value; }
        }
        public string end2MP
        {
            get { return m_end2MP; }
            //set { m_end2MP = value; }
        }
        #endregion
    }
}
