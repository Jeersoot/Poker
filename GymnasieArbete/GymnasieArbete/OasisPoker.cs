using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class OasisPoker : CaribbeanStudPoker
    {
        public OasisPoker()
        {

        }

        public void play(Player player, CardDeck deck)
        {
            replaceTrashCards(player, deck);
            base.play(player);
        }

       private void replaceTrashCards(Player player, CardDeck deck)
       {
           Hand hand = player.getHand();
           List<Card> newHand = new List<Card>();
           switch (hand.getRank())
           {
               case CardRank.RoyalFlush:
               case CardRank.StraightFlush:
               case CardRank.Straight:
               case CardRank.Flush:
               case CardRank.Quads:
               case CardRank.FullHouse:
               case CardRank.Set:
               case CardRank.TwoPair:
               case CardRank.Pair:
                    // I'm happy. Do nothing and exit here.
                    return;
                    /*
               case CardRank.TwoPair:
                   // Replace 1
                   newHand.AddRange(hand.getPair1());
                   newHand.AddRange(hand.getPair2());
                   newHand.AddRange(deck.getCards(1));
                    claim(1);
                    player.payCardReplacement(1);
                   break;
               case CardRank.Set:
                   // Replace 2
                   newHand.AddRange(hand.getSet());
                   newHand.AddRange(deck.getCards(2));
                    claim(2);
                    player.payCardReplacement(2);
                    break;
  
                case CardRank.Pair:
                   // Replace 3
                   newHand.AddRange(hand.getPair1());
                   newHand.AddRange(deck.getCards(3));
                    claim(3);
                    player.payCardReplacement(3);
                    break;
                    */
               case CardRank.HighCard:
                   // Replace 5
                   newHand.AddRange(deck.getCards(5));
                    claim(1);
                    player.payCardReplacement(1);
                    break;
               default:
                   break;
           }
           hand.setHand(newHand);
       }
    }
}
