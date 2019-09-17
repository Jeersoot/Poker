using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Player
    {
        private List<Card> cards = new List<Card>();
        private int handValue = 0;
        private bool changed = false; //If the handValue has changed;

        public int Bet { get; private set; } = 0;
        public int HandValue
        {
            get
            {
                if (changed)
                {
                    handValue = GetHandValue();
                }

                return handValue;
            }

            private set
            {
                changed = true;
                handValue = value;
            }
        }

        public Player(List<Card> cards) : base()
        {
            cards.ForEach(x => cards.Add(x));//Starting cards
        }

        public Player()
        {

        }

        public void GiveCards(List<Card> cards)
        {
            //Deals the player cards, this is used when the game wants to give cards to a player.
            cards.ForEach(x => this.cards.Add(x));
        }

        private int GetHandValue()
        {
            return cards.Aggregate(0, (a, b) => a + b.Value); //Simply an accumilator in a lambda. This adds the value of every card to the handValue.
        }
    }
}
