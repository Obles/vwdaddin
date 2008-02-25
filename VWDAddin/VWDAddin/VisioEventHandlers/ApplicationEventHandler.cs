using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    public class ApplicationEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtDoc + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeQuit,
            (short)VisEventCodes.visEvtCodeWinPageTurn,
        };

        public ApplicationEventHandler(EventManager manager)
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
                case (short)VisEventCodes.visEvtDoc + Constants.visEvtAdd:
                    Document document = subject as Document;
                    if (document.Type == VisDocumentTypes.visTypeDrawing)
                    {
                        Owner.StartDocumentListener(document);
                    }
                    break;
                default:
                    EventHandler.UnhandledEvent(eventCode);
                    break;
            }
            return true;
        }
    }
}
