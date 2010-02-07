using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    public class DocumentEventHandler : VisioAppEventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtPage + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtCodeDocOpen,
            (short)VisEventCodes.visEvtCodeDocSave,
            (short)VisEventCodes.visEvtCodeDocSaveAs,
            (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtDoc,
            (short)VisEventCodes.visEvtCodeDocRunning,
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
                    Debug.WriteLine("Сохранение документа... Номер события: " + eventSequenceNumber.ToString());
                    GetLogger(subject as Document).ApplyChanges();
                    GetLogger(subject as Document).WordDocument.Syncronize(subject as Document, VisioHelpers.GetWordPath(subject as Document));
                    break;
                }
                case (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtDoc:
                {
                    //GetLogger(subject as Document).ApplyChanges();
                    RemoveLogger(subject as Document);
                    break;
                }
                case (short)VisEventCodes.visEvtCodeDocRunning:
                {
                    GetLogger(subject as Document).Active = true;
                    Trace.Unindent();
                    Trace.WriteLine("Document Listener Ready");
                    break;
                }
                default:
                    VisioAppEventHandler.UnhandledEvent(eventCode);
                    break;
            }
            return true;
        }
    }
}