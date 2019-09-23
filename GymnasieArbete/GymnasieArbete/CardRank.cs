using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    // Gives every card combination in the cardranking a value
    enum CardRank
    {
        HighCard = 0,
        Pair = 1,
        TwoPair = 2,
        Set = 3,
        Straight = 4,
        Flush = 5,
        FullHouse = 6,
        Quads = 7,
        StraightFlush = 8,
        RoyalFlush = 9
    }
}
