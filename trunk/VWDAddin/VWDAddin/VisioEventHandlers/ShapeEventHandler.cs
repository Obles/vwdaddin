using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;

namespace VWDAddin
{
    public class ShapeEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtShape,
            (short)VisEventCodes.visEvtShape + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtCodeShapeExitTextEdit,
            (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd,
        };

        public ShapeEventHandler(EventManager manager)
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
                case (short)VisEventCodes.visEvtShape + Constants.visEvtAdd:
                {
                    Shape shape = subject as Shape;
                    shape.get_Cells("User.GUID.Value").Formula = VisioHelpers.ToString(Guid.NewGuid().ToString());

                    GetLogger(shape.Document).Add(new Action(Constants.ActionTypes.ClassAdded));
                    break;
                }
                case (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtShape:
                case (short)VisEventCodes.visEvtCodeShapeExitTextEdit:
                case (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd:
                {
                    Shape shape = subject as Shape;
                    GetLogger(shape.Document).Add(new Action(Constants.ActionTypes.ClassAdded));
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