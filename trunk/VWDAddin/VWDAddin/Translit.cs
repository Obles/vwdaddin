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
                Replace("�", "a").
                Replace("�", "b").
                Replace("�", "v").
                Replace("�", "g").
                Replace("�", "d").
                Replace("�", "e").
                Replace("�", "ye").
                Replace("�", "zh").
                Replace("�", "z").
                Replace("�", "i").
                Replace("�", "y").
                Replace("�", "k").
                Replace("�", "l").
                Replace("�", "m").
                Replace("�", "n").
                Replace("�", "o").
                Replace("�", "p").
                Replace("�", "r").
                Replace("�", "s").
                Replace("�", "t").
                Replace("�", "u").
                Replace("�", "f").
                Replace("�", "ch").
                Replace("�", "z").
                Replace("�", "ch").
                Replace("�", "sh").
                Replace("�", "ch").
                Replace("�", "'").
                Replace("�", "y").
                Replace("�", "'").
                Replace("�", "e").
                Replace("�", "yu").
                Replace("�", "ya").
                Replace("�", "A").
                Replace("�", "B").
                Replace("�", "V").
                Replace("�", "G").
                Replace("�", "D").
                Replace("�", "E").
                Replace("�", "Ye").
                Replace("�", "Zh").
                Replace("�", "Z").
                Replace("�", "I").
                Replace("�", "Y").
                Replace("�", "K").
                Replace("�", "L").
                Replace("�", "M").
                Replace("�", "N").
                Replace("�", "O").
                Replace("�", "P").
                Replace("�", "R").
                Replace("�", "S").
                Replace("�", "T").
                Replace("�", "U").
                Replace("�", "F").
                Replace("�", "Ch").
                Replace("�", "Z").
                Replace("�", "Ch").
                Replace("�", "Sh").
                Replace("�", "Ch").
                Replace("�", "'").
                Replace("�", "Y").
                Replace("�", "'").
                Replace("�", "E").
                Replace("�", "Yu").
                Replace("�", "Ya");
        }
    }
}
