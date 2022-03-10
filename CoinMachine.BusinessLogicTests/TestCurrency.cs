using CoinMachine.Core.Interfaces;

namespace CoinMachine.BusinessLogicTests
{
    internal class TestCurrency : ICurrency
    {
        public string Symbol { get; private set; }
        public decimal[] Denominations { get; private set; }

        public TestCurrency()
        {
            Symbol = "_"; // Any
            Denominations = new decimal[] { 0.03m, 0.02m, 0.01m };
        }
    }
}
