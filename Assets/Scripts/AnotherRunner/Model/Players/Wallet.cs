using System;

namespace AnotherRunner.Model.Players
{
    public class Wallet
    {
        public event Action<int> CountChanged;
        
        public int Count { get; private set; }

        public Wallet(int startCapital)
        {
            Count = startCapital;
        }

        public void Increase(int coins)
        {
            Count += coins;
            CountChanged?.Invoke(Count);
        }
    }
}