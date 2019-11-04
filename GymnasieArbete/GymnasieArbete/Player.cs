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

        public void SetHand(Hand h)
        {
            hand = h;
        }

        public Hand GetHand()
        {
            return hand;
        }

        public int GetBalance()
        {
            return balance;
        }

        public int GetWins()
        {
            return wins;
        }

        public int GetFolds()
        {
            return folds;
        }

        public int GetDraws()
        {
            return draws;
        }
        public int GetCalls()
        {
            return calls;
        }

        public int GetLosses()
        {
            return losses;
        }

        public void HandleFold()
        {
            folds++;
        }

        public void HandleWins()
        {
            wins++;
        }

        public void HandleDraw()
        {
            draws++;
        }

        public void HandleLoss()
        {
            losses++;
        }
        public void HandleCalls()
        {
            calls++;
        }
        public void Creditbalance(int b)
        {
            balance += b;
        }
        public void Debitbalance(int b)
        {
            balance -= b;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
