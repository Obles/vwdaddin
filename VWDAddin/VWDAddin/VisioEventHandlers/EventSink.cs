using System;
using Microsoft.Office.Interop.Visio;
using System.Collections.Generic;

namespace VWDAddin
{

    /// <summary>This class is an event sink for Visio events. It handles event
    /// notification by implementing the IVisEventProc interface, which is
    /// defined in the Visio type library. In order to be notified of events,
    /// an instance of this class must be passed as the eventSink argument in
    /// calls to the EventManager method.</summary>
    public class EventSink : IVisEventProc
    {
        private EventManager owner;
        public EventManager Owner
        {
            get { return owner; }
        }

        private List<VisioAppEventHandler> EventHandlers;
        public EventSink(EventManager owner, IEnumerable<VisioAppEventHandler> handlers)
        {
            this.owner = owner;
            this.EventHandlers = new List<VisioAppEventHandler>(handlers);
        }

        public void Add(VisioAppEventHandler EventHandler)
        {
            EventHandlers.Add(EventHandler);
        }

        /// <summary>This method is called by Visio when an event in the
        /// EventList collection has been triggered. This method is an
        /// implementation of IVisEventProc.VisEventProc method.</summary>
        /// <param name="eventCode">Event code of the event that fired</param>
        /// <param name="source">Reference to source of the event</param>
        /// <param name="eventId">Unique identifier of the event object that 
        /// raised the event</param>
        /// <param name="eventSequenceNumber">Relative position of the event in 
        /// the event list</param>
        /// <param name="subject">Reference to the subject of the event</param>
        /// <param name="moreInformation">Additional information for the event
        /// </param>
        /// <returns>False to allow a QueryCancel operation or True to cancel 
        /// a QueryCancel operation. The return value is ignored by Visio unless 
        /// the event is a QueryCancel event.</returns>
        /// <seealso cref="Microsoft.Office.Interop.Visio.IVisEventProc"></seealso>
        public object VisEventProc(
            short eventCode,
            object source,
            int eventId,
            int eventSequenceNumber,
            object subject,
            object moreInformation)
        {
            object returnValue = true;

            #region Debug information
            if (eventCode != (short)VisEventCodes.visEvtFormula + (short)VisEventCodes.visEvtMod)
            {
                ShowDebugInfo(eventCode, source, eventId, eventSequenceNumber, subject, moreInformation);
            }
            #endregion

            if (Owner.Application.IsUndoingOrRedoing)
            {
                System.Diagnostics.Trace.WriteLine("Is Undoing or Redoing");
                return returnValue;
            }

            foreach (VisioAppEventHandler EventHandler in EventHandlers)
            {
                if (EventHandler.HandlesEvent(eventCode, eventSequenceNumber))
                {
                    returnValue = EventHandler.VisEventProc(
                        eventCode,
                        source,
                        eventId,
                        eventSequenceNumber,
                        subject,
                        moreInformation
                    );
                    break;
                }
            }
            return returnValue;
        }

        public void ShowDebugInfo(
            short eventCode,
            object source,
            int eventId,
            int eventSequenceNumber,
            object subject,
            object moreInformation)
        {
            string tab = "\t";
            string message = "";
            string name = "";
            string eventInformation = "";

            Application subjectApplication = null;
            Document subjectDocument = null;
            Page subjectPage = null;
            Master subjectMaster = null;
            Selection subjectSelection = null;
            Shape subjectShape = null;
            Cell subjectCell = null;
            Connects subjectConnects = null;
            Style subjectStyle = null;
            Window subjectWindow = null;

