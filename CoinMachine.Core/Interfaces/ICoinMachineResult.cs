using System.Collections.Generic;

namespace CoinMachine.Core.Interfaces
{
    public interface ICoinMachineResult
    {
        int SolutionsCount { get; }
        IList<int> CoinsInEachSolutionCount { get; }
    }
}
