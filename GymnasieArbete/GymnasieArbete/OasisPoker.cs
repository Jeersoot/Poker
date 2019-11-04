using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace GymnasieArbete
{
    class OasisPoker : CaribbeanStudPoker
    {
        public OasisPoker()
        {

        }

        public void Play(Player player, CardDeck deck)
        {
            ReplaceTrashCards(player, deck);
            base.Play(player);
        }

        private void ReplaceTrashCards(Player player, CardDeck deck)
        {
            Hand hand = player.GetHand();
            switch (hand.GetRank())
            {
                case CardRank.RoyalFlush:
                case CardRank.StraightFlush:
                case CardRank.Straight:
                case CardRank.Flush:
                case CardRank.Quads:
                case CardRank.FullHouse:
                case CardRank.Set:
                case CardRank.TwoPair:
                    // I'm happy. Do nothing and exit here.
                    return;
                case CardRank.Pair:
                case CardRank.HighCard:
                    ApplyCardSwitchPolicy(hand, player, deck);
                    break;
                default:
                    break;
            }
        }

        /**
         * The player should switch one card with any of the following:
         * 
         * Four to a straight flush and royal flush, even breaking up a pair
         * Four to a flush with no pair.
         * Four to an outside straight with no pair.
         * 
         * The player will sometimes switch with any of the following:
         * -- Not Implemented -- Four to a flush with a low pair (depends on the pair and dealer's up card).
         * Four to an inside straight with no pair (depends on the inside straight and dealer's up card).
         */
        private void ApplyCardSwitchPolicy(Hand h, Player player, CardDeck deck)
        {

            Card switchCardForFlush = null;
            Card switchCardForStraight = null;
            Card dealersUpCard = base.GetDealerHand().GetUpCard();

            bool isOutSideStraight = false;
            bool wantingDealersUp = false;

            /**
             * First, check if four to a flush
             */
            Hashtable flushMap = new Hashtable();
            foreach (Card c in h.GetHand())
            {
                if (flushMap.ContainsKey(c.GetSuit()))
                {
                    List<Card> l = (List<Card>)flushMap[c.GetSuit()];
                    l.Add(c);
                }
                else
                {
                    List<Card> l = new List<Card>();
                    l.Add(c);
                    flushMap.Add(c.GetSuit(), l);
                }
            }

            if (flushMap.Count == 2)
            {
                foreach (DictionaryEntry item in flushMap)
                {
                    List<Card> l = (List<Card>)item.Value;
                    if (l.Count == 1)
                    {
                        switchCardForFlush = l[0];
                    }
                }
            }

            /**
             * Check if four to a straight
             */
            Card card1 = h.GetHand()[0];
            Card card2 = h.GetHand()[1];
            Card card3 = h.GetHand()[2];
            Card card4 = h.GetHand()[3];
            Card card5 = h.GetHand()[4];
            // OXXXX
            if (card2.GetRank() + 1 == card3.GetRank() &&
                card3.GetRank() + 1 == card4.GetRank() &&
                card4.GetRank() + 1 == card5.GetRank())
            {
                // is outside straight
                if (card2.GetRank() >= 3 && card5.GetRank() <= 12)
                {
                    isOutSideStraight = true;
                } else
                {
                    if(dealersUpCard.GetRank() + 1 == card2.GetRank())
                    {
                        wantingDealersUp = true;
                    }
                }
                switchCardForStraight = card1;
            }

            // XOXXX
            else if (card1.GetRank() + 2 == card3.GetRank() &&
                card3.GetRank() + 1 == card4.GetRank() &&
                card4.GetRank() + 1 == card5.GetRank())
            {
                if (dealersUpCard.GetRank() + 1 == card3.GetRank())
                {
                    wantingDealersUp = true;
                }
                switchCardForStraight = card2;
            }

            // XXOXX
            else if (card1.GetRank() + 1 == card2.GetRank() &&
                card2.GetRank() + 2 == card4.GetRank() &&
                card4.GetRank() + 1 == card5.GetRank())
            {
                if (dealersUpCard.GetRank() + 1 == card4.GetRank())
                {
                    wantingDealersUp = true;
                }

                switchCardForStraight = card3;
            }

            // XXXOX
            else if (card1.GetRank() + 1 == card2.GetRank() &&
                card2.GetRank() + 1 == card3.GetRank() &&
                card3.GetRank() + 2 == card5.GetRank())
            {
                if (dealersUpCard.GetRank() + 1 == card5.GetRank())
                {
                    wantingDealersUp = true;
                }
                switchCardForStraight = card4;
            }
            // XXXXO
            else if (card1.GetRank() + 1 == card2.GetRank() &&
                card2.GetRank() + 1 == card3.GetRank() &&
                card3.GetRank() + 1 == card4.GetRank())
            {
                // is outside straight
                if (card1.GetRank() >= 3 && card4.GetRank() <= 12)
                {
                    isOutSideStraight = true;
                } else {
                    if (dealersUpCard.GetRank() - 1 == card4.GetRank())
                    {
                        wantingDealersUp = true;
                    }
                }

                switchCardForStraight = card5;
            }


            /**
             * Now do the card switch
             */
            if(switchCardForFlush != null && switchCardForStraight != null && 
                switchCardForFlush == switchCardForStraight )
            {
                // potential straight flush :-)
                DoSwitchCard(h, switchCardForFlush, player, deck);
            }
            else if (switchCardForFlush != null && h.GetRank() == CardRank.HighCard)
            {
                // potential flush
                DoSwitchCard(h, switchCardForFlush, player, deck);
            }
            else if (switchCardForStraight != null && h.GetRank() == CardRank.HighCard && isOutSideStraight)
            {
                // potential outside straight 
                DoSwitchCard(h, switchCardForStraight, player, deck);
            }
            else if (switchCardForStraight != null && h.GetRank() == CardRank.HighCard && !wantingDealersUp)
            {
                // potential inside straight 
                DoSwitchCard(h, switchCardForStraight, player, deck);
            }
        }

        private void DoSwitchCard(Hand hand, Card switchCard, Player player, CardDeck deck)
        {
            hand.GetHand().Remove(switchCard);
            hand.GetHand().Add(deck.GetCards(1)[0]);
            hand.SortAndRank();

            balance += 1;
            player.Debitbalance(1);

        }
    }
}
