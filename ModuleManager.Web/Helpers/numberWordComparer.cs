using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleManager.Web.Helpers
{
    public class NumberWordComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int intX, intY;

            if (int.TryParse(x, out intX) && int.TryParse(y, out intY))
            {
                if (intX < intY) return -1;
                if (intX == intY) return 0;
                if (intX > intY) return 1;
            }

            return 0;
        }
    }
}