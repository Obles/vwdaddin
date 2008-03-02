using System;
using System.Collections.Generic;
using System.Text;
using EventTypes = VWDAddin.VisioDefinitions.VISIO_EVENT_TYPES;
using VisioDocument = Microsoft.Office.Interop.Visio.Document;

namespace VWDAddin.VisioLogger
{
    public class VisioLogger
    {
        public VisioDocument associatedDocument;
        public List<BaseEvent> eventList;
    }
}
