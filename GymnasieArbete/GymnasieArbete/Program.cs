using System;
using System.Collections.Generic;


namespace GymnasieArbete
{
    class Program
    {
        private static int FIVE_CARD_POKER = 5;
        static List<Card> cards = new List<Card>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            //bool replaceTrash = false; //Replaces Cards in hand

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

            CardDeck deck = new CardDeck();

            CaribbeanStudPoker caribbean = new CaribbeanStudPoker();
            OasisPoker oasis = new OasisPoker();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("");
                Player player1 = new Player("Caribbean " + i);
                Player player2 = new Player("Oasis " + i);

                for (int loop = 0; loop < 10; loop++)
                {
                    deck.Shuffle();
                    Hand playerHand = new Hand(deck.getCards(FIVE_CARD_POKER));
                    Hand dealerHand = new Hand(deck.getCards(FIVE_CARD_POKER));

                    caribbean.setHand(dealerHand);
                    oasis.setHand(dealerHand);

                    player1.setHand(playerHand);
                    player2.setHand(playerHand);

                    caribbean.play(player1);
                    oasis.play(player2, deck);
                }

                Console.WriteLine(player1 + ":  wins = " + player1.getWins() + ", losses = " + player1.getLosses() + ", draws = " + player1.getDraws());
                Console.WriteLine(player1 + ":  calls = " + player1.getCalls() + ", folds = " + player1.getFolds() + ", total balance = " + player1.getBalance());

                Console.WriteLine(player2 + ":  wins = " + player2.getWins() + ", losses = " + player2.getLosses() + ", draws = " + player2.getDraws());
                Console.WriteLine(player2 + ":  calls = " + player2.getCalls() + ", folds = " + player2.getFolds() + ", total balance = " + player2.getBalance());

            }
            // Caribbean Stud Poker:
            Console.WriteLine("");
            Console.WriteLine("Caribbean Stud Poker:");
            Console.WriteLine("Casino: wins = " + caribbean.getWins() + ", losses = " + caribbean.getLosses() + ", draws = " + caribbean.getDraws());
            Console.WriteLine("total balance = " + caribbean.getBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + caribbean.getFolds() + ", Number of Calls = " + caribbean.getCalls());
            Console.WriteLine("");
            Console.WriteLine("Number of Non Qualified games = " + caribbean.getNq());
            Console.WriteLine("---------------------------");

            // Oasis Poker:
            Console.WriteLine("Oasis Poker:");
            Console.WriteLine("");
            Console.WriteLine("Casino:" + " wins = " + oasis.getWins() + ", losses = " + oasis.getLosses() + ", draws = " + oasis.getDraws());
            Console.WriteLine("total balance = " + oasis.getBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + oasis.getFolds() + ", Number of Calls = " + oasis.getCalls());
            Console.WriteLine("");
            Console.WriteLine("Number of Non Qualified games = " + oasis.getNq());
        }
    }
}
