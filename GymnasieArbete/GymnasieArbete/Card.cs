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

        private int rank;
        private Suit suit;

        public Card(int r, Suit s)
        {
            rank = r;
            suit = s;
        }
        public override String ToString()
        {
            // Check if a card is a face card or no
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
