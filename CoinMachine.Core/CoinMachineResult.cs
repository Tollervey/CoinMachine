using CoinMachine.Core.Interfaces;
using System.Collections.Generic;

namespace CoinMachine.Core
{
    public class CoinMachineResult : ICoinMachineResult
    {
        public int SolutionsCount { get; set; }
        public IList<int> CoinsInEachSolutionCount { get; set; }
    }
}
