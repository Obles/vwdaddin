using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin
{
    public class Translit
    {
        public static String Encode(String str)
        {
            return str.
                Replace(" ", "").
                Replace("à", "a").
                Replace("á", "b").
                Replace("â", "v").
                Replace("ã", "g").
                Replace("ä", "d").
                Replace("å", "e").
                Replace("¸", "ye").
                Replace("æ", "zh").
                Replace("ç", "z").
                Replace("è", "i").
                Replace("é", "y").
                Replace("ê", "k").
                Replace("ë", "l").
                Replace("ì", "m").
                Replace("í", "n").
                Replace("î", "o").
                Replace("ï", "p").
                Replace("ð", "r").
                Replace("ñ", "s").
                Replace("ò", "t").
                Replace("ó", "u").
                Replace("ô", "f").
                Replace("õ", "ch").
                Replace("ö", "z").
                Replace("÷", "ch").
                Replace("ø", "sh").
                Replace("ù", "ch").
                Replace("ú", "'").
                Replace("û", "y").
                Replace("ü", "'").
                Replace("ý", "e").
                Replace("þ", "yu").
                Replace("ÿ", "ya").
                Replace("À", "A").
                Replace("Á", "B").
                Replace("Â", "V").
                Replace("Ã", "G").
                Replace("Ä", "D").
                Replace("Å", "E").
                Replace("¨", "Ye").
                Replace("Æ", "Zh").
                Replace("Ç", "Z").
                Replace("È", "I").
                Replace("É", "Y").
                Replace("Ê", "K").
                Replace("Ë", "L").
                Replace("Ì", "M").
                Replace("Í", "N").
                Replace("Î", "O").
                Replace("Ï", "P").
                Replace("Ð", "R").
                Replace("Ñ", "S").
                Replace("Ò", "T").
                Replace("Ó", "U").
                Replace("Ô", "F").
                Replace("Õ", "Ch").
                Replace("Ö", "Z").
                Replace("×", "Ch").
                Replace("Ø", "Sh").
                Replace("Ù", "Ch").
                Replace("Ú", "'").
                Replace("Û", "Y").
                Replace("Ü", "'").
                Replace("Ý", "E").
                Replace("Þ", "Yu").
                Replace("ß", "Ya");
        }
    }
}
