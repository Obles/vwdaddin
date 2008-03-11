using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;
using VWDAddin.VisioLogger.Actions;
using VWDAddin.VisioWrapper;

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
                    VisioShape vs = new VisioShape(shape);
                    if(vs.GUID == String.Empty) vs.GUID = Guid.NewGuid().ToString();

                    switch (VisioHelpers.GetShapeType(shape))
                    {
                        case "class":
                            GetLogger(shape.Document).Add(new ClassAdded(new VisioClass(shape)));
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
                            GetLogger(shape.Document).Add(new ClassDeleted(new VisioClass(shape)));
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
                            GetLogger(shape.Document).Add(new ClassNameChanged(new VisioClass(shape.Parent as Shape)));
                            break;
                        case "attr_section":
                            GetLogger(shape.Document).Add(new ClassAttributesChanged(new VisioClass(shape.Parent as Shape)));
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