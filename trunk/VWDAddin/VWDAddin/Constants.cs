using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin
{
    public class Constants
    {
        public const bool TraceAnyEvent = false;

        // Declare visEvtAdd as a 2-byte value to avoid a run-time overflow error.
        public const short visEvtAdd = -32768;

        public const String StencilName = "Stencil.vss";

        public const String Class = "Class";
        public const String Composition = "Composition";
        public const String Association = "Association";
        public const String Generalization = "Generalization";

        public enum ConnectionTypes
        {
            BeginConnected,
            EndConnected,            
        }
    }
}
