using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;
using VWDAddin.VisioLogger.Actions.Associations;
using VWDAddin.VisioLogger.Actions;
using VWDAddin.VisioWrapper;

namespace VWDAddin
{
    public class ShapeEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtShape,
            (short)VisEventCodes.visEvtShape + Constants.visEvtAdd,
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
            if (eventCode == (short)VisEventCodes.visEvtDel + (short)VisEventCodes.visEvtShape)
            {
                Shape shape = subject as Shape;
                switch (VisioHelpers.GetShapeType(shape))
                {
                    case Constants.Class:
                        GetLogger(shape.Document).Add(new ClassDeleted(new VisioClass(shape)));
                        break;
                    case Constants.Association:
                        GetLogger(shape.Document).Add(new AssociationDeleted(new VisioConnector(shape)));
                        break;
                    case Constants.Composition:
                        GetLogger(shape.Document).Add(new CompositionDeleted(new VisioConnector(shape)));
                        break;
                    case Constants.Generalization:
                        GetLogger(shape.Document).Add(new GeneralizationDeleted(new VisioConnector(shape)));
                        break;
                    default:
                        EventHandler.UnhandledEvent(eventCode);
                        break;
                }
            }
            else if (eventCode == (short)VisEventCodes.visEvtShape + Constants.visEvtAdd)
            {
                VisioShape vs = new VisioShape(subject as Shape);
                if (vs.GUID == String.Empty) vs.GUID = Guid.NewGuid().ToString();
            }
            else EventHandler.UnhandledEvent(eventCode);
            return true;
        }
    }
}