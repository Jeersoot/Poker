using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class FourCardPoker : Pokergame
    {
        private Hand dealerHand = null;

        public FourCardPoker()
        {

        }

      public void Play(Player player, CardDeck deck) 
        {


            //TODO


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
                    } else
                    {
                        toRet = 0; // 10
                    }
                    break;
                case CardRank.FOURCARD_Pair:

                    int playersPairRank = hand.GetPair1()[0].GetRank();
                    if (playersPairRank == Card.ACE)
                    {
                        toRet = 3; // 1
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
                        }
                        toRet = dealersUpInPlayersHand ? 3 : 1;
                    }
                    else if (playersPairRank == Card.NINE || playersPairRank == Card.TEN)
                    {
                        if (dealerHand.GetUpCard().GetRank() < playersPairRank)
                        {
                            toRet = 3; // 4
                        }
                        else
                        {
                            toRet = 1;
                        }
                    }
                    else if (playersPairRank == Card.EIGHT)
                    {
                        if (dealerHand.GetUpCard().GetRank() == Card.TWO)
                        {
                            toRet = 3; // 5
                        }
                        else
                        {
                            toRet = 1;
                        }
                    }
                    else if (playersPairRank == Card.SEVEN ||
                      playersPairRank == Card.SIX ||
                      playersPairRank == Card.FIVE ||
                      playersPairRank == Card.FOUR)
                    {
                        toRet = 1; // 6
                    }
                    else if (playersPairRank == Card.THREE)
                    {
                        toRet = 1; // 7
                        if (dealerHand.GetUpCard().GetRank() == Card.JACK)
                        {
                            if (hand.GetCards()[4].GetRank() <= Card.TEN)
                            {
                                toRet = 0;
                            }
                        }
                    }
                    else if (playersPairRank == Card.TWO)
                    {
                        toRet = 0; // 8
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
                case CardRank.FOURCARD_Set:
                case CardRank.FOURCARD_Quads:
                case CardRank.FOURCARD_Straight:
                case CardRank.FOURCARD_StraightFlush:
                    toRet = 3;  // 1
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
                case CardRank.Quads:
                    toRet = 50;
                    break;
                case CardRank.StraightFlush:
                    toRet = 40;
                    break;
                case CardRank.Set:
                    toRet = 9;
                    break;
                case CardRank.Flush:
                    toRet = 6;
                    break;
                case CardRank.Straight:
                    toRet = 4;
                    break;
                case CardRank.TwoPair:
                    toRet = 2;
                    break;
                case CardRank.Pair:
                    toRet = 1;
                    break;
                case CardRank.HighCard:
                    toRet = 1;
                    break;
                default:
                    break;
            }
            return toRet;
        }
        public void Play(Player player)
        {
            int ante = 1;
            int bid = 2;

            Console.Write(player.GetHand() + "vs " + dealerHand + "- " + player.GetHand().GetRank() + " -");

            // Now start to play
            player.Debitbalance(ante);
            balance += ante;

            if (DoCall(player.GetHand()))
            {
                player.Debitbalance(bid);
                player.HandleCalls();
                balance += bid;

                    if (player.GetHand().GetRank() > dealerHand.GetRank())
                    {
                        int odds = PayOutTable(player.GetHand());
                        // Player wins, pay out bid * odds, plus initial ante*2 and bid
                        player.Creditbalance(odds * bid + ante * 2 + bid);
                        player.HandleWins();
                        balance -= (odds * bid + ante * 2 + bid);
                        // Casino loses
                        losses++;
                        Console.WriteLine(" win, odds = " + odds);
                    }
                    else if (player.GetHand().GetRank() < dealerHand.GetRank())
                    {
                        // Player lose
                        player.HandleLoss();

                        // Casino wins
                        wins++;
                        Console.WriteLine(" loss");
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
                            Console.WriteLine(" loss");
                        }
                        else if (res == -1)
                        {
                            int odds = PayOutTable(player.GetHand());
                            // Player wins, pay out bid * odds, plus initial ante*2 and bid
                            player.Creditbalance(odds * bid + ante * 2 + bid);
                            player.HandleWins();
                            balance -= (odds * bid + ante * 2 + bid);
                            // Casino lose
                            losses++;
                            Console.WriteLine(" win, odds = " + odds);
                        }
                        else
                        {
                            // Return ante + bid
                            player.Creditbalance(ante + bid);
                            balance -= (ante + bid);

                            draws++;
                            Console.WriteLine(" draw");
                        }
                    }
            }
            else
            {
                // Player folds
                player.HandleFold();
                folds++;
                Console.WriteLine(" fold");
            }
        }

        private bool DoCall(Hand h)
        {
            bool toRet = false;
            if (h.GetRank() >= CardRank.Pair)
            {
                calls++;
                toRet = true;
            }
            else
            {
                if (h.GetCards()[4].GetRank() == 14 && h.GetCards()[3].GetRank() == 13)
                {
                   toRet = true;
                   calls++;
                }
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
                    if (h1.GetCards()[4].GetRank() > h2.GetCards()[4].GetRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.GetCards()[4].GetRank() < h2.GetCards()[4].GetRank())
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
                    for (int i = 4; i > 0; i--)
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

            //debugging purposes
            
            Console.Write(h1.GetRank() + "  ");

            if (toRet == 0)
            {
                Console.WriteLine(h1 + "= " + h2);
            }
            else if (toRet == 1)
            {
                Console.WriteLine(h1 + "> " + h2);
            }
            else
            {
                Console.WriteLine(h1 + "< " + h2);
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
                hand.GetQuad().AddRange(quads);
                hand.SetCards(quads);
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
