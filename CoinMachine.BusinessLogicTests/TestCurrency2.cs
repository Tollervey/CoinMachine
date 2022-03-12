using CoinMachine.Core.Interfaces;

namespace CoinMachine.BusinessLogicTests
{
    internal class TestCurrency2 : ICurrency
    {
        public string Symbol { get; private set; }
        public decimal[] Denominations { get; private set; }

        public TestCurrency2()
        {
            Symbol = "_"; // Any
            Denominations = new decimal[] { 0.02m, 0.05m, 0.03m, 0.06m };
        }
    }
}