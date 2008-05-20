using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VWDAddin.VisioWrapper;

namespace VWDAddin.Synchronize
{
    public class UniqueNames
    {
        private static bool ExistsSame(VisioPage Page, VisioClass VisioClass, String Name)
        {
            foreach (VisioClass vc in Page.Classes)
            {
                if (!VisioClass.Equals(vc) && vc.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }

        public static String UniqueName(VisioPage Page, VisioClass VisioClass)
        {
            String Name = VisioClass.Name;
            Regex regex = new Regex(@"^(.*?)([0-9]+)$");
            Match m = regex.Match(Name);
            String BaseName = m.Success ? m.Groups[1].Value : Name;
            int index = m.Success ? int.Parse(m.Groups[2].Value) : 1;

            while (ExistsSame(Page, VisioClass, Name))
            {
                Name = BaseName + (++index);            
            }

            Trace.WriteLine(Name);
            return Name;
        }

        private static bool ExistsSame(VisioPage Page, VisioConnector VisioConnector, String Name)
        {
            foreach (VisioConnector vc in Page.Relationships)
            {
                if (!VisioConnector.Equals(vc) && vc.Name == Name)
                {
                    return true;
                }
            }
            return false;
        }

        public static String UniqueName(VisioPage Page, VisioConnector VisioConnector)
        {
            String Name = VisioConnector.Name;
            Regex regex = new Regex(@"^(.*?)([0-9]+)$");
            Match m = regex.Match(Name);
            String BaseName = m.Success ? m.Groups[1].Value : Name;
            int index = m.Success ? int.Parse(m.Groups[2].Value) : 1;

            while (ExistsSame(Page, VisioConnector, Name))
            {
                Name = BaseName + (++index);
            }

            Trace.WriteLine(Name);
            return Name;
        }
    }
}
