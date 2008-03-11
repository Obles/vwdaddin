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
                        case "association":
                            //GetLogger(shape.Document).Add(new AssociationAdded(new VisioConnector(shape)));
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
                        case "association":
                            GetLogger(shape.Document).Add(new AssociationDeleted(new VisioConnector(shape)));
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
                        case "association":
                            GetLogger(shape.Document).Add(new AssociationNameChanged(new VisioConnector(shape)));
                            break;
                        case "end1_name":
                            GetLogger(shape.Document).Add(new AssociationSourceNameChanged(new VisioConnector(shape)));
                            break;
                        case "end1_mp":
                            GetLogger(shape.Document).Add(new AssociationSourceMPChanged(new VisioConnector(shape)));
                            break;
                        case "end2_name":
                            GetLogger(shape.Document).Add(new AssociationTargetNameChanged(new VisioConnector(shape)));
                            break;
                        case "end2_mp":
                            GetLogger(shape.Document).Add(new AssociationTargetMPChanged(new VisioConnector(shape)));
                            break;
                        default:
                            break;
                    }
                    break;
                }
                case (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd:
                {
                    VisioConnector connector = new VisioConnector((subject as Connects).FromSheet);
                    if ((subject as Connects).ToSheet.Name.Equals(connector.Source.Name))
                    {
                        GetLogger(connector.Shape.Document).Add(new AssociationConnected(connector, Constants.ConnectionTypes.BeginConnected));
                    }
                    else
                    {
                        GetLogger(connector.Shape.Document).Add(new AssociationConnected(connector, Constants.ConnectionTypes.EndConnected));
                    }
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