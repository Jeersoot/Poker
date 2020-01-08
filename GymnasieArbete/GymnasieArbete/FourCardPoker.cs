using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class FourCardPoker : Pokergame
    {
        private Hand dealerHand = null;
        private bool playAcesUp = false;

        public FourCardPoker()
        {
        }
        public FourCardPoker(bool b)
        {
            playAcesUp = b;
        }


        public void Play(Player player, CardDeck deck)
        {

            int ante = 1;
            int bid;

            //Console.Write(player.GetHand() + "vs " + dealerHand + "- " + player.GetHand().GetRank() + " -");

            // Now start to play
            player.Debitbalance(ante);
            UpdateBalance(ante);

            bid = GetRaiseBet(player.GetHand());

            if (bid > 0)
            {
                player.Debitbalance(bid);
                player.HandleCalls();
                calls++;
                UpdateBalance(bid);

                if (player.GetHand().GetRank() > dealerHand.GetRank())
                {
                    // Player wins, pay out bid 1-to-1, plus initial ante*2
                    player.Creditbalance((bid + ante) * 2);
                    player.HandleWins();
                    UpdateBalance(-(bid + ante) * 2);
                    // Casino loses
                    losses++;
                    //Console.Write(" win, bid = " + bid);
                }
                else if (player.GetHand().GetRank() < dealerHand.GetRank())
                {
                    // Player lose
                    player.HandleLoss();

                    // Casino wins
                    wins++;
                    //Console.Write(" loss, bid = " + bid);
                }
                else
                {
                    // Hands are equal. Compare card by card 
                    int res = CompareHands(dealerHand, player.GetHand());
                    if (res == 1)
                    {
                        // Player lose
                        player.HandleLoss();
                        // Casino wins
                        wins++;
                        //Console.Write(" loss, bid = " + bid);
                    }
                    else
                    {
                        player.Creditbalance((bid + ante) * 2);
                        player.HandleWins();
                        UpdateBalance(-(bid + ante) * 2);
                        // Casino loses
                        losses++;
                        //Console.Write(" win, bid = " + bid);
                    } 
                }
            }
            else
            {
                // Player folds
                player.HandleFold();
                folds++;
                //Console.Write(" fold");
            }

            if (playAcesUp) 
            {
                // If the player has a Pair of Aces or better, then he shall also be paid after the Aces Up Pay Table
                int acesUp = AcesUpTable(player.GetHand());
                player.Creditbalance(acesUp);
                UpdateBalance(-(acesUp));

                //Console.WriteLine(", Aces Up = " + acesUp + ", balance = " + GetBalance());
            } 
            
            int  AcesUpTable(Hand hand)
            {
                int toRet = -1;
                switch (hand.GetRank())
                {
                    case CardRank.FOURCARD_Quads:
                        toRet = 50;
                        break;
                    case CardRank.FOURCARD_StraightFlush:
                        toRet = 40;
                        break;
                    case CardRank.FOURCARD_Set:
                        toRet = 8;
                        break;
                    case CardRank.FOURCARD_Flush:
                        toRet = 5;
                        break;
                    case CardRank.FOURCARD_Straight:
                        toRet = 4;
                        break;
                    case CardRank.FOURCARD_TwoPair:
                        toRet = 3;
                        break;
                    case CardRank.FOURCARD_Pair:
                        int playersPairRank = hand.GetPair1()[0].GetRank();
                        if (playersPairRank == Card.ACE)
                        {
                            toRet = 1;
                        }
                        break;
                    case CardRank.FOURCARD_HighCard:
                        toRet = -1;
                        break;
                    default:
                        break;
                }
                return toRet;
            }

            // If the player has at least a three of a kind, then he shall also be paid a Bonus, regardless of the value of the dealer's hand. 
            if (player.GetHand().GetRank() >= CardRank.FOURCARD_Set)
            {
                int bonus = BonusTable(player.GetHand());
                player.Creditbalance(bonus);
                player.HandleWins();
                UpdateBalance(-(bonus));
                
                //Console.WriteLine(", bonus = " + bonus);
            } 
            else
            {
                //Console.WriteLine(", no bonus");
            }
        }

        private int BonusTable(Hand hand)
        {
            int toRet = 0;
            switch (hand.GetRank())
            {
                case CardRank.FOURCARD_Quads:
                    toRet = 25;
                    break;
                case CardRank.FOURCARD_StraightFlush:
                    toRet = 20;

                    break;
                case CardRank.FOURCARD_Set:
                    toRet = 2;
                    break;
                default:
                    break;
            }
             return toRet;
        }

        /*
         *     1 - Pair of Aces or better: Bet 3X
         *     2 - Pair of Ks: Bet 3X, except bet 1X against an Ace and you don't have an Ace nor 4.
         *     3 - Pair of Js or Qs: Bet 3X, except bet 1X if the dealer's card outranks pair your pair rank and does not match a singleton in your hand.
         *     4 - Pair of 9s or 10s: Bet 3X, except bet 1X if dealer card outranks your pair rank.
         *     5 - Pair of 8s: Bet 1X, except bet 3X against a 2
         *     6 - Pair of 4s thru 7s: Bet 1X
         *     7 - Pair of 3s: Bet 1X, except fold against a Jack if your highest kicker is a 10 or lower
         *     8 - Pair of 2s or AKQ: Fold, except bet 1X if dealer card matches a rank in your hand
         *     9 - AKJT: Fold, except bet 1X against a Jack
         *     10 - AKJ9 or lower: Fold 
         */
        private int GetRaiseBet(Hand hand)
        {
            int toRet = 1;
            switch (hand.GetRank())
            {
                case CardRank.FOURCARD_HighCard:
                    if(hand.GetCards()[0].GetRank() == Card.TEN &&
                        hand.GetCards()[1].GetRank() == Card.JACK &&
                        hand.GetCards()[2].GetRank() == Card.KING &&
                        hand.GetCards()[3].GetRank() == Card.ACE &&
                        dealerHand.GetUpCard().GetRank() == Card.JACK)
                    {
                        toRet = 1; // 9
                        //Console.Write("; Strategy case 9, dealersUp: " + dealerHand.GetUpCard() + ";"); 
                       
                    } else
                    {
                        toRet = 0; // 10
                        //Console.Write("; Strategy case 10, dealersUp: " + dealerHand.GetUpCard() + ";");
                    }
                    break;
                case CardRank.FOURCARD_Pair:

                    int playersPairRank = hand.GetPair1()[0].GetRank();
                    if (playersPairRank == Card.ACE)
                    {
                        toRet = 3; // 1
                        //Console.Write("; Strategy case 1, dealersUp: " + dealerHand.GetUpCard() + ";");

                    }
                    else if (playersPairRank >= Card.JACK)
                    {
                        bool dealersUpInPlayersHand = false;
                        if (dealerHand.GetUpCard().GetRank() < playersPairRank)
                        {
                            toRet = 3; // 2, 3   

                        }
                        else
                        {
                            // Ok, check if Player has a card with the same rank as dealersUp. 
                            foreach (Card c in hand.GetCards())
                            {
                                if (c.GetRank() == dealerHand.GetUpCard().GetRank())
                                {
                                    dealersUpInPlayersHand = true;
                                    break;
                                }
                            }
                            toRet = dealersUpInPlayersHand ? 3 : 1;
                        }
                        //Console.Write("; Strategy case 2 or 3, dealersUp: " + dealerHand.GetUpCard() + ";");
                    }
                    else if (playersPairRank == Card.NINE || playersPairRank == Card.TEN)
                    {
                        if (dealerHand.GetUpCard().GetRank() <= playersPairRank)
                        {
                            toRet = 3; // 4
                            //Console.Write("; Strategy case 4, dealersUp: " + dealerHand.GetUpCard() + ";");

                        }
                        else
                        {
                            toRet = 1;
                            //Console.Write("; Strategy case 4, dealersUp: " + dealerHand.GetUpCard() + ";");
                        }
                    }
                    else if (playersPairRank == Card.EIGHT)
                    {
                        if (dealerHand.GetUpCard().GetRank() == Card.TWO)
                        {
                            toRet = 3; // 5
                            //Console.Write("; Strategy case 5, dealersUp: " + dealerHand.GetUpCard() + ";");

                        }
                        else
                        {
                            toRet = 1;
                            //Console.Write("; Strategy case 5, dealersUp: " + dealerHand.GetUpCard() + ";");
                        }
                    }
                    else if (playersPairRank == Card.SEVEN ||
                      playersPairRank == Card.SIX ||
                      playersPairRank == Card.FIVE ||
                      playersPairRank == Card.FOUR)
                         {
                        toRet = 1; // 6
                        //Console.Write("; Strategy case 6, dealersUp: " + dealerHand.GetUpCard() + ";");
                         }

                    else if (playersPairRank == Card.THREE)
                    {
                        toRet = 1; // 7
                        //Console.Write("; Strategy case 7, dealersUp: " + dealerHand.GetUpCard() + ";");

                        if (dealerHand.GetUpCard().GetRank() >= Card.JACK)
                        {
                            toRet = 0;
                            for(int i = 3; i>=0; i--)
                            {
                                Card c = hand.GetCards()[i];
                                if (hand.GetPair1().Contains(c))
                                {
                                    continue;
                                } 
                                
                                if(c.GetRank() >= dealerHand.GetUpCard().GetRank())
                                {
                                    toRet = 1;
                                    break;
                                }
                            }
                        }
                    }
                    else if (playersPairRank == Card.TWO)
                    {
                        toRet = 0; // 8
                        //Console.Write("; Strategy case 8, dealersUp: " + dealerHand.GetUpCard() + ";");

                        foreach (Card c in hand.GetCards())
                        {
                            if (c.GetRank() == dealerHand.GetUpCard().GetRank())
                            {
                                toRet = 1;
                            }
                        }
                    }
                    break;
                case CardRank.FOURCARD_TwoPair:
                case CardRank.FOURCARD_Flush:
                case CardRank.FOURCARD_Set:
                case CardRank.FOURCARD_Quads:
                case CardRank.FOURCARD_Straight:
                case CardRank.FOURCARD_StraightFlush:
                    toRet = 3;  // 1
                    //Console.Write("; Strategy case 1, dealersUp: " + dealerHand.GetUpCard()+";");
                    break;
                default:
                    toRet = 0;
                    break;
            }

            return toRet;
        }

        public void SetHand(Hand h)
        {
            dealerHand = h;
        }

        private int PayOutTable(Hand hand)
        {
            int toRet = 0;
            switch (hand.GetRank())
            {
                case CardRank.FOURCARD_Quads:
                    toRet = 50;
                    break;
                case CardRank.FOURCARD_StraightFlush:
                    toRet = 40;
                    break;
                case CardRank.FOURCARD_Set:
                    toRet = 9;
                    break;
                case CardRank.FOURCARD_Flush:
                    toRet = 6;
                    break;
                case CardRank.FOURCARD_Straight:
                    toRet = 4;
                    break;
                case CardRank.FOURCARD_TwoPair:
                    toRet = 2;
                    break;
                case CardRank.FOURCARD_Pair:
                    toRet = 1;
                    break;
                case CardRank.FOURCARD_HighCard:
                    toRet = 1;
                    break;
                default:
                    break;
            }
            return toRet;
        }
 
 
        private int CompareHands(Hand h1, Hand h2)
        {
            int toRet = 0;
            switch (h1.GetRank())
            {
                case CardRank.FOURCARD_Quads:
                    if (h1.GetQuad()[0].GetRank() > h2.GetQuad()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else
                    {
                        toRet = -1;
                    }
                    break;
                case CardRank.FOURCARD_StraightFlush:
                    if (h1.GetCards()[3].GetRank() > h2.GetCards()[3].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetCards()[3].GetRank() < h2.GetCards()[3].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.FOURCARD_Set:
                    if (h1.GetSet()[0].GetRank() > h2.GetSet()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else
                    {
                        toRet = -1;
                    }
                    break;
                case CardRank.FOURCARD_Flush:
                case CardRank.FOURCARD_Straight:
                    if (h1.GetCards()[0].GetRank() > h2.GetCards()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetCards()[0].GetRank() < h2.GetCards()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.FOURCARD_TwoPair:
                    toRet = 0;
                    if (h1.GetPair2()[0].GetRank() > h2.GetPair2()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetPair2()[0].GetRank() < h2.GetPair2()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        if (h1.GetPair1()[0].GetRank() > h2.GetPair1()[0].GetRank())
                        {
                            toRet = 1;
                        }
                        else if (h1.GetPair1()[0].GetRank() < h2.GetPair1()[0].GetRank())
                        {
                            toRet = -1;
                        }
                    }
                    break;
                case CardRank.FOURCARD_Pair:
                    if (h1.GetPair1()[0].GetRank() > h2.GetPair1()[0].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetPair1()[0].GetRank() < h2.GetPair1()[0].GetRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.FOURCARD_HighCard:
                    for (int i = 3; i > 0; i--)
                    {
                        if (h1.GetCards()[i].GetRank() > h2.GetCards()[i].GetRank())
                        {
                            toRet = 1;
                            break;
                        }
                        else if (h1.GetCards()[i].GetRank() < h2.GetCards()[i].GetRank())
                        {
                            toRet = -1;
                            break;
                        }
                    }
                    toRet = 0;
                    break;
                default:
                    break;
            }

            return toRet;
            
        }
        public Hand GetDealerHand()
        {
            return dealerHand;
        }


        public static Hand SortAndRankHandForFourCard(Hand hand)
        {

            List<Card> clubs = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> spades = new List<Card>();
            List<Card> diamonds = new List<Card>();
            List<Card> pair1 = new List<Card>();
            List<Card> pair2 = new List<Card>();
            List<Card> pair3 = new List<Card>();
            List<Card> set1 = new List<Card>();
            List<Card> set2 = new List<Card>();
            List<Card> quads = new List<Card>();
            List<Card> straight = new List<Card>();

            // Clear old rank
            hand.ResetHandRank();

            // sort
            hand.Sort();


            // Check if pair or similar (doesn't check Straight nor Flush)
            List<Card> cards = hand.GetCards();


            foreach (Card c in cards)
            {
                switch (c.GetSuit())
                {
                    case Suit.Heart:
                        hearts.Add(c);
                        break;
                    case Suit.Spade:
                        spades.Add(c);
                        break;
                    case Suit.Diamond:
                        diamonds.Add(c);
                        break;
                    case Suit.Club:
                        clubs.Add(c);
                        break;
                    default:
                        break;
                }
            }

            int i = 0;
            foreach (Card c1 in cards)
            {
                i++;
                Card[] subList = cards.GetRange(i, cards.Count - i).ToArray();

                int j = 0;
                foreach (Card c2 in subList)
                {
                    j++;
                    if (straight.Count == 0)
                    {
                        if ((c1.GetRank() + 1 == c2.GetRank()))
                        {
                            straight.Add(c1);
                            straight.Add(c2);
                        }
                    }
                    else
                    {
                        if (straight[straight.Count - 1].GetRank() + 1 == c2.GetRank())
                        {
                            straight.Add(c2);
                        }
                        else if (straight[straight.Count - 1].GetRank() + 1 < c2.GetRank() && straight.Count < 4)
                        {
                            straight.Clear();
                            straight.Add(c2);
                        }
                    }

                    if (c1.GetRank() == c2.GetRank() && quads.Count == 0)
                    {
                        if (set1.Count > 0)
                        {
                            if (!set1.Contains(c2) && set1[0].GetRank() == c2.GetRank())
                            {
                                quads.AddRange(set1);
                                quads.Add(c2);
                                set1.Clear();
                                break;
                            }
                        }

                        if (pair1.Count > 0)
                        {
                            if (!pair1.Contains(c2) && pair1[0].GetRank() == c2.GetRank())
                            {
                                if (set1.Count == 0)
                                {
                                    set1.AddRange(pair1);
                                    set1.Add(c2);
                                    pair1.Clear();
                                    continue;
                                }
                                else if (set2.Count == 0)
                                {
                                    set2.AddRange(pair1);
                                    set2.Add(c2);
                                    pair1.Clear();
                                    continue;
                                }
                            }
                        }

                        if (pair2.Count > 0)
                        {
                            if (pair2.Contains(c2) && pair2[0].GetRank() == c2.GetRank())
                            {
                                if (set1.Count == 0)
                                {
                                    set1.AddRange(pair1);
                                    set1.Add(c2);
                                    pair2.Clear();
                                    continue;
                                }
                                else if (set2.Count == 0)
                                {
                                    set2.AddRange(pair2);
                                    set2.Add(c2);
                                    pair2.Clear();
                                    continue;
                                }
                            }
                        }

                        if (pair1.Count == 0)
                        {
                            pair1.Add(c1);
                            pair1.Add(c2);
                        }
                        else if (pair2.Count == 0)
                        {
                            pair2.Add(c1);
                            pair2.Add(c2);
                        }
                        else
                        {
                            pair3.Add(c1);
                            pair3.Add(c2);
                        }
                    }
                }
            }

            // Ok, cards have been organized. Now let's decide rank 

            // Quads
            if(quads.Count == 4)
            {
                hand.SetRank(CardRank.FOURCARD_Quads);
                hand.SetCards(quads);
                hand.GetQuad().AddRange(quads);
                return hand;
            }

            // Straight Flush
            if (straight.Count >= 4)
            {
                List<Card> straightFlush = new List<Card>();
                if(clubs.Count >= 4)
                {
                    Card precedingCard = null;
                    foreach (Card c in straight)
                    {    
                        if (clubs.Contains(c))
                        {
                            if(precedingCard == null || precedingCard.GetRank() + 1 == c.GetRank())
                            {
                                straightFlush.Add(c);
                            } else
                            {
                                straightFlush.Clear();
                                straightFlush.Add(c);
                            }
                            precedingCard = c;
                        }
                    }
                }
                else if (hearts.Count >= 4)
                {
                    Card precedingCard = null;
                    foreach (Card c in straight)
                    {
                        if (hearts.Contains(c))
                        {
                            if (precedingCard == null || precedingCard.GetRank() + 1 == c.GetRank())
                            {
                                straightFlush.Add(c);
                            }
                            else
                            {
                                straightFlush.Clear();
                                straightFlush.Add(c);
                            }
                            precedingCard = c;
                        }
                    }
                }
                else if (diamonds.Count >= 4)
                {
                    Card precedingCard = null;
                    foreach (Card c in straight)
                    {
                        if (diamonds.Contains(c))
                        {
                            if (precedingCard == null || precedingCard.GetRank() + 1 == c.GetRank())
                            {
                                straightFlush.Add(c);
                            }
                            else
                            {
                                straightFlush.Clear();
                                straightFlush.Add(c);
                            }
                            precedingCard = c;
                        }
                    }
                }
                else if (spades.Count >= 4)
                {
                    Card precedingCard = null;
                    foreach (Card c in straight)
                    {
                        if (spades.Contains(c))
                        {
                            if (precedingCard == null || precedingCard.GetRank() + 1 == c.GetRank())
                            {
                                straightFlush.Add(c);
                            }
                            else
                            {
                                straightFlush.Clear();
                                straightFlush.Add(c);
                            }
                            precedingCard = c;
                        }
                    }
                }

                if (straightFlush.Count >= 4)
                {
                    hand.SetRank(CardRank.FOURCARD_StraightFlush);
                    hand.SetCards(straightFlush.GetRange(straightFlush.Count - 4, 4));
                    return hand;
                }
            }

            // Set
            if (set2.Count == 3 || set1.Count == 3)
            {                
                List<Card> newHand = new List<Card>();
                List<Card> set = null;
                hand.SetRank(CardRank.FOURCARD_Set);
                
                if(set2.Count == 3)
                {
                    set = set2;
                } else
                {
                    set = set1;
                }
                newHand.AddRange(set);

                for (int j = hand.GetCards().Count -1; j > 0; j--)
                {
                    if (!set.Contains(hand.GetCards()[j]))
                    {
                        newHand.Add(hand.GetCards()[j]);
                        break;
                    }
                }

                hand.SetCards(newHand);
                hand.GetSet().AddRange(set);
                return hand;
            }

            // Flush
            if(clubs.Count >= 4)
            {
                hand.SetRank(CardRank.FOURCARD_Flush);
                hand.SetCards(clubs.GetRange(clubs.Count - 4, 4));
                return hand;
            }
            else if (hearts.Count >= 4)
            {
                hand.SetRank(CardRank.FOURCARD_Flush);
                hand.SetCards(hearts.GetRange(hearts.Count - 4, 4));
                return hand;
            }
            else if (diamonds.Count >= 4)
            {
                hand.SetRank(CardRank.FOURCARD_Flush);
                hand.SetCards(diamonds.GetRange(diamonds.Count - 4, 4));
                return hand;
            }
            else if (spades.Count >= 4)
            {
                hand.SetRank(CardRank.FOURCARD_Flush);
                hand.SetCards(spades.GetRange(spades.Count - 4, 4));
                return hand;
            }

            // Straight
            if (straight.Count >= 4)
            {
                hand.SetRank(CardRank.FOURCARD_Straight);
                hand.SetCards(straight.GetRange(straight.Count - 4, 4));
                return hand;
            }


            if (pair3.Count == 2 && pair2.Count == 2)
            {
                hand.SetRank(CardRank.FOURCARD_TwoPair);
                hand.GetPair1().AddRange(pair2);
                hand.GetPair2().AddRange(pair3);

                hand.SetCards(pair2);
                hand.GetCards().AddRange(pair3);
                return hand;
            }
            else if(pair2.Count == 2 && pair1.Count == 2)
            {
                hand.SetRank(CardRank.FOURCARD_TwoPair);
                hand.GetPair1().AddRange(pair1);
                hand.GetPair2().AddRange(pair2);

                hand.SetCards(pair1);
                hand.GetCards().AddRange(pair2);
                return hand;
            }

            // Pair
            if (pair1.Count == 2)
            {
                List<Card> newHand = new List<Card>();
                hand.SetRank(CardRank.FOURCARD_Pair);
                hand.GetPair1().AddRange(pair1);
                newHand.AddRange(pair1);

                for (int j = hand.GetCards().Count -1; j > 0; j--)
                {
                    if (!pair1.Contains(hand.GetCards()[j]))
                    {
                        newHand.Add(hand.GetCards()[j]);
                        if(newHand.Count == 4)
                        {
                            break;
                        }
                    }
                }

                hand.SetCards(newHand);
                hand.Sort();
                return hand;
            }

            hand.SetRank(CardRank.FOURCARD_HighCard);
            hand.SetCards(hand.GetCards().GetRange(hand.GetCards().Count - 4, 4));
            return hand;
        }
    }
}
