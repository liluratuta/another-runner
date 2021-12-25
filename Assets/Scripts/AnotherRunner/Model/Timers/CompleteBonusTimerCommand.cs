using AnotherRunner.Model.Bonuses;

namespace AnotherRunner.Model.Timers
{
    public class CompleteBonusTimerCommand : ITimerCommand
    {
        private readonly IBonus _bonus;

        public CompleteBonusTimerCommand(IBonus bonus)
        {
            _bonus = bonus;
        }

        public void Execute() => _bonus.Complete();
    }
}