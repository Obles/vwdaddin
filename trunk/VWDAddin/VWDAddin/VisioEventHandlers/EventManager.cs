using System;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.Collections.Generic;

namespace VWDAddin
{
    public class EventManager
    {
        /// <summary>The EventSink class will sink the events added in this
        /// class. It will write event information to the debug output window.
        /// </summary>
        private EventSink eventHandler;

        public EventManager()
        {
            eventHandler = new EventSink(this, new EventHandler[] {
                new ApplicationEventHandler(this),
                new DocumentEventHandler(this),
            });
        }

        public void FillEventList(EventList EventList, IEnumerable<short> Events)
        {
            try
            {
                foreach (short eventCode in Events)
                {
                    EventList.AddAdvise(eventCode, eventHandler, "", "");
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public void StartApplicationListener(Application theApplication)
        {
            Trace.WriteLine("Start Application Listener for " + theApplication.Name + " " + theApplication.Version);
            FillEventList(theApplication.EventList, ApplicationEventHandler.HandleEvents);
        }

        public void StartDocumentListener(Document theDocument)
        {
            Trace.WriteLine("Start Document Listener for " + theDocument.Name);
            FillEventList(theDocument.EventList, DocumentEventHandler.HandleEvents);
        }
    }
}