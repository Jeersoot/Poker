using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GymnasieArbete
{
    /* 
     * Creates the card
     */
    class Card
    {
        public static int TWO = 2;
        public static int THREE = 3;
        public static int FOUR = 4;
        public static int FIVE = 5;
        public static int SIX = 6;
        public static int SEVEN = 7;
        public static int EIGHT = 8;
        public static int NINE = 9;
        public static int TEN = 10;
        public static int JACK = 11;
        public static int QUEEN = 12;
        public static int KING = 13;
        public static int ACE = 14;
    
        private int rank;
        private Suit suit;

        public Card(int r, Suit s)
        {
            rank = r;
            suit = s;
        }
        public override String ToString()
        {
            // Check if a card is a face card or not
            String toRet = "";
            switch (rank)
            {
                case 11:
                    // Jack 
                    toRet = suit.GetDescription() + "J";
                    break;
                case 12:
                    // Queen
                    toRet = suit.GetDescription() + "Q";
                    break;
                case 13:
                    // King
                    toRet = suit.GetDescription() + "K";
                    break;
                case 14:
                    // Ace
                    toRet = suit.GetDescription() + "A";
                    break;
                default:
                    // Returning a number between 2 - 10
                    toRet =  suit.GetDescription() + rank;
                    break;
            }

            return toRet;
        }

        public int GetRank()
        {
            return rank;
        }


        public Suit GetSuit()
        {
            return suit;
        }
    }
}
