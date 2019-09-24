using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class CaribbeanStudPoker : Pokergame
    {
        private Hand dealerHand = null;

        public CaribbeanStudPoker()
        {

        }

        public void setHand(Hand h)
        {
            dealerHand = h;
        }
        
        private int payOutTable(Hand hand)
        {
            int toRet = 0;
            switch (hand.getRank())
            {
                case CardRank.RoyalFlush:
                    toRet = 100;
                    break;
                case CardRank.StraightFlush:
                    toRet = 50;
                    break;
                case CardRank.Quads:
                    toRet = 20;
                    break;
                case CardRank.FullHouse:
                    toRet = 7;
                    break;
                case CardRank.Flush:
                    toRet = 5;
                    break;
                case CardRank.Straight:
                    toRet = 4;
                    break;
                case CardRank.Set:
                    toRet = 3;
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

        public void play(Player player)
        {
            int ante = 0;
            int bid = 0;

            Console.Write(player.getHand() + "vs " + dealerHand + "- " + player.getHand().getRank() + " -");

            //Now start to play
            ante = player.getAnteBid();

            if (doCall(player.getHand()))
            {
                bid = player.getCallBid();

                if (isQualified(dealerHand))
                {
                    if (player.getHand().getRank() > dealerHand.getRank())
                    {
                        int odds = payOutTable(player.getHand());
                        // Player wins, pay out bid * odds, plus initial ante*2 and bid
                        player.handlePayOut(odds * bid + ante*2 + bid);
                        // Casino loses
                        losses++;
                        Console.WriteLine(" win, odds = " + odds);
                    }
                    else if (player.getHand().getRank() < dealerHand.getRank())
                    {
                        // Player lose
                        player.handleLoss();
                        claim(bid);
                        claim(ante);
                        // Casino wins
                        wins++;
                        Console.WriteLine(" loss");
                    }
                    else
                    {
                        //Hands are eqaul. Compare card by card 
                        int res = compareHands(dealerHand, player.getHand());
                        if(res == 1)
                        {
                            //Player loose
                            player.handleLoss();
                            claim(bid);
                            claim(ante);
                            //Casino wins
                            wins++;
                            Console.WriteLine(" loss");
                        }
                        else if (res == -1)
                        {
                            int odds = payOutTable(player.getHand());
                            // Player wins, pay out bid * odds, plus initial ante*2 and bid
                            player.handlePayOut(odds * bid + ante * 2 + bid);
                            //Casino lose
                            losses++;
                            Console.WriteLine(" win, odds = " + odds);
                        }
                        else
                        {
                            //Return ante + bid
                            player.handleDraw(ante + bid);
                            draws++;
                            Console.WriteLine(" draw");
                        }
                    }
                } 
                else
                {
                    //Pay back bid and 2*ante
                    player.handlePayOut(ante + ante + bid);
                    nq++;
                    Console.WriteLine(" NQ");
                }
            }
            else
            {
                //Player folds
                player.handleFold();
                claim(ante);
                folds++;
                Console.WriteLine(" fold");
            }
        }

        private bool doCall(Hand h)
        {
            bool toRet = false;
            if (h.getRank() >= CardRank.Pair)
            {
                calls++;
                toRet = true;
            }
            return toRet;
        }

        private bool isQualified(Hand h)
        {
            bool toRet = false;
            if (h.getRank() > CardRank.HighCard)
            {
                toRet = true;
            }
            else
            {
                bool kingOrAce = false;
                foreach (Card c in h.getHand())
                {
                    if (c.getRank() == 13 || c.getRank() == 14)
                    {
                        if (kingOrAce)
                        {
                            toRet = true;
                            break;
                        }
                        kingOrAce = true;
                    }
                }
            }

            return toRet;
        }

        private protected void claim(int amount)
        {
            balance = balance + amount;
        }


        private int compareHands(Hand h1, Hand h2)
        {
            int toRet = 0;
            switch (h1.getRank())
            {
                case CardRank.RoyalFlush:
                    toRet = 0;
                    break;
                case CardRank.StraightFlush:
                    if (h1.getHand()[4].getRank() > h2.getHand()[4].getRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.getHand()[4].getRank() < h2.getHand()[4].getRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.Quads:
                    if (h1.getQuad()[0].getRank() > h2.getQuad()[0].getRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.getQuad()[0].getRank() < h2.getQuad()[0].getRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.FullHouse:
                case CardRank.Set:
                    if (h1.getSet()[0].getRank() > h2.getSet()[0].getRank())
                    {
                        toRet = 1;
                    }
                    else
                    {
                        toRet = -1;
                    }
                    break;
                case CardRank.Flush:
                case CardRank.Straight:
                    if (h1.getHand()[0].getRank() > h2.getHand()[0].getRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.getHand()[0].getRank() < h2.getHand()[0].getRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.TwoPair:
                    toRet = 0;
                    if (h1.getPair2()[0].getRank() > h2.getPair2()[0].getRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.getPair2()[0].getRank() < h2.getPair2()[0].getRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        if (h1.getPair1()[0].getRank() > h2.getPair1()[0].getRank())
                        {
                            toRet = 1;
                        }
                        else if (h1.getPair1()[0].getRank() < h2.getPair1()[0].getRank())
                        {
                            toRet = -1;
                        }
                    }
                    break;
                case CardRank.Pair:
                    if (h1.getPair1()[0].getRank() > h2.getPair1()[0].getRank())
                    {
                        toRet = 1;
                    }
                    else if (h1.getPair1()[0].getRank() < h2.getPair1()[0].getRank())
                    {
                        toRet = -1;
                    }
                    else
                    {
                        toRet = 0;
                    }
                    break;
                case CardRank.HighCard:
                    for (int i = 4; i > 0; i--)
                    {
                        if (h1.getHand()[i].getRank() > h2.getHand()[i].getRank())
                        {
                            toRet = 1;
                            break;
                        }
                        else if (h1.getHand()[i].getRank() < h2.getHand()[i].getRank())
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
            /*
            Console.Write(h1.getRank() + "  ");

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
            */
            return toRet;
        }
    }
}
