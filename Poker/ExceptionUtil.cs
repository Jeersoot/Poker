using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public static class ExceptionUtil
    {
        public static string FormatExceptionString(string exception, string description)
        {
            //Returns a formated string that is formated for exception info giving.
            return "EXCEPTION: " + exception.ToUpper().Trim().Replace(' ', '_') + "\nDescription:\n\t" + description;
        }
    }
}
