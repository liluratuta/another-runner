using AnotherRunner.Model.Players;

namespace AnotherRunner.Model.Bonuses
{
    public interface IBonus
    {
        float Duration { get; }
        void Apply(IPlayer player);
        void Complete();
    }
}