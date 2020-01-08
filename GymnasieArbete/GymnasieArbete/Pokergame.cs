using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class Pokergame
    {
        private int volume = 0;
        private int balance = 0;
        private protected int wins = 0;
        private protected int losses = 0;
        private protected int calls = 0;
        private protected int folds = 0;
        private protected int draws = 0;
        private protected int nq = 0;
        private protected int noFourCardQuad = 0;
        private protected int noFourCardStraightFlush = 0;
        private protected int noFourCardSet = 0;
        private protected int noFourCardFlush = 0;
        private protected int noFourCardTwoPair = 0;
        private protected int noFourCardPair = 0;
        private protected int noFourCardHighCard = 0;

        public int GetBalance()
        {
            return balance;
        }
        public int GetWins()
        {
            return wins;
        }
        public int GetLosses()
        {
            return losses;
        }
        public int GetDraws()
        {
            return draws;
        }
        public int GetFolds()
        {
            return folds;
        }
        public int GetCalls()
        {
            return calls;
        }
        public int GetNq()
        {
            return nq;
        }
        public int GetVolume()
        {
            return volume;
        }

        private protected void UpdateBalance(int a)
        {
            balance += a;

            if (a > 0)
            {
                volume += a;
            }
        }

        public String GetHousesEdge()
        {
           double edge =  Convert.ToDouble(balance) * 100 / Convert.ToDouble(volume);
           return Math.Round(edge, 2).ToString() + "%";
        }

        // Collects the total number of times every combination is in a game
        
            // Four Card Poker
        public int GetNoFourCardQuad() 
        {
            return noFourCardQuad;
        }
        public int GetNoFourCardStraightFlush()
        {
            return noFourCardStraightFlush;
        }
        public int GetNoFourCardSet() 
        {
            return noFourCardSet;
        }
        public int GetNoFourCardFlush() 
        {
            return noFourCardFlush;
        }
        public int GetNoFourCardTwoPair() 
        {
            return noFourCardTwoPair;
        }
        public int GetNoFourCardPair() 
        {
            return noFourCardPair;
        }
        public int GetNoFourCardHighCard() 
        {
            return noFourCardHighCard;
        }

        public static Hand SortAndRankHand(Hand hand)
        {
            bool isFlush = true;
            bool isStraight = true;

            // Clear old rank
            hand.ResetHandRank();

            // sort
            hand.Sort();

            // Check if pair or similar (doesn't check Straight nor Flush)
            List<Card> cards = hand.GetCards();
            int i = 0;
            foreach (Card c1 in cards)
            {
                i++;
                Card[] subList = cards.GetRange(i, cards.Count - i).ToArray();

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
                        switch (hand.GetRank())
                        {
                            case CardRank.HighCard:
                                // Pair
                                hand.SetRank(CardRank.Pair);
                                if (hand.GetPair1().Count == 0)
                                {
                                    hand.GetPair1().Add(c1);
                                    hand.GetPair1().Add(c2);
                                }
                                break;
                            case CardRank.Pair:
                                if (!hand.GetPair1().Contains(c2) && hand.GetPair1()[0].GetRank() == c2.GetRank())
                                {
                                    // Set
                                    hand.SetRank(CardRank.Set);
                                    hand.GetSet().AddRange(hand.GetPair1());
                                    hand.GetSet().Add(c2);
                                    hand.GetPair1().Clear();
                                }
                                else
                                {
                                    hand.SetRank(CardRank.TwoPair);
                                    hand.GetPair2().Add(c1);
                                    hand.GetPair2().Add(c2);
                                }
                                break;
                            case CardRank.Set:
                                if (!hand.GetSet().Contains(c2) && hand.GetSet()[0].GetRank() == c2.GetRank())
                                {
                                    // Quads
                                    hand.SetRank(CardRank.Quads);
                                    hand.GetQuad().AddRange(hand.GetSet());
                                    hand.GetQuad().Add(c2);
                                    hand.GetSet().Clear();
                                }
                                else if (hand.GetSet()[0].GetRank() != c2.GetRank())
                                {
                                    // Full House
                                    hand.SetRank(CardRank.FullHouse);
                                    hand.GetPair1().Add(c1);
                                    hand.GetPair1().Add(c2);
                                }
                                break;
                            case CardRank.Quads:
                                break;
                            default:
                                hand.SetRank(CardRank.HighCard);
                                break;
                        }
                    }
                }
            }

            if (isFlush && isStraight)
            {
                // Checks if the last card is an Ace
                if (cards[4].GetRank() == Card.ACE)
                {
                    hand.SetRank(CardRank.RoyalFlush);
                }
                else
                {
                    hand.SetRank(CardRank.StraightFlush);
                }
            }
            else if (isFlush)
            {
                hand.SetRank(CardRank.Flush);
            }
            else if (isStraight)
            {
                hand.SetRank(CardRank.Straight);
            }

            return hand;
        }
    }
}
