using System;
using System.Collections.Generic;
using System.Text;
using ActionTypes = VWDAddin.Constants.ActionTypes;

namespace VWDAddin.VisioLogger
{
    public class Action
    {
        protected ActionTypes Type;

        public Action(ActionTypes Type)
        {
            this.Type = Type;
        }
    }
}
