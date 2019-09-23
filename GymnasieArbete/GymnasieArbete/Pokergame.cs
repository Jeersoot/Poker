using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieArbete
{
    class Pokergame
    {
        private protected int balance = 0;
        private protected int wins = 0;
        private protected int losses = 0;
        private protected int calls = 0;
        private protected int folds = 0;
        private protected int draws = 0;
        private protected int nq = 0;
        public int getBalance()
        {
            return balance;
        }
        public int getWins()
        {
            return wins;
        }
        public int getLosses()
        {
            return losses;
        }
        public int getDraws()
        {
            return draws;
        }
        public int getFolds()
        {
            return folds;
        }
        public int getCalls()
        {
            return calls;
        }
        public int getNq()
        {
            return nq;
        }
    }
}
