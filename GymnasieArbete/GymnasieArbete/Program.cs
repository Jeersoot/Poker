using System;
using System.Collections.Generic;

namespace GymnasieArbete
{
    class Program
    {
        private static int FIVE_CARD_POKER = 5;
        private static int FOUR_CARD_DEALER_HAND = 6;
        private static bool ACES_UP = true;
        private static int NUMBER_OF_PLAYERS = 1;

        private static List<Card> cards = new List<Card>();
        private static List<Player> caribbeanPlayers = new List<Player>();
        private static List<Player> oasisPlayers = new List<Player>();
        private static List<Player> fourCardPlayers = new List<Player>();
        private static List<Player> acesUpPlayers = new List<Player>();


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CardDeck deck = new CardDeck();

            CaribbeanStudPoker caribbean = new CaribbeanStudPoker();
            OasisPoker oasis = new OasisPoker();
            FourCardPoker fourcard = new FourCardPoker();
            FourCardPoker fourcardAcesUp = new FourCardPoker(ACES_UP);
            createPlayers();

            for (int i = 0; i < 1000; i++)
            {
                //Console.WriteLine("");


                for (int loop = 0; loop < 1; loop++)
                {
                    playCaribbeanAndOasis(caribbean, oasis, deck);

                    playFourcardAndAcesUp(fourcard, fourcardAcesUp, deck);
                }
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
            Console.WriteLine("Total Volume: " + caribbean.GetVolume());
            Console.WriteLine("");
            Console.WriteLine("House's edge: " + caribbean.GetHousesEdge()); 
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
            Console.WriteLine("");
            Console.WriteLine("Total Volume: " + oasis.GetVolume());
            Console.WriteLine("House's edge: " + oasis.GetHousesEdge());

            Console.WriteLine("---------------------------");

            // Four Card Poker: Aces Up
            Console.WriteLine("Four Card Poker; Aces Up:");
            Console.WriteLine("");
            Console.WriteLine("Casino:" + " wins = " + fourcardAcesUp.GetWins() + ", losses = " + fourcardAcesUp.GetLosses() + ", draws = " + fourcardAcesUp.GetDraws());
            Console.WriteLine("total balance = " + fourcardAcesUp.GetBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + fourcardAcesUp.GetFolds() + ", Number of Calls = " + fourcardAcesUp.GetCalls());

            Console.WriteLine("");
            Console.WriteLine("Total Volume: " + fourcardAcesUp.GetVolume());
            Console.WriteLine("House's edge: " + fourcardAcesUp.GetHousesEdge());

            Console.WriteLine("---------------------------");
            // Four Card Poker:
            Console.WriteLine("Four Card Poker:");
            Console.WriteLine("");
            Console.WriteLine("Casino:" + " wins = " + fourcard.GetWins() + ", losses = " + fourcard.GetLosses() + ", draws = " + fourcard.GetDraws());
            Console.WriteLine("total balance = " + fourcard.GetBalance());
            Console.WriteLine("");
            Console.WriteLine("Number of Folds = " + fourcard.GetFolds() + ", Number of Calls = " + fourcard.GetCalls());

            Console.WriteLine("");
            Console.WriteLine("Total Volume: " + fourcard.GetVolume());
            Console.WriteLine("House's edge: " + fourcard.GetHousesEdge());
        }

        private static void playCaribbeanAndOasis(CaribbeanStudPoker caribbean, OasisPoker oasis, CardDeck deck)
        {
            // new give
            deck.Shuffle();

            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
            {
                Hand playerHand = new Hand(deck.GetCards(FIVE_CARD_POKER));
                playerHand = Pokergame.SortAndRankHand(playerHand);

                caribbeanPlayers[i].SetHand(playerHand);
                oasisPlayers[i].SetHand(playerHand);
            }

            Hand dealerHand = new Hand(deck.GetCards(FIVE_CARD_POKER));
            dealerHand = Pokergame.SortAndRankHand(dealerHand);

            caribbean.SetHand(dealerHand);
            oasis.SetHand(dealerHand);

            caribbean.SkipQualification(false);
            oasis.SkipQualification(false);

            CardDeck oasisDeck = deck.Clone();
            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
            {
                caribbean.Play(caribbeanPlayers[i]);
                oasis.Play(oasisPlayers[i], oasisDeck);
            }
        }

        private static void playFourcardAndAcesUp(FourCardPoker fourCard, FourCardPoker acesUp, CardDeck deck)
        {
            // new give
            deck.Shuffle();

            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
            {
                Hand playerHand = new Hand(deck.GetCards(FIVE_CARD_POKER));
                playerHand = FourCardPoker.SortAndRankHandForFourCard(playerHand);
                fourCardPlayers[i].SetHand(playerHand);
                acesUpPlayers[i].SetHand(playerHand);
            }

            Hand dealerHand = new Hand(deck.GetCards(FOUR_CARD_DEALER_HAND));
            dealerHand = FourCardPoker.SortAndRankHandForFourCard(dealerHand);

            fourCard.SetHand(dealerHand);
            acesUp.SetHand(dealerHand);

            CardDeck acesUpDeck = deck.Clone();
            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
            {
                fourCard.Play(fourCardPlayers[i], deck);
                acesUp.Play(acesUpPlayers[i], acesUpDeck);
            }
        }

        private static void createPlayers()
        {
            for (int i = 0; i < NUMBER_OF_PLAYERS; i++)
            {
                caribbeanPlayers.Add((new Player("Caribbean: " + (i + 1))));
                oasisPlayers.Add(( new Player("Oasis: " + (i + 1))));
                fourCardPlayers.Add((new Player("Fourcard: " + (i + 1))));
                acesUpPlayers.Add((new Player("AcesUp: " + (i + 1))));
            }
        }
    }
}
