using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    public class AnyEventHandler : EventHandler
    {
        public static short[] HandleEvents = {
            // Document event codes
            (short)VisEventCodes.visEvtDoc + (short)VisEventCodes.visEvtDel,
            (short)VisEventCodes.visEvtCodeBefDocSave,
            (short)VisEventCodes.visEvtCodeBefDocSaveAs,
            (short)VisEventCodes.visEvtCodeDocDesign,
            (short)VisEventCodes.visEvtDoc + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtDoc + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtCodeCancelDocClose,
            (short)VisEventCodes.visEvtCodeDocCreate,
            (short)VisEventCodes.visEvtCodeDocOpen,
            (short)VisEventCodes.visEvtCodeDocSave,
            (short)VisEventCodes.visEvtCodeDocSaveAs,
            (short)VisEventCodes.visEvtCodeDocRunning,

            // Page event codes
            (short)VisEventCodes.visEvtPage + (short)VisEventCodes.visEvtDel,
            (short)VisEventCodes.visEvtPage + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtPage + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtCodeCancelPageDel,

            // Master event codes
            (short)VisEventCodes.visEvtMaster + (short)VisEventCodes.visEvtDel,
            (short)VisEventCodes.visEvtMaster + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtCodeCancelMasterDel,
            (short)VisEventCodes.visEvtMaster + Constants.visEvtAdd,

            // Selection event codes
            (short)VisEventCodes.visEvtCodeBefSelDel,
            (short)VisEventCodes.visEvtCodeSelAdded,
            (short)VisEventCodes.visEvtCodeCancelSelDel,
            (short)VisEventCodes.visEvtCodeCancelConvertToGroup,
            (short)VisEventCodes.visEvtCodeCancelUngroup,

            // Shape event codes
            (short)VisEventCodes.visEvtShape + (short)VisEventCodes.visEvtDel,
            (short)VisEventCodes.visEvtCodeShapeBeforeTextEdit,
            (short)VisEventCodes.visEvtShape + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtShape + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtCodeShapeExitTextEdit,
            (short)VisEventCodes.visEvtCodeShapeParentChange,
            (short)VisEventCodes.visEvtText + (short)VisEventCodes.visEvtMod,

            // Cell event codes
            (short)VisEventCodes.visEvtCell + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtFormula + (short)VisEventCodes.visEvtMod,

            // Connects event codes
            (short)VisEventCodes.visEvtConnect + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtConnect + (short)VisEventCodes.visEvtDel,

            // Style event codes
            (short)VisEventCodes.visEvtStyle + (short)VisEventCodes.visEvtDel,
            (short)VisEventCodes.visEvtStyle + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtStyle + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtCodeCancelStyleDel,

            // Window event codes
            (short)VisEventCodes.visEvtWindow + (short)VisEventCodes.visEvtDel,
            (short)VisEventCodes.visEvtCodeBefWinPageTurn,
            (short)VisEventCodes.visEvtWindow + Constants.visEvtAdd,
            (short)VisEventCodes.visEvtWindow + (short)VisEventCodes.visEvtMod,
            (short)VisEventCodes.visEvtCodeWinPageTurn,
            (short)VisEventCodes.visEvtCodeBefWinSelDel,
            (short)VisEventCodes.visEvtCodeCancelWinClose,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtWinActivate,
            (short)VisEventCodes.visEvtCodeWinSelChange,
            (short)VisEventCodes.visEvtCodeViewChanged,

            // Application event codes
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAfterModal,
            (short)VisEventCodes.visEvtCodeAfterResume,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppActivate,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtAppDeactivate,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtObjActivate,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtObjDeactivate,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeModal,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtBeforeQuit,
            (short)VisEventCodes.visEvtCodeBeforeSuspend,
            (short)VisEventCodes.visEvtCodeEnterScope,
            (short)VisEventCodes.visEvtCodeExitScope,
            (short)VisEventCodes.visEvtCodeKeyDown,
            (short)VisEventCodes.visEvtCodeKeyPress,
            (short)VisEventCodes.visEvtCodeKeyUp,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtMarker,
            (short)VisEventCodes.visEvtCodeMouseDown,
            (short)VisEventCodes.visEvtCodeMouseMove,
            (short)VisEventCodes.visEvtCodeMouseUp,
            (short)VisEventCodes.visEvtCodeBefForcedFlush,
            (short)VisEventCodes.visEvtCodeAfterForcedFlush,
            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtNonePending,
            (short)VisEventCodes.visEvtCodeWinOnAddonKeyMSG,
            (short)VisEventCodes.visEvtCodeCancelQuit,
            (short)VisEventCodes.visEvtCodeCancelSuspend,
//            (short)VisEventCodes.visEvtApp + (short)VisEventCodes.visEvtIdle,
        };

        public AnyEventHandler(EventManager manager)
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
            return true;
        }
    }
}