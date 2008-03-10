using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin.DslWrapper
{
    public enum Multiplicity
    {
        ZeroMany, ZeroOne, One, OneMany
    }

    public class MultiplicityHelper
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

        public static String AsDigits(Multiplicity mult)
        {
            switch (mult)
            {
                case Multiplicity.ZeroMany: return "0..*";
                case Multiplicity.ZeroOne: return "0..1";
                case Multiplicity.One: return "1";
                case Multiplicity.OneMany: return "1..*";
                default: throw new NotImplementedException();
            }
        }
    }
}
