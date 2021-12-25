using AnotherRunner.Model.Games;

namespace AnotherRunner.Model.Timers
{
    public class SpawnObstacleTimerCommand : ITimerCommand
    {
        private readonly Game _game;

        public SpawnObstacleTimerCommand(Game game)
        {
            _game = game;
        }

        public void Execute() => _game.SpawnObstacle();
    }
}