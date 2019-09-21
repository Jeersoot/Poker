using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GymnasieArbete
{
    public static class Helper
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attr.Length == 0 ? value.ToString() : (attr[0] as DescriptionAttribute).Description;
        }
    }
    enum Suit
    {
        [Description("♣")] Club = 1,
        [Description("♦")] Diamond = 2,
        [Description("♥")] Heart = 3,
        [Description("♠")] Spade = 4
    }

   
}
