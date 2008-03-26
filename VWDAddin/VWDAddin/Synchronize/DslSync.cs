using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;
using VWDAddin.DslWrapper;
using VWDAddin.VisioWrapper;

namespace VWDAddin.Synchronize
{
    class DslSync
    {
        private Logger Logger;

        public DslSync(Logger Logger)
        {
            this.Logger = Logger;
        }

        public void Synchronize()
        {
            //TODO сделать синхронизацию Dsl с Visio без обратной синхронизации удаления
        }
    }
}
