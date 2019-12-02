using System;
using System.Collections.Generic;


namespace GymnasieArbete
{
    class Program
    {
        private static int FIVE_CARD_POKER = 5;
        private static int FOUR_CARD_DEALER_HAND = 6;

        static List<Card> cards = new List<Card>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            //Replaces Cards in hand

            // Test cases
            List<Card> twoPair = new List<Card>() { new Card(2, Suit.Club), new Card(2, Suit.Heart), new Card(4, Suit.Club), new Card(4, Suit.Heart), new Card(5, Suit.Heart) };
            List<Card> set = new List<Card>() { new Card(2, Suit.Club), new Card(2, Suit.Heart), new Card(2, Suit.Diamond), new Card(4, Suit.Heart), new Card(5, Suit.Heart) };
            List<Card> quad = new List<Card>() { new Card(2, Suit.Club), new Card(2, Suit.Heart), new Card(2, Suit.Diamond), new Card(2, Suit.Spade), new Card(5, Suit.Heart) };
            List<Card> flush = new List<Card>() { new Card(2, Suit.Club), new Card(3, Suit.Club), new Card(4, Suit.Club), new Card(7, Suit.Club), new Card(9, Suit.Club) };
            List<Card> straight = new List<Card>() { new Card(2, Suit.Diamond), new Card(3, Suit.Club), new Card(4, Suit.Diamond), new Card(5, Suit.Club), new Card(6, Suit.Club) };
            List<Card> straightFlush = new List<Card>() { new Card(3, Suit.Club), new Card(4, Suit.Club), new Card(5, Suit.Club), new Card(6, Suit.Club), new Card(7, Suit.Club) };
            List<Card> royalFlush = new List<Card>() { new Card(10, Suit.Club), new Card(11, Suit.Club), new Card(12, Suit.Club), new Card(13, Suit.Club), new Card(14, Suit.Club) };
            List<Card> testAce = new List<Card>() { new Card(14, Suit.Club), new Card(2, Suit.Club), new Card(3, Suit.Club), new Card(4, Suit.Club), new Card(5, Suit.Club) };

            //new Hand(testAce);
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
            FourCardPoker fourcard = new FourCardPoker();

   
            for (int i = 0; i < 1000; i++)
            {    
                Console.WriteLine("");
                Player player1 = new Player("Caribbean " + i);
                Player player2 = new Player("Oasis " + i);
                Player player3 = new Player("Four Card " + i);

                for (int loop = 0; loop < 10; loop++)
                {
                    deck.Shuffle();
                    ////
                    // Test Handranking FourCardsPoker
                 /*
                    Hand dealerHand = new Hand(deck.GetCards(5));
                    dealerHand.Sort();
                    Console.Write(dealerHand.ToString());
                    fourcard.SetHand(dealerHand);
                    dealerHand = FourCardPoker.SortAndRankHandForFourCard(dealerHand);
                    Console.WriteLine("-> " + dealerHand.ToString() + ": " + dealerHand.GetRank().ToString());
                    ////
*/
                    Hand playerHand = new Hand(deck.GetCards(FIVE_CARD_POKER));
                    playerHand = Pokergame.SortAndRankHand(playerHand);

                    Hand dealerHand = new Hand(deck.GetCards(FIVE_CARD_POKER));
                    dealerHand = Pokergame.SortAndRankHand(dealerHand);

                    caribbean.SetHand(dealerHand);
                    oasis.SetHand(dealerHand);
                    fourcard.SetHand(dealerHand);


                    player1.SetHand(playerHand);

                    player2.SetHand(playerHand);

                    player3.SetHand(playerHand);

                    caribbean.Play(player1);
                    CardDeck deckCopy = deck.Clone();
                    oasis.Play(player2, deckCopy);
                    // fourcard.Play(player3, deckCopy);
        }

                Console.WriteLine(player1 + ":  wins = " + player1.GetWins() + ", losses = " + player1.GetLosses() + ", draws = " + player1.GetDraws());
                Console.WriteLine(player1 + ":  calls = " + player1.GetCalls() + ", folds = " + player1.GetFolds() + ", total balance = " + player1.GetBalance());

                Console.WriteLine(player2 + ":  wins = " + player2.GetWins() + ", losses = " + player2.GetLosses() + ", draws = " + player2.GetDraws());
                Console.WriteLine(player2 + ":  calls = " + player2.GetCalls() + ", folds = " + player2.GetFolds() + ", total balance = " + player2.GetBalance());

                Console.WriteLine(player3 + ": wins = " + player3.GetWins() + ", losses = " + player3.GetLosses() + ", draws = " + player3.GetDraws());
                Console.WriteLine(player3 + ": calls = " + player3.GetCalls() + ", folds = " + player3.GetFolds() + ", total balance = " + player3.GetBalance());

            }
            // Caribbean Stud Poker:
 
            Console.WriteLine("");
            Console.WriteLine("Caribbean Stud Poker:");
            Console.WriteLine("Casino: wins = " + caribbean.GetWins() + ", losses = " + caribbean.GetLosses() + ", draws = " + caribbean.GetDraws());
            Console.WriteLine("total balance = " + caribbean.GetBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + caribbean.GetFolds() + ", Number of Calls = " + caribbean.GetCalls());
            Console.WriteLine("");
            Console.WriteLine("Number of Non Qualified games = " + caribbean.GetNq());
            Console.WriteLine("---------------------------");

            // Oasis Poker:
            Console.WriteLine("Oasis Poker:");
            Console.WriteLine("");
            Console.WriteLine("Casino:" + " wins = " + oasis.GetWins() + ", losses = " + oasis.GetLosses() + ", draws = " + oasis.GetDraws());
            Console.WriteLine("total balance = " + oasis.GetBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + oasis.GetFolds() + ", Number of Calls = " + oasis.GetCalls());
            Console.WriteLine("");
            Console.WriteLine("Number of Non Qualified games = " + oasis.GetNq());
            Console.WriteLine("---------------------------");

            // Four Card Poker:
            Console.WriteLine("Four Card Poker:");
            Console.WriteLine("");
            Console.WriteLine("Casino:" + " wins = " + fourcard.GetWins() + ", losses = " + fourcard.GetLosses() + ", draws = " + fourcard.GetDraws());
            Console.WriteLine("total balance = " + fourcard.GetBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + fourcard.GetFolds() + ", Number of Calls = " + fourcard.GetFolds() );
            Console.WriteLine("");
            Console.WriteLine("Number of Non Qualified games = " + fourcard.GetNq());

        }
    }
}
