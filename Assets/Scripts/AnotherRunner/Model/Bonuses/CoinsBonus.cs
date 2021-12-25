using AnotherRunner.Model.Players;

namespace AnotherRunner.Model.Bonuses
{
    public class CoinsBonus : IBonus
    {
        public float Duration { get; }
        
        private readonly int _coins;

        public CoinsBonus(int coins)
        {
            _coins = coins;
            Duration = 0;
        }

        public void Apply(IPlayer player)
        {
            var wallet = player.Wallet;
            wallet.Increase(_coins);
        }

        public void Complete() {}
    }
}