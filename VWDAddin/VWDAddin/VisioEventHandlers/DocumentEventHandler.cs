using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    public class DocumentEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtPage + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtCodeDocOpen,
            (short)VisEventCodes.visEvtCodeDocSave,
            (short)VisEventCodes.visEvtCodeDocSaveAs,
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
                case (short)VisEventCodes.visEvtCodeDocSave:
                case (short)VisEventCodes.visEvtCodeDocSaveAs:
                {
                    GetLogger(subject as Document).ApplyChanges();
                    break;
                }
                case (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtDoc:
                {
                    GetLogger(subject as Document).ApplyChanges();
                    //TODO чистить за собой
                    break;
                }
                default:
                    EventHandler.UnhandledEvent(eventCode);
                    break;
            }
            return true;
        }
    }
}