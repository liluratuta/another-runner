using AnotherRunner.Model.Bonuses.BonusBoxes;
using AnotherRunner.Model.Obstacles;
using AnotherRunner.Model.Players;
using AnotherRunner.Model.Spawners;
using AnotherRunner.Model.Timers;

namespace AnotherRunner.Model.Games
{
    public class Game
    {
        private const float ObstacleDamage = 1f;
        private const float ObstaclesSpawnInterval = 3f;
        private const float BonusBoxesSpawnInterval = 6f;
        
        private readonly ISpawner<IObstacle> _obstacleSpawner;
        private readonly ISpawner<IBonusBox> _bonusBoxSpawner;
        private readonly IPlayer _player;
        private readonly ITimer _timer;

        public Game(ISpawner<IObstacle> obstacleSpawner, ISpawner<IBonusBox> bonusBoxSpawner, IPlayer player, ITimer timer)
        {
            _obstacleSpawner = obstacleSpawner;
            _bonusBoxSpawner = bonusBoxSpawner;
            _player = player;
            _timer = timer;
        }

        public void Start()
        {
            _timer.Repeat(ObstaclesSpawnInterval, new SpawnObstacleTimerCommand(this));
            _timer.Repeat(BonusBoxesSpawnInterval, new SpawnBonusBoxTimerCommand(this));
        }

        public void OpenBonusBox(IBonusBox bonusBox)
        {
            var bonus = bonusBox.Open();
            
            bonus.Apply(_player);
            _timer.Add(bonus.Duration, new CompleteBonusTimerCommand(bonus));
            
            ReclaimBonusBox(bonusBox);
        }

        public void PerformPlayerDamage(IObstacle obstacle)
        {
            _player.ApplyDamage(ObstacleDamage);
            
            ReclaimObstacle(obstacle);
        }

        public void SpawnObstacle() => _obstacleSpawner.Spawn();
        public void SpawnBonusBox() => _bonusBoxSpawner.Spawn();
        public void ReclaimObstacle(IObstacle obstacle) => _obstacleSpawner.Reclaim(obstacle);
        public void ReclaimBonusBox(IBonusBox bonusBox) => _bonusBoxSpawner.Reclaim(bonusBox);
    }
}