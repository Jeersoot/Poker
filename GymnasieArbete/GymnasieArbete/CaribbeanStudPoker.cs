using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace GymnasieArbete
{
    class CaribbeanStudPoker : Pokergame
    {
        private static Hashtable strategyMap = new Hashtable() {
            {"12-11-10", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-9", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-8", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-7", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-6", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-5", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-4", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-3", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-11-2", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R", "R"}},
            {"12-10-9", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "2", "R", "R", "R"}},
            {"12-10-8", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "3", "R", "R", "R"}},
            {"12-10-7", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "3", "R", "R", "R"}},
            {"12-10-6", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "4", "R", "R", "R"}},
            {"12-10-5", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "4", "R", "R", "R"}},
            {"12-10-4", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "4", "R", "R", "R"}},
            {"12-10-3", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "R", "4", "R", "R", "R"}},
            {"12-10-2", new List<String>(){"R", "R", "R", "R", "1", "1", "R", "R", "R", "4", "R", "R", "R"}},
            {"12-9-8", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "F", "3", "R", "R", "R"}},
            {"12-9-7", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "F", "3", "R", "R", "R"}},
            {"12-9-6", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "F", "4", "R", "R", "R"}},
            {"12-9-5", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "F", "4", "R", "R", "R"}},
            {"12-9-4", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "R", "F", "4", "R", "R", "R"}},
            {"12-9-3", new List<String>(){"R", "R", "R", "R", "1", "1", "R", "R", "F", "4", "R", "R", "R"}},
            {"12-9-2", new List<String>(){"R", "R", "R", "1", "1", "1", "R", "R", "F", "4", "R", "R", "R"}},
            {"12-8-7", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "F", "F", "3*", "R", "R", "R"}},
            {"12-8-6", new List<String>(){"R", "R", "R", "R", "R", "R", "R", "F", "F", "4", "R", "R", "R"}},
            {"12-8-5", new List<String>(){"R", "R", "R", "R", "1", "1", "R", "F", "F", "4", "R", "R", "R"}},
            {"12-8-4", new List<String>(){"R", "R", "R", "1", "1", "1", "R", "F", "F", "4", "R", "R", "R"}},
            {"12-8-3", new List<String>(){"R", "R", "R", "1", "1", "1", "R", "F", "F", "4", "R", "R", "R"}},
            {"12-8-2", new List<String>(){"R", "R", "R", "1", "2", "2", "R", "F", "F", "4", "R", "R", "R"}},
            {"12-7-6", new List<String>(){"R", "R", "R", "1", "R", "R", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-7-5", new List<String>(){"R", "R", "R", "R", "1", "R", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-7-4", new List<String>(){"R", "R", "R", "1", "2", "R", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-7-3", new List<String>(){"R", "R", "1", "2", "2", "R", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-7-2", new List<String>(){"R", "R", "1", "2", "2*", "R", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-6-5", new List<String>(){"R", "R", "R", "R", "R", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-6-4", new List<String>(){"R", "R", "R", "2", "R", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-6-3", new List<String>(){"R", "R", "1", "3", "R", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-6-2", new List<String>(){"R", "R", "1*", "3", "R", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-5-4", new List<String>(){"R", "R", "R", "R", "F", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-5-3", new List<String>(){"R", "R", "2", "R", "F", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-5-2", new List<String>(){"R", "1", "2*", "R", "F", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-4-3", new List<String>(){"R", "R", "R", "F", "F", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-4-2", new List<String>(){"R", "2", "R", "F", "F", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"12-3-2", new List<String>(){"R", "R", "F", "F", "F", "F", "F", "F", "F", "F", "R", "R", "R"}},
            {"11-10-9", new List<String>(){"F", "F", "F", "F", "F", "F", "F", "R", "R", "R", "F", "R", "R"}},
            {"11-10-8", new List<String>(){"F", "F", "F", "F", "F", "F", "R", "F", "R", "R", "F", "R", "R"}},
            {"11-10-7", new List<String>(){"F", "F", "F", "F", "F", "R", "F", "F", "R", "R", "F", "R", "R"}},
            {"11-10-6", new List<String>(){"F", "F", "F", "F", "R", "F", "F", "F", "R", "R", "F", "R", "R"}},
            {"11-10-5", new List<String>(){"F", "F", "F", "R", "F", "F", "F", "F", "R", "R", "F", "R", "R"}},
            {"11-10-4", new List<String>(){"F", "F", "R", "F", "F", "F", "F", "F", "R", "R", "F", "R", "R"}},
            {"11-10-3", new List<String>(){"F", "R", "F", "F", "F", "F", "F", "F", "R", "R", "F", "R", "R"}},
            {"11-10-2", new List<String>(){"R", "F", "F", "F", "F", "F", "F", "F", "R", "R", "F", "R", "R"}},
            {"11-9-8", new List<String>(){"F", "F", "F", "F", "F", "F", "R", "R", "F", "R", "F", "R", "R"}},
            {"11-9-7", new List<String>(){"F", "F", "F", "F", "F", "R", "F", "R", "F", "R", "F", "R", "R"}},
            {"11-9-6", new List<String>(){"F", "F", "F", "F", "R", "F", "F", "R", "F", "R", "F", "R", "R"}},
            {"11-9-5", new List<String>(){"F", "F", "F", "R", "F", "F", "F", "R", "F", "R", "F", "R", "R"}},
            {"11-9-4", new List<String>(){"F", "F", "R", "F", "F", "F", "F", "R", "F", "R", "F", "R", "R"}},
            {"11-9-3", new List<String>(){"F", "R", "F", "F", "F", "F", "F", "R", "F", "R", "F", "R", "R"}},
            {"11-9-2", new List<String>(){"R", "F", "F", "F", "F", "F", "F", "R", "F", "R", "F", "R", "R"}},
            {"11-8-7", new List<String>(){"F", "F", "F", "F", "F", "R", "R", "F", "F", "R", "F", "R", "R"}},
            {"11-8-6", new List<String>(){"F", "F", "F", "F", "R", "F", "R", "F", "F", "R", "F", "R", "R"}},
            {"11-8-5", new List<String>(){"F", "F", "F", "R", "F", "F", "R", "F", "F", "R", "F", "R", "R"}},
            {"11-8-4", new List<String>(){"F", "F", "R", "F", "F", "F", "R", "F", "F", "R", "F", "R", "R"}},
            {"11-8-3", new List<String>(){"F", "R", "F", "F", "F", "F", "R", "F", "F", "R", "F", "R", "R"}},
            {"11-8-2", new List<String>(){"R", "F", "F", "F", "F", "F", "R", "F", "F", "R", "F", "R", "R"}},
            {"11-7-6", new List<String>(){"F", "F", "F", "F", "R", "R", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-7-5", new List<String>(){"F", "F", "F", "R", "F", "R", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-7-4", new List<String>(){"F", "F", "R", "F", "F", "R", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-7-3", new List<String>(){"F", "R", "F", "F", "F", "R", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-7-2", new List<String>(){"R", "F", "F", "F", "F", "R", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-6-5", new List<String>(){"F", "F", "F", "R", "R", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-6-4", new List<String>(){"F", "F", "R", "F", "R", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-6-3", new List<String>(){"F", "R", "F", "F", "R", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-6-2", new List<String>(){"R", "F", "F", "F", "R", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-5-4", new List<String>(){"F", "F", "R", "R", "F", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-5-3", new List<String>(){"F", "R", "F", "R", "F", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-5-2", new List<String>(){"R", "F", "F", "R", "F", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-4-3", new List<String>(){"F", "R", "R", "F", "F", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-4-2", new List<String>(){"R", "F", "R", "F", "F", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"11-3-2", new List<String>(){"R", "R", "F", "F", "F", "F", "F", "F", "F", "R", "F", "R", "R"}},
            {"10-9-8", new List<String>(){"F", "F", "F", "F", "F", "F", "R", "R", "R", "F", "F", "F", "F"}},
            {"10-9-7", new List<String>(){"F", "F", "F", "F", "F", "R", "F", "R", "R", "F", "F", "F", "F"}},
            {"10-9-6", new List<String>(){"F", "F", "F", "F", "R", "F", "F", "R", "R", "F", "F", "F", "F"}},
            {"10-9-5", new List<String>(){"F", "F", "F", "R", "F", "F", "F", "R", "R", "F", "F", "F", "F"}},
            {"10-9-4", new List<String>(){"F", "F", "R", "F", "F", "F", "F", "R", "R", "F", "F", "F", "F"}},
            {"10-9-3", new List<String>(){"F", "R", "F", "F", "F", "F", "F", "R", "R", "F", "F", "F", "F"}},
            {"10-9-2", new List<String>(){"R", "F", "F", "F", "F", "F", "F", "R", "R", "F", "F", "F", "F"}},
            {"10-8-7", new List<String>(){"F", "F", "F", "F", "F", "R", "R", "F", "R", "F", "F", "F", "F"}},
            {"10-8-6", new List<String>(){"F", "F", "F", "F", "R", "F", "R", "F", "R", "F", "F", "F", "F"}},
            {"10-8-5", new List<String>(){"F", "F", "F", "R", "F", "F", "R", "F", "R", "F", "F", "F", "F"}},
            {"10-8-4", new List<String>(){"F", "F", "R", "F", "F", "F", "R", "F", "R", "F", "F", "F", "F"}},
            {"10-8-3", new List<String>(){"F", "R", "F", "F", "F", "F", "R", "F", "R", "F", "F", "F", "F"}},
            {"10-8-2", new List<String>(){"R", "F", "F", "F", "F", "F", "R", "F", "R", "F", "F", "F", "F"}},
            {"10-7-6", new List<String>(){"F", "F", "F", "F", "R", "R", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-7-5", new List<String>(){"F", "F", "F", "R", "F", "R", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-7-4", new List<String>(){"F", "F", "R", "F", "F", "R", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-7-3", new List<String>(){"F", "R", "F", "F", "F", "R", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-7-2", new List<String>(){"R", "F", "F", "F", "F", "R", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-6-5", new List<String>(){"F", "F", "F", "R", "R", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-6-4", new List<String>(){"F", "F", "R", "F", "R", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-6-3", new List<String>(){"F", "R", "F", "F", "R", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-6-2", new List<String>(){"R", "F", "F", "F", "R", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-5-4", new List<String>(){"F", "F", "R", "R", "F", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-5-3", new List<String>(){"F", "R", "F", "R", "F", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-5-2", new List<String>(){"R", "F", "F", "R", "F", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-4-3", new List<String>(){"F", "R", "R", "F", "F", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-4-2", new List<String>(){"R", "F", "R", "F", "F", "F", "F", "F", "R", "F", "F", "F", "F"}},
            {"10-3-2", new List<String>(){"R", "R", "F", "F", "F", "F", "F", "F", "1*", "F", "F", "F", "F"}},
            {"9-8-7", new List<String>(){"F", "F", "F", "F", "F", "R", "R", "R", "F", "F", "F", "F", "F"}},
            {"9-8-6", new List<String>(){"F", "F", "F", "F", "R", "F", "R", "R", "F", "F", "F", "F", "F"}},
            {"9-8-5", new List<String>(){"F", "F", "F", "R", "F", "F", "R", "R", "F", "F", "F", "F", "F"}},
            {"9-8-4", new List<String>(){"F", "F", "R", "F", "F", "F", "R", "R", "F", "F", "F", "F", "F"}},
            {"9-8-3", new List<String>(){"F", "R", "F", "F", "F", "F", "R", "R", "F", "F", "F", "F", "F"}},
            {"9-8-2", new List<String>(){"R", "F", "F", "F", "F", "F", "R", "R", "F", "F", "F", "F", "F"}},
            {"9-7-6", new List<String>(){"F", "F", "F", "F", "R", "R", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-7-5", new List<String>(){"F", "F", "F", "R", "F", "R", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-7-4", new List<String>(){"F", "F", "R", "F", "F", "R", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-7-3", new List<String>(){"F", "R", "F", "F", "F", "R", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-7-2", new List<String>(){"R", "F", "F", "F", "F", "R", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-6-5", new List<String>(){"F", "F", "F", "R", "R", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-6-4", new List<String>(){"F", "F", "R", "F", "R", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-6-3", new List<String>(){"F", "R", "F", "F", "R", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-6-2", new List<String>(){"R", "F", "F", "F", "R", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-5-4", new List<String>(){"F", "F", "R", "R", "F", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-5-3", new List<String>(){"F", "R", "F", "R", "F", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-5-2", new List<String>(){"R", "F", "F", "R", "F", "F", "F", "R", "F", "F", "F", "F", "F"}},
            {"9-4-3", new List<String>(){"F", "R", "R", "F", "F", "F", "F", "1", "F", "F", "F", "F", "F"}},
            {"9-4-2", new List<String>(){"R", "F", "R", "F", "F", "F", "F", "2", "F", "F", "F", "F", "F"}},
            {"9-3-2", new List<String>(){"R", "R", "F", "F", "F", "F", "F", "4", "F", "F", "F", "F", "F"}},
            {"8-7-6", new List<String>(){"F", "F", "F", "F", "R", "R", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-7-5", new List<String>(){"F", "F", "F", "R", "F", "R", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-7-4", new List<String>(){"F", "F", "R", "F", "F", "R", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-7-3", new List<String>(){"F", "R", "F", "F", "F", "R", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-7-2", new List<String>(){"R", "F", "F", "F", "F", "R", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-6-5", new List<String>(){"F", "F", "F", "R", "R", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-6-4", new List<String>(){"F", "F", "R", "F", "R", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-6-3", new List<String>(){"F", "R", "F", "F", "R", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-6-2", new List<String>(){"R", "F", "F", "F", "R", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-5-4", new List<String>(){"F", "F", "R", "R", "F", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-5-3", new List<String>(){"F", "R", "F", "R", "F", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-5-2", new List<String>(){"R", "F", "F", "R", "F", "F", "R", "F", "F", "F", "F", "F", "F"}},
            {"8-4-3", new List<String>(){"F", "R", "R", "F", "F", "F", "1", "F", "F", "F", "F", "F", "F"}},
            {"8-4-2", new List<String>(){"R", "F", "R", "F", "F", "F", "2", "F", "F", "F", "F", "F", "F"}},
            {"8-3-2", new List<String>(){"R", "R", "F", "F", "F", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-6-5", new List<String>(){"F", "F", "F", "R", "R", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-6-4", new List<String>(){"F", "F", "R", "F", "R", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-6-3", new List<String>(){"F", "R", "F", "F", "R", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-6-2", new List<String>(){"R", "F", "F", "F", "R", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-5-4", new List<String>(){"F", "F", "R", "R", "F", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-5-3", new List<String>(){"F", "R", "F", "R", "F", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-5-2", new List<String>(){"R", "F", "F", "R", "F", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-4-3", new List<String>(){"F", "R", "R", "F", "F", "R", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-4-2", new List<String>(){"R", "F", "R", "F", "F", "1*", "F", "F", "F", "F", "F", "F", "F"}},
            {"7-3-2", new List<String>(){"R", "R", "F", "F", "F", "4", "F", "F", "F", "F", "F", "F", "F"}},
            {"6-5-4", new List<String>(){"F", "F", "R", "R", "R", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"6-5-3", new List<String>(){"F", "R", "F", "R", "R", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"6-5-2", new List<String>(){"R", "F", "F", "R", "R", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"6-4-3", new List<String>(){"F", "R", "R", "F", "R", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"6-4-2", new List<String>(){"R", "F", "R", "F", "1", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"6-3-2", new List<String>(){"R", "R", "F", "F", "2*", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"5-4-3", new List<String>(){"F", "R", "R", "R", "F", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"5-4-2", new List<String>(){"R", "F", "R", "R", "F", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"5-3-2", new List<String>(){"R", "R", "F", "1", "F", "F", "F", "F", "F", "F", "F", "F", "F"}},
            {"4-3-2", new List<String>(){"R", "R", "R", "F", "F", "F", "F", "F", "F", "F", "F", "F", "F"}}
         };


        private Hand dealerHand = null;

        public CaribbeanStudPoker()
        {

        }

        public void SetHand(Hand h)
        {
            dealerHand = h;
        }
        
        private int PayOutTable(Hand hand)
        {
            int toRet = 0;
            switch (hand.GetRank())
            {
                case CardRank.RoyalFlush:
                    toRet = 100;
                    break;
                case CardRank.StraightFlush:
                    toRet = 50;
                    break;
                case CardRank.Quads:
                    toRet = 20;
                    break;
                case CardRank.FullHouse:
                    toRet = 7;
                    break;
                case CardRank.Flush:
                    toRet = 5;
                    break;
                case CardRank.Straight:
                    toRet = 4;
                    break;
                case CardRank.Set:
                    toRet = 3;
                    break;
                case CardRank.TwoPair:
                    toRet = 2;
                    break;
                case CardRank.Pair:
                    toRet = 1;
                    break;
                case CardRank.HighCard:
                    toRet = 1;
                    break;
                default:
                    break;
            }
            return toRet;
        }

        public void Play(Player player)
        {
            int ante = 1;
            int bid = 2;

            Console.Write(player.GetHand() + "vs " + dealerHand + "- " + player.GetHand().GetRank() + " -");

            //Now start to play
            player.Debitbalance(ante);
            balance += ante;

            if (DoCall(player.GetHand()))
            {
                player.Debitbalance(bid);
                player.HandleCalls();
                balance += bid;

                if (IsQualified(dealerHand))
                {
                    if (player.GetHand().GetRank() > dealerHand.GetRank())
                    {
                        int odds = PayOutTable(player.GetHand());
                        // Player wins, pay out bid * odds, plus initial ante*2 and bid
                        player.Creditbalance(odds * bid + ante*2 + bid);
                        player.HandleWins();
                        balance -= (odds * bid + ante * 2 + bid);
                        // Casino loses
                        losses++;
                        Console.WriteLine(" win, odds = " + odds);
                    }
                    else if (player.GetHand().GetRank() < dealerHand.GetRank())
                    {
                        // Player lose
                        player.HandleLoss();

                        // Casino wins
                        wins++;
                        Console.WriteLine(" loss");
                    }
                    else
                    {
                        //Hands are e12aul. Compare card by card 
                        int res = CompareHands(dealerHand, player.GetHand());
                        if(res == 1)
                        {
                            //Player lose
                            player.HandleLoss();
                            //Casino wins
                            wins++;
                            Console.WriteLine(" loss");
                        }
                        else if (res == -1)
                        {
                            int odds = PayOutTable(player.GetHand());
                            // Player wins, pay out bid * odds, plus initial ante*2 and bid
                            player.Creditbalance(odds * bid + ante * 2 + bid);
                            player.HandleWins();
                            balance -= (odds * bid + ante * 2 + bid);
                            //Casino lose
                            losses++;
                            Console.WriteLine(" win, odds = " + odds);
                        }
                        else
                        {
                            //Return ante + bid
                            player.Creditbalance(ante + bid);
                            balance -= (ante + bid);

                            draws++;
                            Console.WriteLine(" draw");
                        }
                    }
                } 
                else
                {
                    //Pay back bid and 2*ante
                    player.Creditbalance(ante + ante + bid);
                    balance -= (ante + ante + bid);
                    nq++;
                    Console.WriteLine(" NQ");
                }
            }
            else
            {
                //Player folds
                player.HandleFold();
                folds++;
                Console.WriteLine(" fold");
            }
        }

        private bool DoCall(Hand h)
        {
            bool toRet = false;
            if (h.GetRank() >= CardRank.Pair)
            {
                calls++;
                toRet = true;
            }
            else
            {
                if (h.GetHand()[4].GetRank() == 14 && h.GetHand()[3].GetRank() == 13)
                {
                    if (CallIfAceAndKing(h, dealerHand.GetUpCard()))
                    {
                        toRet = true;
                        calls++;
                    }
                }
            }
            return toRet;
        }

        private bool IsQualified(Hand h)
        {
            bool toRet = false;
            if (h.GetRank() > CardRank.HighCard)
            {
                toRet = true;
            }
            else
            {
                bool kingOrAce = false;
                foreach (Card c in h.GetHand())
                {
                    if (c.GetRank() == 13 || c.GetRank() == 14)
                    {
                        if (kingOrAce)
                        {
                            toRet = true;
                            break;
                        }
                        kingOrAce = true;
                    }
                }
            }

            return toRet;
        }
        private int CompareHands(Hand h1, Hand h2)
        {
            int toRet = 0;
            switch (h1.GetRank())
            {
                case CardRank.RoyalFlush:
                    toRet = 0;
                    break;
                case CardRank.StraightFlush:
                    if (h1.GetHand()[4].GetRank() > h2.GetHand()[4].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetHand()[4].GetRank() < h2.GetHand()[4].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.Quads:
                    if (h1.GetQuad()[0].GetRank() > h2.GetQuad()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetQuad()[0].GetRank() < h2.GetQuad()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.FullHouse:
                case CardRank.Set:
                    if (h1.GetSet()[0].GetRank() > h2.GetSet()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else
                    {
                        toRet = -1;
                    }
                    break;
                case CardRank.Flush:
                case CardRank.Straight:
                    if (h1.GetHand()[0].GetRank() > h2.GetHand()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetHand()[0].GetRank() < h2.GetHand()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.TwoPair:
                    toRet = 0;
                    if (h1.GetPair2()[0].GetRank() > h2.GetPair2()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetPair2()[0].GetRank() < h2.GetPair2()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        if (h1.GetPair1()[0].GetRank() > h2.GetPair1()[0].GetRank())
                        {
                            toRet = 1;
                        }
                        else if (h1.GetPair1()[0].GetRank() < h2.GetPair1()[0].GetRank())
                        {
                            toRet = -1;
                        }
                    }
                    break;
                case CardRank.Pair:
                    if (h1.GetPair1()[0].GetRank() > h2.GetPair1()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetPair1()[0].GetRank() < h2.GetPair1()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.HighCard:
                    for (int i = 4; i > 0; i--)
                    {
                        if (h1.GetHand()[i].GetRank() > h2.GetHand()[i].GetRank())
                        {
                            toRet = 1;
                            break;
                        }
                        else if (h1.GetHand()[i].GetRank() < h2.GetHand()[i].GetRank())
                        {
                            toRet = -1;
                            break;
                        }
                    }
                    toRet = 0;
                    break;
                default:
                    break;
            }

            //debugging purposes
            /*
            Console.Write(h1.GetRank() + "  ");

            if (toRet == 0)
            {
                Console.WriteLine(h1 + "= " + h2);
            }
            else if (toRet == 1)
            {
                Console.WriteLine(h1 + "> " + h2);
            }
            else
            {
                Console.WriteLine(h1 + "< " + h2);
            }
            */
            return toRet;
        }

        private bool CallIfAceAndKing(Hand hand, Card dealersUp)
        {
            String threeCardKey = hand.GetHand()[2].GetRank() + "-" + hand.GetHand()[1].GetRank() + "-" + hand.GetHand()[0].GetRank();
            List<string> threeCardList = (List<string>)strategyMap[threeCardKey];
            String strategyCode = threeCardList[dealersUp.GetRank() - 2];
            bool toRet = false;

            switch (strategyCode)
            {
                case "R":
                    // Always raise
                    toRet = true;
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                    // Raise if the suit of dealer's card matches the suit of at least x number of your cards.
                    int noOfSameSuit = 0;
                    foreach (Card c in hand.GetHand())
                    {
                        if (dealersUp.GetSuit() == c.GetSuit())
                        {
                            noOfSameSuit++;
                        }
                    }

                    try
                    {
                        if (noOfSameSuit >= Convert.ToInt32(strategyCode))
                        {
                            toRet = true;
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        // should never happen	
                    }
                    toRet = false;
                    break;

                case "F":
                    toRet = false;
                    break;
                default:
                    // Borderline Hands not implemented.
                    // Better safe than sorry -> don't call
                    toRet = false;
                    Console.WriteLine("Borderline Not Implemented, but wow!!");
                    break;
            }
            return toRet;
        }

        public Hand GetDealerHand()
        {
            return dealerHand;
        }
    }
}
