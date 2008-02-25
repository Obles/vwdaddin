using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;

namespace VWDAddin
{
    public class DocumentEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtShape,
            (short)VisEventCodes.visEvtPage + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtShape + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtDoc,
        };

        public DocumentEventHandler(EventManager manager)
            : base(manager, HandleEvents)
        {
        }

        public override object VisEventProc(
            short eventCode,
            object source,
            int eventId,
            int eventSequenceNumber,
            object subject,
            object moreInformation)
        {
            switch (eventCode)
            {
                default:
                    EventHandler.UnhandledEvent(eventCode);
                    break;
            }
            return true;
        }
    }
}