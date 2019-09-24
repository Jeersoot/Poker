using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class Player
    {
        private Hand hand = null;
        private int balance = 0;
        private String name = "NoName";
        private int wins = 0;
        private int losses = 0;
        private int calls = 0;
        private int folds = 0;
        private int draws = 0;

        public Player(String n)
        {
            name = n;
        }

        public void setHand(Hand h)
        {
            hand = h;
        }

        public Hand getHand()
        {
            return hand;
        }

        public int getBalance()
        {
            return balance;
        }

        public int getWins()
        {
            return wins;
        }

        public int getFolds()
        {
            return folds;
        }

        public int getDraws()
        {
            return draws;
        }
        public int getCalls()
        {
            return calls;
        }

        public int getLosses()
        {
            return losses;
        }

        public int getAnteBid()
        {
            balance = balance - 1;
            return 1;
        }

        public void payCardReplacement(int a)
        {
            balance = balance - a;
        }

        public int getCallBid()
        {
            calls++;
            balance = balance - 2;
            return 2;
        }

        public void handleFold()
        {
            folds++;
        }

        public void handlePayOut(int a)
        {
            wins++;
            balance = balance + a;  
        }

        public void handleDraw(int a)
        {
            draws++;
            balance = balance + a;
        }

        public void handleLoss()
        {
            losses++;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
