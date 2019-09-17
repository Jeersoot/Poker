using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Poker
{
    class Game
    {
        private Random rand = new Random();
        private readonly Stack<Card> deck = new Stack<Card>(Convert.ToInt32(DECK_CARD_COUNT));
        private readonly List<Player> players;
        private const string DECK_LOCATION = "some string";
        private const uint DECK_CARD_COUNT = 52;
        private const uint DECK_SHUFFLE_COUNT = 20;

        public Game(uint playerCount, uint startCardCount)
        {
            if (playerCount < 2)
            {
                throw new Exception(ExceptionUtil.FormatExceptionString("TOO_FEW_PLAYERS_EXCEPTION",
                    "Game was initalized with too few players. The minimum amount of players are 2"));
            }

            var maybeCards = DeserializeDeck();
            var cards = maybeCards.HasItem ? maybeCards.Item : throw new Exception("CARD_INIT_EXCEPTION"); //If cards don't exist, panic.
            Shuffle(cards); //Gives the cards a random order before adding them to the deck.
            cards.ForEach(x => deck.Push(x)); //Adds the cards to the deck.

            //Adds players to game
            players = new List<Player>(Convert.ToInt32(playerCount));
            for (int i = 0; i < playerCount; i++)
            {
                var startingCards = new List<Card>();
                for (int p = 0; p < startCardCount; p++)
                {
                    startingCards.Add(deck.Pop());
                }
                players.Add(new Player(startingCards));
            }
        }

        private void Deal(Player player, uint amount)
        {
            //Deals cards to a player. Amount is unsigned since one cannot give negative cards;

            if(amount > deck.Count)
            {
                throw new Exception("Out of cards"); //TODO: Add a reshuffle deck or something.
            }

            List<Card> cards = new List<Card>(Convert.ToInt32(amount));
            for(uint i = 0; i < amount; i++)
            {
                cards.Add(deck.Pop());
            }
            player.GiveCards(cards);
        }

        private Maybe<List<Card>> DeserializeDeck()
        {
            var cards = new Maybe<List<Card>>(JsonConvert.DeserializeObject<List<Card>>(DECK_LOCATION));
            if (!cards.HasItem)
            {
                throw new Exception(ExceptionUtil.FormatExceptionString("CARD_DESERIALIZATION_EXCEPTION", "The card json file failed to deserialize."));
            }
            else if (cards.Item.Count != DECK_CARD_COUNT)
            {
                throw new Exception(ExceptionUtil.FormatExceptionString("CARD_COUNT_EXCEPTION", "The card json file has too few or many cards."));
            }
            return cards;
        }

        private void Shuffle(List<Card> cards)
        {
            //Shuffles the cards in a random order.
            for(uint i = 0; i < DECK_SHUFFLE_COUNT; i++)
            {
                cards.Swap(Convert.ToUInt32(rand.Next(0, Convert.ToInt32(DECK_CARD_COUNT))),
                    Convert.ToUInt32(rand.Next(0, Convert.ToInt32(DECK_CARD_COUNT))));
            }
        }
    }
}
