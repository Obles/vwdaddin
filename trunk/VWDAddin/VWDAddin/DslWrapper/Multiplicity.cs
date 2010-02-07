using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

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

        public static Multiplicity Compatible(String value)
        {
            Regex rx = new Regex(@"\d+|\*|\+");
            MatchCollection matches = rx.Matches(value);

            if (matches.Count == 0)
            {
                return Multiplicity.ZeroMany;
            }
            else if (matches.Count == 1)
            {
                return CompatibleOne(matches[0].Groups[0].Value);
            }
            else
            {
                String fstStr = matches[0].Groups[0].Value;
                String sndStr = matches[1].Groups[0].Value;
                int? fst = ParseInt(fstStr);
                int? snd = ParseInt(sndStr);

                // сортируем
                if (fst == null)
                {
                    if (snd != null)
                    {
                        Swap(ref fst, ref fstStr, ref snd, ref sndStr);
                    }
                    else return CompatibleOne(fstStr);
                }
                if(snd != null && fst > snd)
                {
                    Swap(ref fst, ref fstStr, ref snd, ref sndStr);
                }

                // определяем наиболее совместимую множественность
                if (fst > 0)
                {
                    if (fst == snd && snd == 1)
                    {
                        return Multiplicity.One;
                    }
                    else return Multiplicity.OneMany;
                }
                else
                {
                    if (snd == 1)
                    {
                        return Multiplicity.ZeroOne;
                    }
                    else return Multiplicity.ZeroMany;
                }
            }
        }

        private static Multiplicity CompatibleOne(String countStr)
        {
            int count;
            if (int.TryParse(countStr, out count))
            {
                if (count == 1)
                {
                    return Multiplicity.One;
                }
                else
                {
                    return Multiplicity.OneMany;
                }
            }
            else 
            {
                switch (countStr)
                {
                    case "*": return Multiplicity.ZeroMany;
                    case "+": return Multiplicity.OneMany;
                    default: throw new NotSupportedException();
                }
            }
        }

        private static int? ParseInt(String value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return null;
            }
        }

        private static void Swap(ref int? fst, ref String fstStr, ref int? snd, ref String sndStr)
        {
            String tmpStr = fstStr;
            int? tmp = fst;

            fstStr = sndStr;
            fst = snd;

            sndStr = tmpStr;
            snd = tmp;
        }

        public static bool Equals(String str, Multiplicity mul)
        {
            return mul.Equals(Compatible(str));
        }
    }
}
