using System;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.Collections.Generic;
using VWDAddin.VisioLogger;
using VWDAddin.DslWrapper;

namespace VWDAddin
{
    public class EventManager
    {
        /// <summary>The EventSink class will sink the events added in this
        /// class. It will write event information to the debug output window.
        /// </summary>
        private EventSink eventHandler;

        private Application visioApplication;
        public Application Application
        {
            get { return visioApplication; }
        }

        private LoggerManager loggerManager = new LoggerManager();
        public LoggerManager LoggerManager
        {
            get { return loggerManager; }
        }

        public EventManager()
        {
            eventHandler = new EventSink(this, new EventHandler[] {
                new ApplicationEventHandler(this),
                new MarkerEventHandler(this),
                new DocumentEventHandler(this),
                new ShapeEventHandler(this),
            });
            if (Constants.TraceAnyEvent)
            {
                eventHandler.Add(new AnyEventHandler(this));
            }
        }

        public void FillEventList(EventList EventList, IEnumerable<short> Events)
        {
            short t = 0;
            try
            {
                foreach (short eventCode in Events)
                {
                    t = eventCode;
                    EventList.AddAdvise(eventCode, eventHandler, "", "");
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(EventHandler.GetDescription(t));
                Debug.WriteLine(err.Message);
            }
        }

        public void StartApplicationListener(Application theApplication)
        {
            Trace.WriteLine("Start Application Listener for " + theApplication.Name + " " + theApplication.Version);
            visioApplication = theApplication;

            FillEventList(theApplication.EventList, ApplicationEventHandler.HandleEvents);
            FillEventList(theApplication.EventList, MarkerEventHandler.HandleEvents);

            if (Constants.TraceAnyEvent)
            {
                FillEventList(theApplication.EventList, AnyEventHandler.HandleEvents);
            }
        }

        public void StartDocumentListener(Document theDocument)
        {
            Trace.WriteLine("Start Document Listener for " + theDocument.Name);
            Trace.Indent();

            FillEventList(theDocument.EventList, DocumentEventHandler.HandleEvents);
            FillEventList(theDocument.EventList, ShapeEventHandler.HandleEvents);

            DslCompare.ApplyChanges(theDocument);
            LoggerManager.CreateLogger(theDocument).Active = false;

            if (Constants.TraceAnyEvent)
            {
                FillEventList(theDocument.EventList, AnyEventHandler.HandleEvents);
            }
        }
    }
}