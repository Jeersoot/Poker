using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    // Gives every card combination in the cardrank a value
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
        RoyalFlush = 9,
        FOURCARD_HighCard = 10,
        FOURCARD_Pair = 20,
        FOURCARD_TwoPair = 30,
        FOURCARD_Straight = 40,
        FOURCARD_Flush = 50,
        FOURCARD_Set = 60,
        FOURCARD_StraightFlush = 70,
        FOURCARD_Quads = 80
    }
}