            //try
            {
                switch (eventCode)
                {

                    // Document event codes
                    case (short)VisEventCodes.visEvtDoc + (short)VisEventCodes.visEvtDel:
                    case (short)VisEventCodes.visEvtCodeBefDocSave:
                    case (short)VisEventCodes.visEvtCodeBefDocSaveAs:
                    case (short)VisEventCodes.visEvtCodeDocDesign:
                    case (short)VisEventCodes.visEvtDoc + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtDoc + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtCodeCancelDocClose:
                    case (short)VisEventCodes.visEvtCodeDocCreate:
                    case (short)VisEventCodes.visEvtCodeDocOpen:
                    case (short)VisEventCodes.visEvtCodeDocSave:
                    case (short)VisEventCodes.visEvtCodeDocSaveAs:
                    case (short)VisEventCodes.visEvtCodeDocRunning:
                    case (short)VisEventCodes.visEvtCodeQueryCancelDocClose:

                        // Subject object is a Document
                        //   Eventinfo may indicate what changed, e.g. 
                        //   /pagereordered, etc. For the save, saveas events 
                        //   the eventinfo is typically empty. However, starting
                        //   with Visio 2000 SR1 it is the name of the recover 
                        //   file if save occured for autorecovery.  
                        //   In general expect non-empty eventinfo only for SaveAs.
                        subjectDocument =
                            (Document)subject;
                        subjectApplication = subjectDocument.Application;
                        name = subjectDocument.Name;
                        break;

                    // Page event codes
                    case (short)VisEventCodes.visEvtPage + (short)VisEventCodes.visEvtDel:
                    case (short)VisEventCodes.visEvtPage + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtPage + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtCodeCancelPageDel:
                    case (short)VisEventCodes.visEvtCodeQueryCancelPageDel:

                        // Subject object is a Page
                        subjectPage = (Page)subject;
                        subjectApplication = subjectPage.Application;
                        name = subjectPage.Name;
                        break;

                    // Master event codes
                    case (short)VisEventCodes.visEvtMaster + (short)VisEventCodes.visEvtDel:
                    case (short)VisEventCodes.visEvtMaster + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtCodeCancelMasterDel:
                    case (short)VisEventCodes.visEvtMaster + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtCodeQueryCancelMasterDel:

                        // Subject object is a Master
                        subjectMaster = (Master)subject;
                        subjectApplication = subjectMaster.Application;
                        name = subjectMaster.Name;
                        break;

                    // Selection event codes
                    case (short)VisEventCodes.visEvtCodeBefSelDel:
                    case (short)VisEventCodes.visEvtCodeSelAdded:
                    case (short)VisEventCodes.visEvtCodeCancelSelDel:
                    case (short)VisEventCodes.visEvtCodeCancelConvertToGroup:
                    case (short)VisEventCodes.visEvtCodeQueryCancelUngroup:
                    case (short)VisEventCodes.visEvtCodeQueryCancelConvertToGroup:
                    case (short)VisEventCodes.visEvtCodeQueryCancelSelDel:
                    case (short)VisEventCodes.visEvtCodeCancelUngroup:

                        // Subject object is a Selection
                        subjectSelection =
                            (Selection)subject;
                        subjectApplication = subjectSelection.Application;
                        break;

                    // Shape event codes
                    case (short)VisEventCodes.visEvtShape + (short)VisEventCodes.visEvtDel:
                    case (short)VisEventCodes.visEvtCodeShapeBeforeTextEdit:
                    case (short)VisEventCodes.visEvtShape + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtShape + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtCodeShapeExitTextEdit:
                    case (short)VisEventCodes.visEvtCodeShapeParentChange:
                    case (short)VisEventCodes.visEvtCodeShapeDelete:
                    case (short)VisEventCodes.visEvtText + (short)VisEventCodes.visEvtMod:

                        // Subject object is a Shape
                        subjectShape =
                            (Shape)subject;
                        subjectApplication = subjectShape.Application;
                        name = subjectShape.Name;
                        break;

                    // Cell event codes
                    case (short)VisEventCodes.visEvtCell + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtFormula + (short)VisEventCodes.visEvtMod:

                        // Subject object is a Cell
                        subjectCell =
                            (Cell)subject;
                        subjectShape = subjectCell.Shape;
                        subjectApplication = subjectCell.Application;
                        name = subjectShape.Name + "!" + subjectCell.Name;
                        break;

                    // Connects event codes
                    case (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtConnect + (short)VisEventCodes.visEvtDel:

                        // Subject object is a Connects collection
                        subjectConnects =
                            (Connects)subject;
                        subjectApplication = subjectConnects.Application;
                        break;

                    // Style event codes
                    case (short)VisEventCodes.visEvtStyle + (short)VisEventCodes.visEvtDel:
                    case (short)VisEventCodes.visEvtStyle + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtStyle + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtCodeCancelStyleDel:
                    case (short)VisEventCodes.visEvtCodeQueryCancelStyleDel:

                        // Subject object is a Style
                        subjectStyle =
                            (Style)subject;
                        subjectApplication = subjectStyle.Application;
                        name = subjectStyle.Name;
                        break;

                    // Window event codes
                    case (short)VisEventCodes.visEvtWindow + (short)VisEventCodes.visEvtDel:
                    case (short)VisEventCodes.visEvtCodeBefWinPageTurn:
                    case (short)VisEventCodes.visEvtWindow + Constants.visEvtAdd:
                    case (short)VisEventCodes.visEvtWindow + (short)VisEventCodes.visEvtMod:
                    case (short)VisEventCodes.visEvtCodeWinPageTurn:
                    case (short)VisEventCodes.visEvtCodeBefWinSelDel:
                    case (short)VisEventCodes.visEvtCodeCancelWinClose:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtWinActivate:
                    case (short)VisEventCodes.visEvtCodeWinSelChange:
                    case (short)VisEventCodes.visEvtCodeViewChanged:
                    case (short)VisEventCodes.visEvtCodeQueryCancelWinClose:

                        // Subject object is a Window
                        subjectWindow =
                            (Window)subject;
                        subjectApplication = subjectWindow.Application;
                        name = subjectWindow.Caption;
                        break;

                    // Application event codes
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAfterModal:
                    case (short)VisEventCodes.visEvtCodeAfterResume:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppActivate:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppDeactivate:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtObjActivate:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtObjDeactivate:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeModal:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeQuit:
                    case (short)VisEventCodes.visEvtCodeBeforeSuspend:
                    case (short)VisEventCodes.visEvtCodeEnterScope:
                    case (short)VisEventCodes.visEvtCodeExitScope:
                    case (short)VisEventCodes.visEvtCodeKeyDown:
                    case (short)VisEventCodes.visEvtCodeKeyPress:
                    case (short)VisEventCodes.visEvtCodeKeyUp:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker:
                    case (short)VisEventCodes.visEvtCodeMouseDown:
                    case (short)VisEventCodes.visEvtCodeMouseMove:
                    case (short)VisEventCodes.visEvtCodeMouseUp:
                    case (short)VisEventCodes.visEvtCodeBefForcedFlush:
                    case (short)VisEventCodes.visEvtCodeAfterForcedFlush:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtNonePending:
                    case (short)VisEventCodes.visEvtCodeWinOnAddonKeyMSG:
                    case (short)VisEventCodes.visEvtCodeQueryCancelQuit:
                    case (short)VisEventCodes.visEvtCodeQueryCancelSuspend:
                    case (short)VisEventCodes.visEvtCodeCancelQuit:
                    case (short)VisEventCodes.visEvtCodeCancelSuspend:
                    case (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtIdle:

                        // Subject object is an Application
                        subjectApplication =
                            (Application)subject;
                        break;

                    default:
                        name = "Unknown";
                        break;
                }

                // get a description for this event code
                message = VisioAppEventHandler.GetDescription(eventCode);

                // append the name of the subject object
                if (name.Length > 0)
                {

                    message += ": " + name;
                }

                // append event info when it is available
                if (subjectApplication != null)
                {

                    eventInformation = subjectApplication.get_EventInfo(
                        (short)VisEventCodes.
                        visEvtIdMostRecent);

                    if (eventInformation != null)
                    {

                        message += tab + eventInformation;
                    }
                }

                // append moreInformation when it is available
                if (moreInformation != null)
                {

                    message += tab + moreInformation.ToString();
                }

                // get the targetArgs string from the event object. targetArgs
                // are added to the event object in the AddAdvise method
                EventList events = null;
                Event thisEvent = null;
                string sourceType;
                string targetArgs = "";

                sourceType = source.GetType().FullName;
                if (sourceType == "Microsoft.Office.Interop.Visio.ApplicationClass")
                {

                    events = ((Application)source)
                        .EventList;
                }
                else if (sourceType == "Microsoft.Office.Interop.Visio.DocumentClass")
                {

                    events = ((Document)source)
                        .EventList;
                }
                else if (sourceType == "Microsoft.Office.Interop.Visio.PageClass")
                {

                    events = ((Page)source)
                        .EventList;
                }

                if (events != null)
                {

                    thisEvent = events.get_ItemFromID(eventId);
                    targetArgs = thisEvent.TargetArgs;

                    // append targetArgs when it is available
                    if (targetArgs.Length > 0)
                    {

                        message += " " + targetArgs;
                    }
                }

                // Write the event info to the output window
                System.Diagnostics.Debug.WriteLine(message);
            }
            //catch (Exception err)
            //{
            //    System.Diagnostics.Debug.WriteLine(err.Message);
            //}
        }
    }
}