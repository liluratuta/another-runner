using AnotherRunner.Model.Players;

namespace AnotherRunner.Model.Bonuses
{
    public class MultiplierJumpHeightBonus : IBonus
    {
        public float Duration { get; } = 10f;
        
        private readonly float _multiplier;
        private MultipliableValue _jumpHeight; 

        public MultiplierJumpHeightBonus(float multiplier)
        {
            _multiplier = multiplier;
        }

        public void Apply(IPlayer player)
        {
            _jumpHeight = player.MultipliableJumpHeight;
            _jumpHeight.AddMultiplier(_multiplier);
        }

        public void Complete()
        {
            _jumpHeight.RemoveMultiplier(_multiplier);
        }
    }
}