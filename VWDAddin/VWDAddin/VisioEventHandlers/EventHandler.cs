using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    public abstract class EventHandler
    {
        protected EventManager Owner;
        protected List<short> handleEvents;

        public EventHandler(EventManager manager, IEnumerable<short> events)
        {
            Owner = manager;
            handleEvents = new List<short>(events);
        }

        public abstract object VisEventProc(
            short eventCode,
            object source,
            int eventId,
            int eventSequenceNumber,
            object subject,
            object moreInformation
        );
        
        public bool HandlesEvent(short eventCode)
        {
            return handleEvents.Contains(eventCode);
        }

        #region Debug information
        public static void UnhandledEvent(short eventCode)
        {
            Debug.WriteLine("Unhandled event " + eventCode + " " + GetDescription(eventCode));
        }
        private static Dictionary<short, String> eventDescriptions = null;
        public static String GetDescription(short eventCode)
        {
            if(eventDescriptions == null)
            {
                initializeDescriptions();
            }
            String s = eventDescriptions[eventCode];
            return s == null? "NoEventDescription" : s;
        }
        public static void initializeDescriptions()
        {
            eventDescriptions = new Dictionary<short, String>();

            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAfterModal, 
                "AfterModal");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeAfterResume, 
                "AfterResume");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppActivate, 
                "AppActivated");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppDeactivate, 
                "AppDeactivated");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtObjActivate, 
                "AppObjActivated");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtObjDeactivate, 
                "AppObjDeactivated");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtDoc + (short)VisEventCodes.visEvtDel, 
                "BeforeDocumentClose");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBefDocSave, 
                "BeforeDocumentSave");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBefDocSaveAs, 
                "BeforeDocumentSaveAs");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtMaster + (short)VisEventCodes.visEvtDel, 
                "BeforeMasterDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeModal, 
                "BeforeModal");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtPage + (short)VisEventCodes.visEvtDel, 
                "BeforePageDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeQuit,
                "BeforeQuit");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBefSelDel, 
                "BeforeSelectionDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtShape + (short)VisEventCodes.visEvtDel, 
                "BeforeShapeDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeShapeBeforeTextEdit,
                "BeforeShapeTextEdit");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtStyle + (short)VisEventCodes.visEvtDel, 
                "BeforeStyleDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBeforeSuspend, 
                "BeforeSuspend");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtWindow + (short)VisEventCodes.visEvtDel, 
                "BeforeWindowClose");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBefWinPageTurn, 
                "BeforeWindowPageTurn");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBefWinSelDel, 
                "BeforeWindowSelDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCell + (short)VisEventCodes.visEvtMod, 
                "CellChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd, 
                "ConnectionsAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtConnect + (short)VisEventCodes.visEvtDel, 
                "ConnectionsDeleted");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelConvertToGroup,
                "ConvertToGroupCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeDocDesign, 
                "DesignModeEntered");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtDoc + Constants.visEvtAdd, 
                "DocumentAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtDoc + (short)VisEventCodes.visEvtMod, 
                "DocumentChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelDocClose, 
                "DocumentCloseCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeDocCreate, 
                "DocumentCreated");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeDocOpen, 
                "DocumentOpened");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeDocSave, 
                "DocumentSaved");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeDocSaveAs, 
                "DocumentSavedAs");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeEnterScope, 
                "EnterScope");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeExitScope, 
                "ExitScope");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtFormula + (short)VisEventCodes.visEvtMod, 
                "FormulaChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeKeyDown, 
                "KeyDown");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeKeyPress, 
                "KeyPress");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeKeyUp, 
                "KeyUp");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtMaster + Constants.visEvtAdd, 
                "MasterAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker, 
                "MarkerEvent");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtMaster + (short)VisEventCodes.visEvtMod, 
                "MasterChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelMasterDel, 
                "MasterDeleteCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeMouseDown, 
                "MouseDown");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeMouseMove, 
                "MouseMove");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeMouseUp, 
                "MouseUp");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeBefForcedFlush,
                "MustFlushScopeBeginning");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeAfterForcedFlush, 
                "MustFlushScopeEnded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtNonePending, 
                "NoEventsPending");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeWinOnAddonKeyMSG,
                "OnKeystrokeMessageForAddon");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtPage + Constants.visEvtAdd, 
                "PageAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtPage + (short)VisEventCodes.visEvtMod, 
                "PageChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelPageDel, 
                "PageDeleteCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelConvertToGroup,
                "QueryCancelConvertToGroup");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelDocClose,
                "QueryCancelDocumentClose");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelMasterDel,
                "QueryCancelMasterDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelPageDel,
                "QueryCancelPageDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelQuit, 
                "QuerCancelQuit");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelSelDel,
                "QueryCancelSelectionDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelStyleDel,
                "QueryCancelStyleDelete");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelSuspend, 
                "QueryCancelSuspend");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelUngroup, 
                "QueryCancelUngroup");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeQueryCancelWinClose,
                "QueryCancelWindowClose");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelQuit, 
                "QuitCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeDocRunning, 
                "RunModeEntered");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeSelAdded, 
                "SelectionAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeWinSelChange, 
                "SelectionChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelSelDel, 
                "SelectionDeleteCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtShape + Constants.visEvtAdd, 
                "ShapeAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtShape + (short)VisEventCodes.visEvtMod, 
                "ShapeChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeShapeExitTextEdit, 
                "ShapeExitedTextEdit");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeShapeParentChange, 
                "ShapeParentChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeShapeDelete, 
                "ShapesDeleted");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtStyle + Constants.visEvtAdd, 
                "StyleAdded");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtStyle + (short)VisEventCodes.visEvtMod, 
                "StyleChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelStyleDel, 
                "StyleDeleteCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelSuspend, 
                "SuspendCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtText + (short)VisEventCodes.visEvtMod, 
                "TextChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelUngroup, 
                "UngroupCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeViewChanged, 
                "ViewChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtIdle, 
                "VisioIsIdle");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtWinActivate, 
                "WindowActivated");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeCancelWinClose, 
                "WindowCloseCanceled");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtWindow + Constants.visEvtAdd, 
                "WindowOpened");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtWindow + (short)VisEventCodes.visEvtMod, 
                "WindowChanged");
            eventDescriptions.Add(
                (short)VisEventCodes.visEvtCodeWinPageTurn, 
                "WindowTurnedToPage");
        }
        #endregion
    }
}
