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

        private CardRank cardRank = CardRank.HighCard;

        public Hand(List<Card> h)
        {
            hand = h;
            sortAndRank();
        }

        public List<Card> getPair1(){
            return pair1;
        }
        public List<Card> getPair2()
        {
            return pair2;
        }
        public List<Card> getSet()
        {
            return set;
        }
        public List<Card> getQuad()
        {
            return quad;
        }
        public List<Card> getHand()
        {
            return hand;
        }

        public void setHand(List<Card> h)
        {
            hand.Clear();
            hand.AddRange(h);
            sortAndRank();
        }

        private void sortAndRank()
        {
            bool isFlush = true;
            bool isStraight = true;

            // clear before sorting 
            pair1.Clear();
            pair2.Clear();
            set.Clear();
            quad.Clear();
            // also default to lowest rank    
            cardRank = CardRank.HighCard;

            // hand must be sorted
            hand.Sort((x, y) => x.getRank().CompareTo(y.getRank()));

            int i = 0;
            foreach (Card c1 in hand)
            {
                i++;
                Card[] subList = hand.GetRange(i, hand.Count - i).ToArray();

                int j = 0;
                foreach (Card c2 in subList)
                {
                    j++;
                    if (c1.getSuit() != c2.getSuit())
                    {
                        isFlush = false;
                    }

                    if (c1.getRank() != c2.getRank() - j)
                    {
                        isStraight = false;
                    }

                    if (c1.getRank() == c2.getRank())
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
                                if (!pair1.Contains(c2) && pair1[0].getRank() == c2.getRank())
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
                                if (!set.Contains(c2) && set[0].getRank() == c2.getRank())
                                {
                                    // Quads
                                    cardRank = CardRank.Quads;
                                    quad.AddRange(set);
                                    quad.Add(c2);
                                    set.Clear();
                                }
                                else if (set[0].getRank() != c2.getRank())
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
                if (hand[4].getRank() == 14)
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

        public CardRank getRank()
        {
            return cardRank;
        }

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
