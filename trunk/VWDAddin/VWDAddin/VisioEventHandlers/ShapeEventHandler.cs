using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;
using VWDAddin.VisioLogger.Actions;

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
            Shape shape = subject as Shape;
            switch (eventCode)
            {
                case (short)VisEventCodes.visEvtShape + Constants.visEvtAdd:
                {                    
                    shape.get_Cells("User.GUID.Value").Formula = VisioHelpers.ToString(Guid.NewGuid().ToString());
                    switch (VisioHelpers.GetShapeType(shape))
                    {
                        case "class":
                            GetLogger(shape.Document).Add(new ClassAdded(shape));
                            break;                        
                        default:
                            break;
                    }
                    break;
                }
                case (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtShape:
                {
                    switch (VisioHelpers.GetShapeType(shape))
                    {
                        case "class":
                            GetLogger(shape.Document).Add(new ClassDeleted(shape));
                            break;
                        default:
                            break;
                    }
                    break;
                }
                case (short)VisEventCodes.visEvtCodeShapeExitTextEdit:                    
                {
                    switch (VisioHelpers.GetShapeType(shape))
                    {
                        case "class_name":
                            GetLogger(shape.Document).Add(new ClassNameChanged(shape));
                            break;
                        case "attr_section":
                            GetLogger(shape.Document).Add(new ClassAttributesChanged(shape));
                            break;
                        default:
                            break;
                    }
                    break;
                }
                case (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd:
                {
                    
                    //GetLogger(shape.Document).Add(new ...);
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