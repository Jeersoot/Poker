using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class CardDeck
    {
        private List<Card> deck = null;
        private int deckIndex = 0;

        public CardDeck()
        {
            deck = new List<Card>();
            CreateCards();
        }

        public CardDeck Clone()
        {
            return (CardDeck) this.MemberwiseClone();
        }
        public void Shuffle()
        {
            List<Card> shuffledDeck = new List<Card>();

            Random r = new Random();
            while (deck.Count > 0)
            {
                int randomIndex = r.Next(0, deck.Count);
                shuffledDeck.Add(deck[randomIndex]); //add it to the new, random list
                deck.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            deck = shuffledDeck;
            deckIndex = 0;
        }
        public List<Card> GetDeck()
        {
            return deck;
        }

        public List<Card> GetCards(int count)
        {
            //Checks how many new cards it is to be created
            List<Card> toRet = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                toRet.Add(deck[deckIndex++]);
            }

            return toRet;
        }
        /*
         * Creates the new cards
         */
        private void CreateCards()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int rank = 2; rank <= 14; rank++)
                {
                    deck.Add(new Card(rank, suit));
                }
            }
        }
    }
}
