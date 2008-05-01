using Application = Microsoft.Office.Interop.Visio.Application;
using Shape = Microsoft.Office.Interop.Visio.Shape;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.DslWrapper;
using VWDAddin.VisioLogger;
using VWDAddin.Synchronize;
using System.Windows.Forms;
using System.Threading;

namespace VWDAddin
{
    public class ApplicationEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
          (short)VisEventCodes.visEvtDoc + Constants.visEvtAdd,
          (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeQuit,
          (short)VisEventCodes.visEvtCodeWinPageTurn,
          (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppActivate, 
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
                case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppActivate:
                    Application app = subject as Application;
                    foreach (Document doc in app.Documents)
                    {
                        VerifyModified(GetLogger(doc));
                    }
                    break;
                default:
                    EventHandler.UnhandledEvent(eventCode);
                    break;
            }
            return true;
        }

        private static void VerifyModified(Logger Logger)
        {
            if (DslCompare.IsModified(Logger))
            {
                if (MessageBox.Show("Связанный Dsl-документ был изменен извне. Принять изменения?",
                    Logger.Document.Title,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    new Thread(new ThreadStart(
                        delegate { new VisioSync(Logger).Synchronize(); }
                    )).Start();
                }
                else
                {
                    Logger.ApplyChanges();
                }
            }
        }
    }
}
