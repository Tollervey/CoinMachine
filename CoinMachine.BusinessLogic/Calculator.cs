using CoinMachine.Core;
using CoinMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoinMachine.BusinessLogicTests")]

namespace CoinMachine.BusinessLogic
{
    public class Calculator<T> where T : class, ICurrency, new()
    {
        private readonly T _currency;
        public Calculator()
        {
            _currency = new T();
        }

        /// <summary>
        /// Takes an input string for the coin machine and return the number of solutions with an odd coin count
        /// </summary>
        /// <param name="input"></param>
        /// <example>£{Pound}-{ Pence}, £10-</example>
        /// <returns></returns>
        public int GetChangeOddCoinCount(string input)
        {
            if (Decimal.TryParse(input.Replace(_currency.Symbol, "").Replace("-", "."), out var amount))
            {
                return GetSolutionsOddCount(amount);
            }

            return 0;
        }

        internal int GetSolutionsOddCount(decimal amountDeposited)
        {
            var result = GetSolutionsCount(amountDeposited);
            return result.CoinsInEachSolutionCount.Count(_ => _ % 2 != 0);

        }

        internal ICoinMachineResult GetSolutionsCount(decimal amountDeposited)
        {
            List<int> newDenominations = new List<int>(_currency.Denominations.Length);

            // multiple values by 100 so that calculations can be done using integers
            newDenominations.AddRange(_currency.Denominations.Select(_ => Decimal.ToInt32(_ * 100)).ToList());
            var newAmountDeposited = Decimal.ToInt32(amountDeposited * 100);
            var solutionCount = Count(newDenominations.ToArray(), _currency.Denominations.Length, newAmountDeposited, 0);

            var result = new CoinMachineResult() { SolutionsCount = solutionCount, CoinsInEachSolutionCount = _coinsInEachSolutionCounter };
            return result;
        }

        //private int _recursiveCounter = 1;
        private readonly IList<int> _coinsInEachSolutionCounter = new List<int>();

        internal int Count(int[] denomination, int denominationPositionInArray, int amountDeposited/*, int depth = 1, int left = 0, int right = 0*/, int incrementCoinsInSolutionCounter)
        {
            // If amountDeposited is 0 then there is 1 solution found
            if (amountDeposited == 0)
            {
               // _recursiveCounter++;
                _coinsInEachSolutionCounter.Add(incrementCoinsInSolutionCounter);
                return 1;
            }

            // If amountDeposited is less then 0 or there are no coins, then no solution exists
            if (amountDeposited < 0 || denominationPositionInArray <= 0)
            {
                //_recursiveCounter++;
                return 0;
            }

            var firstCountMoveLeftDownOne = Count(denomination, denominationPositionInArray - 1, amountDeposited/*, depth+1, left+1, right*/, incrementCoinsInSolutionCounter);

            var secondCountMoveRightDownOne = Count(denomination, denominationPositionInArray, amountDeposited - denomination[denominationPositionInArray - 1]/*, depth + 1, left, right+1*/, incrementCoinsInSolutionCounter += 1);

            var result = firstCountMoveLeftDownOne + secondCountMoveRightDownOne;

            // _recursiveCounter++;
            
            return result;
        }
    }
}
