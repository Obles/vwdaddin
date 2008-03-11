using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;

namespace VWDAddin.VisioLogger
{
    public class LoggerManager
    {
        private Dictionary<Document, Logger> Loggers = new Dictionary<Document, Logger>();

        public Logger CreateLogger(Document Document)
        {
            Logger Logger = new Logger(this, Document);
            Loggers.Add(Document, Logger);
            return Logger;
        }

        public Logger GetLogger(Document Document)
        {
            return Loggers[Document];
        }

        public void RemoveLogger(Document Document)
        {
            Loggers[Document].Cleanup();
            Loggers.Remove(Document);
        }

        public Logger ResetLogger(Document Document)
        {
            RemoveLogger(Document);
            return CreateLogger(Document);
        }
    }
}
