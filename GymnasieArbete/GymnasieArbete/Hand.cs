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
            SortAndRank();
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
        public List<Card> GetHand()
        {
            return hand;
        }

        public Card GetUpCard()
        {
            return upCard;
        }

        public void SetHand(List<Card> h)
        {
            hand.Clear();
            hand.AddRange(h);
            SortAndRank();
        }

        public void SortAndRank()
        {
            // Assumes the hand is Straight or Flush
            bool isFlush = true;
            bool isStraight = true;

            // Clear before sorting 
            pair1.Clear();
            pair2.Clear();
            set.Clear();
            quad.Clear();
            // Also default to lowest rank    
            cardRank = CardRank.HighCard;

            // Hand must be sorted
            hand.Sort((x, y) => x.GetRank().CompareTo(y.GetRank()));

            /*
             * Check if pair or similar (doesn't check Straight nor Flush)
             */

            int i = 0;
            foreach (Card c1 in hand)
            {
                i++;
                Card[] subList = hand.GetRange(i, hand.Count - i).ToArray();

                int j = 0;
                foreach (Card c2 in subList)
                {
                    j++;
                    // Checks if card1 (c1) and card2 (c2) has the same suit
                    if (c1.GetSuit() != c2.GetSuit())
                    {
                        isFlush = false;
                    }

                    // Checks if c1 has a value of j lower than c2
                    if (c1.GetRank() != c2.GetRank() - j)
                    {
                        isStraight = false;
                    }

                    if (c1.GetRank() == c2.GetRank())
                    {
                        switch (cardRank)
                        {
                            case CardRank.HighCard:
                                // Pair
                                cardRank = CardRank.Pair;
                                if (pair1.Count == 0)
                                {
                                    pair1.Add(c1);
                                    pair1.Add(c2);
                                }
                                break;
                            case CardRank.Pair:
                                if (!pair1.Contains(c2) && pair1[0].GetRank() == c2.GetRank())
                                {
                                    // Set
                                    cardRank = CardRank.Set;
                                    set.AddRange(pair1);
                                    set.Add(c2);
                                    pair1.Clear();
                                }
                                else
                                {
                                    cardRank = CardRank.TwoPair;
                                    pair2.Add(c1);
                                    pair2.Add(c2);
                                }
                                break;
                            case CardRank.Set:
                                if (!set.Contains(c2) && set[0].GetRank() == c2.GetRank())
                                {
                                    // Quads
                                    cardRank = CardRank.Quads;
                                    quad.AddRange(set);
                                    quad.Add(c2);
                                    set.Clear();
                                }
                                else if (set[0].GetRank() != c2.GetRank())
                                {
                                    // Full House
                                    cardRank = CardRank.FullHouse;
                                    pair1.Add(c1);
                                    pair1.Add(c2);
                                }
                                break;
                            case CardRank.Quads:
                                break;
                            default:
                                cardRank = CardRank.HighCard;
                                break;
                        }
                    }
                }
            }

            if (isFlush && isStraight)
            {
                // Checks if the last card is an Ace
                if (hand[4].GetRank() == 14)
                {
                    cardRank = CardRank.RoyalFlush;
                }
                else
                {
                    cardRank = CardRank.StraightFlush;
                }
            }
            else if (isFlush)
            {
                cardRank = CardRank.Flush;
            }
            else if (isStraight)
            {
                cardRank = CardRank.Straight;
            }
        }

        public CardRank GetRank()
        {
            return cardRank;
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

        /*
         * OXXXX - A
         * XOXXX - B
         * XXOXX - C
         * XXXOX - D 
         * XXXX0 - E 
         */
        private void OneCardFromStraightOrFlush(Hand h)
        {
            // A
            if(h.GetHand()[1].GetSuit() == h.GetHand()[2].GetSuit() &&
                h.GetHand()[1].GetSuit() == h.GetHand()[3].GetSuit() &&
                h.GetHand()[1].GetSuit() == h.GetHand()[4].GetSuit()) {

            }

            // B
            if (h.GetHand()[0].GetSuit() == h.GetHand()[2].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[3].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[4].GetSuit())
            {

            }

            // C
            if (h.GetHand()[0].GetSuit() == h.GetHand()[1].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[3].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[4].GetSuit())
            {

            }

            // D
            if (h.GetHand()[0].GetSuit() == h.GetHand()[1].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[2].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[4].GetSuit())
            {

            }

            // E
            if (h.GetHand()[0].GetSuit() == h.GetHand()[1].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[2].GetSuit() &&
                h.GetHand()[0].GetSuit() == h.GetHand()[3].GetSuit())
            {

            }

        }
    }
}
