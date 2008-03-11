using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using ActionTypes = VWDAddin.Constants.ActionTypes;

namespace VWDAddin.VisioLogger.Actions
{
    public class BaseAction
    {
        virtual public void Apply(Logger Logger) { }
    }
}
