using CoinMachine.Core.Interfaces;

namespace CoinMachine.Core.Currencies
{
    public class PoundSterling : ICurrency
    {
        public string Symbol { get; private set; }
        public decimal[] Denominations { get; private set; }

        public PoundSterling()
        {
            Symbol = "£";
            // Spec appears to be wrong so have commented out 5,2 and 1p
            Denominations = new decimal[] { 2m, 1m, 0.5m, 0.2m, 0.1m/*, 0.05m, 0.02m, 0.01m*/ };
        }
    }
}
