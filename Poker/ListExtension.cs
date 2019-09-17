using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public static class ListExtension
    {
        public static bool Swap<T>(this List<T> list, uint index1, uint index2)
        {
            //Swaps two spots in a list. Parameters are unsigned to force the user to give a non-negative index.
            if(index1 > list.Capacity || index2 > list.Capacity)
            {
                return false;
            }
            else
            {
                var ob = list[Convert.ToInt32(index1)];
                list[Convert.ToInt32(index1)] = list[Convert.ToInt32(index2)];
                list[Convert.ToInt32(index2)] = ob;
                return true;
            }
        }
    }
}
