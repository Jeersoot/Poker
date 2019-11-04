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
        public int GetBalance()
        {
            return balance;
        }
        public int GetWins()
        {
            return wins;
        }
        public int GetLosses()
        {
            return losses;
        }
        public int GetDraws()
        {
            return draws;
        }
        public int GetFolds()
        {
            return folds;
        }
        public int GetCalls()
        {
            return calls;
        }
        public int GetNq()
        {
            return nq;
        }
    }
}
