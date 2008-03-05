using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin.DslWrapper
{
    enum Multiplicity
    {
        ZeroMany, ZeroOne, One, OneMany
    }

    class MultiplicityHelper
    {
        public static Multiplicity Parse(String value)
        {
            switch (value.ToLower())
            {
                case "zeromany": return Multiplicity.ZeroMany;
                case "zeroone": return Multiplicity.ZeroOne;
                case "one": return Multiplicity.One;
                case "onemany": return Multiplicity.OneMany;
                default: return Multiplicity.ZeroMany;
            }
        }
    }
}
