using Application = Microsoft.Office.Interop.Visio.Application;
using Shape = Microsoft.Office.Interop.Visio.Shape;
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
          (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker,
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
                case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker:
                    Application application = (Application)subject;
                    int id = Convert.ToInt32(application.get_EventInfo(0));
                    Shape selectedShape = VisioHelpers.GetShapeByID(id, application);
                    if (selectedShape != null )
                    {                        
                        string type = VisioHelpers.GetShapeType(selectedShape);
                        if (type.Equals("association"))
                        {
                            AssociationDisplayOptions dlg = new AssociationDisplayOptions(selectedShape);
                            dlg.Show();                        
                        }
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
