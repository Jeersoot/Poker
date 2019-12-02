using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class Hand
    {
        private List<Card> hand = null;
        private List<Card> pair1 = new List<Card>();
        private List<Card> pair2 = new List<Card>();
        private List<Card> set = new List<Card>();
        private List<Card> quad = new List<Card>();

        private Card upCard = null;

        private CardRank cardRank = CardRank.HighCard;

        public Hand(List<Card> h)
        {
            hand = h;
            upCard = hand[0];
        }

        public List<Card> GetPair1()
        {
            return pair1;
        }
        public List<Card> GetPair2()
        {
            return pair2;
        }
        public List<Card> GetSet()
        {
            return set;
        }
        public List<Card> GetQuad()
        {
            return quad;
        }
        public List<Card> GetCards()
        {
            return hand;
        }
        public Card GetUpCard()
        {
            return upCard;
        }

        public void SetCards(List<Card> h)
        {
            hand.Clear();
            hand.AddRange(h);
        }

        public void ResetHandRank()
        {
            // Clear before sorting 
            pair1.Clear();
            pair2.Clear();
            set.Clear();
            quad.Clear();

            // Also default to lowest rank    
            cardRank = CardRank.HighCard;

        }

        public void Sort()
        {
            // Hand must be sorted
            hand.Sort((x, y) => x.GetRank().CompareTo(y.GetRank()));
        }

        public CardRank GetRank()
        {
            return cardRank;
        }

        public void SetRank(CardRank rank)
        {
           cardRank = rank;
        }
  
        /*
         * Skriver ut handen i en sorterad ordning
         */
        public override string ToString()
        {
            String toRet = "";
            switch (cardRank)
            {
                case CardRank.Pair:
                    foreach (Card c in pair1)
                    {
                        toRet += c + " ";
                    }

                    foreach (Card c in hand)
                    {
                        if (!pair1.Contains(c))
                        {
                            toRet += c + " ";
                        }
                    }
                    break;

                case CardRank.TwoPair:
                    foreach (Card c in pair1)
                    {
                        toRet += c + " ";
                    }
                    foreach (Card c in pair2)
                    {
                        toRet += c + " ";
                    }
                    foreach (Card c in hand)
                    {
                        if (!pair1.Contains(c) && !pair2.Contains(c))
                        {
                            toRet += c + " ";
                        }
                    }
                    break;

                case CardRank.Set:
                    foreach (Card c in set)
                    {
                        toRet += c + " ";
                    }

                    foreach (Card c in hand)
                    {
                        if (!set.Contains(c))
                        {
                            toRet += c + " ";
                        }
                    }
                    break;

                case CardRank.Quads:
                    foreach (Card c in quad)
                    {
                        toRet += c + " ";
                    }
                    foreach (Card c in hand)
                    {
                        if (!quad.Contains(c))
                        {
                            toRet += c + " ";
                        }
                    }
                    break;

                case CardRank.FullHouse:
                    foreach (Card c in set)
                    {
                        toRet += c + " ";
                    }

                    foreach (Card c in pair1)
                    {
                        toRet += c + " ";
                    }
                    break;

                default:
                    foreach (Card c in hand)
                    {
                        toRet += c + " ";
                    }
                    break;
            }
            return toRet;
        }
    }
}
