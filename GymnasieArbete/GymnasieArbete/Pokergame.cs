using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class PokerGame
    {
        private List<Card> deck = null;
        private int deckIndex = 0;

        public PokerGame()
        {
            deck = new List<Card>();
            createCards();
        }
        public void Shuffle()
        {
            List<Card> shuffledDeck = new List<Card>();

            Random r = new Random();
            int randomIndex = 0;
            while (deck.Count > 0)
            {
                randomIndex = r.Next(0, deck.Count); //Choose a random object in the list
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

        public List<Card> getDeal()
        {
            return getCards(5);
        }

        public bool hasMoreCards()
        {
            if(deckIndex < deck.Count - 10)
            {
                return true;
            }
            return false;
        }

        public void replaceTrashCardsForHand(Hand hand)
        {
            List<Card> newHand = new List<Card>();
            switch (hand.getRank())
            {
                case CardRank.RoyalFlush:
                case CardRank.StraightFlush:
                case CardRank.Straight:
                case CardRank.Flush:
                case CardRank.Quads:
                case CardRank.FullHouse:
                    // I'm happy. Do nothing and exit here.
                    return;
                case CardRank.TwoPair:
                    // replace 1
                    newHand.AddRange(hand.getPair1());
                    newHand.AddRange(hand.getPair2());
                    newHand.AddRange(getCards(1));
                    break;
                case CardRank.Set:
                    // replace 2
                    newHand.AddRange(hand.getSet());
                    newHand.AddRange(getCards(2));
                    break;
                case CardRank.Pair:
                    // replace 3
                    newHand.AddRange(hand.getPair1());
                    newHand.AddRange(getCards(3));
                    break;
                case CardRank.HighCard:
                    // replace 5
                    newHand.AddRange(getCards(5));
                    break;
                default:
                    break;
            }
            hand.setHand(newHand);


        }

        private List<Card> getCards(int count)
        {
            List<Card> toRet = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                toRet.Add(deck[deckIndex++]);
            }

            return toRet;
        }

        private void createCards()
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
