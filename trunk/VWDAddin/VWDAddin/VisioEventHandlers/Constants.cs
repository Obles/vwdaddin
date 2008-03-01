using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin
{
    class Constants
    {
        public const bool TraceAnyEvent = false;

        // Declare visEvtAdd as a 2-byte value to avoid a run-time overflow error.
        public const short visEvtAdd = -32768;

    }
}
