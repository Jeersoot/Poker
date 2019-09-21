using System;
using System.Collections.Generic;


namespace GymnasieArbete
{
    class Program
    {

        static List<Card> cards = new List<Card>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PokerGame game = new PokerGame();
            bool replaceTrash = false; //Replaces Cards in hand
            // Test cases
            List<Card> twoPair = new List<Card>() { new Card(2, Suit.Club), new Card(2, Suit.Heart), new Card(4, Suit.Club), new Card(4, Suit.Heart), new Card(5, Suit.Heart) };
            List<Card> set = new List<Card>() { new Card(2, Suit.Club), new Card(2, Suit.Heart), new Card(2, Suit.Diamond), new Card(4, Suit.Heart), new Card(5, Suit.Heart) };
            List<Card> quad = new List<Card>() { new Card(2, Suit.Club), new Card(2, Suit.Heart), new Card(2, Suit.Diamond), new Card(2, Suit.Spade), new Card(5, Suit.Heart) };
            List<Card> flush = new List<Card>() { new Card(2, Suit.Club), new Card(3, Suit.Club), new Card(4, Suit.Club), new Card(7, Suit.Club), new Card(9, Suit.Club) };
            List<Card> straight = new List<Card>() { new Card(2, Suit.Diamond), new Card(3, Suit.Club), new Card(4, Suit.Diamond), new Card(5, Suit.Club), new Card(6, Suit.Club) };
            List<Card> straightFlush = new List<Card>() { new Card(3, Suit.Club), new Card(4, Suit.Club), new Card(5, Suit.Club), new Card(6, Suit.Club), new Card(7, Suit.Club) };
            List<Card> royalFlush = new List<Card>() { new Card(10, Suit.Club), new Card(11, Suit.Club), new Card(12, Suit.Club), new Card(13, Suit.Club), new Card(14, Suit.Club) };

            //new Hand(twoPair);
            //new Hand(set);
            //new Hand(quad);
            //new Hand(flush);
            //new Hand(straight);
            //new Hand(straightFlush);
            //new Hand(royalFlush);

           for (int loop = 0; loop < 10000; loop++)
           //CardRank rank = CardRank.HighCard;
            //while(rank != CardRank.RoyalFlush) //Goes until Royal Flush
            {
                game.Shuffle();

                while (game.hasMoreCards())
                {
                    Hand hand = new Hand(game.getDeal());
                    Console.WriteLine(hand + "  " + hand.getRank() + "  ");
                    if (replaceTrash)
                    {
                        game.replaceTrashCardsForHand(hand);
                        Console.WriteLine(hand + "  " + hand.getRank());
                    }

                    //rank = hand.getRank();
                    //if(rank == CardRank.RoyalFlush)
                    //{
                    //  break;    //Goes until Royal Flush
                    //}
                }

            }
        }
    }
}
