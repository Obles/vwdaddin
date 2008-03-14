using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using Form = System.Windows.Forms.Form;
using DialogResult = System.Windows.Forms.DialogResult;

namespace VWDAddin
{
    public class MarkerEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker,
        };

        public MarkerEventHandler(EventManager manager)
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
            if (eventCode == (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker)
            {
                Application application = subject as Application;
                String[] Params = application.get_EventInfo(0).Split(':');
                switch (Params[0])
                {
                    case AssociationDisplayOptions.MarkerName:
                    {
                        int id = Convert.ToInt32(Params[1]);
                        Shape selectedShape = VisioHelpers.GetShapeByID(id, application);
                        if (selectedShape != null)
                        {
                            string type = VisioHelpers.GetShapeType(selectedShape);
                            if (type.Equals("association"))
                            {
                                Show(new AssociationDisplayOptions(selectedShape), application);
                            }
                        }
                        break;
                    }
                    case DocumentProperties.MarkerName:
                    {
                        Show(new DocumentProperties(GetLogger(application.ActiveDocument)), application);
                        break;
                    }
                    default:
                        Trace.WriteLine("Undefined function " + Params[0]);
                        break;
                }
            }
            else EventHandler.UnhandledEvent(eventCode);
            return true;
        }

        private static void Show(Form Form, Application App)
        {
            App.EndUndoScope(
                App.BeginUndoScope("Show Dialog"),
                Form.ShowDialog() == DialogResult.OK
            );
        }
    }
}
