namespace CoinMachine.Core.Interfaces
{
    public interface ICurrency
    {
        string Symbol { get; }
        decimal[] Denominations { get; }
    }
}
