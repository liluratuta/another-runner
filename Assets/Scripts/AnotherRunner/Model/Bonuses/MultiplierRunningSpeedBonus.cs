using AnotherRunner.Model.Players;

namespace AnotherRunner.Model.Bonuses
{
    public class MultiplierRunningSpeedBonus : IBonus
    {
        public float Duration { get; } = 10f;
        
        private readonly float _multiplier;
        private MultipliableValue _runningSpeed;

        public MultiplierRunningSpeedBonus(float multiplier)
        {
            _multiplier = multiplier;
        }

        public void Apply(IPlayer player)
        {
            _runningSpeed = player.MultipliableRunningSpeed;
            _runningSpeed.AddMultiplier(_multiplier);
        }

        public void Complete()
        {
            _runningSpeed.RemoveMultiplier(_multiplier);
        }
    }
}