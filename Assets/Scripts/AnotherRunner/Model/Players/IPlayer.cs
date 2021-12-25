using AnotherRunner.Model.Bodies;

namespace AnotherRunner.Model.Players
{
    public interface IPlayer : IRunner, IJumper
    {
        float HP { get; }
        Wallet Wallet { get; }
        MultipliableValue MultipliableRunningSpeed { get; }
        MultipliableValue MultipliableJumpHeight { get; }
        void ApplyDamage(float damage);
    }
}