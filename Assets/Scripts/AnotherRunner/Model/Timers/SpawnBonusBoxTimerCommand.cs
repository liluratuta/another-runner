using AnotherRunner.Model.Games;

namespace AnotherRunner.Model.Timers
{
    public class SpawnBonusBoxTimerCommand : ITimerCommand
    {
        private readonly Game _game;

        public SpawnBonusBoxTimerCommand(Game game)
        {
            _game = game;
        }

        public void Execute() => _game.SpawnBonusBox();
    }
}