using System.Collections.Generic;
using AnotherRunner.Model.Bodies;
using AnotherRunner.Model.Games;
using AnotherRunner.Model.Obstacles;
using AnotherRunner.Model.Simulations;
using AnotherRunner.Model.Spawners;

namespace AnotherRunner.Model.Routers
{
    public class ObstacleEventsRouter
    {
        private readonly Game _game;
        private readonly ISpawner<IObstacle> _spawner;
        private readonly RunningSimulation _runningSimulation;
        private readonly CollisionObserver _collisionObserver;

        private readonly Dictionary<IBody, IObstacle> _spawnedObstacles = new Dictionary<IBody, IObstacle>();
        
        public ObstacleEventsRouter(Game game, ISpawner<IObstacle> spawner, RunningSimulation runningSimulation, CollisionObserver collisionObserver)
        {
            _game = game;
            _spawner = spawner;
            _runningSimulation = runningSimulation;
            _collisionObserver = collisionObserver;

            _spawner.Spawned += OnObstacleSpawned;
            _spawner.Reclaimed += OnObstacleReclaimed;
            _runningSimulation.BodyOutside += OnBodyOutside;
            _collisionObserver.CollidedWithPlayer += OnCollidedWithPlayer;
        }

        public void Dispose()
        {
            _spawner.Spawned -= OnObstacleSpawned;
            _spawner.Reclaimed -= OnObstacleReclaimed;
            _runningSimulation.BodyOutside -= OnBodyOutside;
            _collisionObserver.CollidedWithPlayer -= OnCollidedWithPlayer;
        }

        private void OnCollidedWithPlayer(IBody body)
        {
            if (!_spawnedObstacles.ContainsKey(body))
            {
                return;
            }
            
            _game.PerformPlayerDamage(_spawnedObstacles[body]);
        }

        private void OnBodyOutside(IBody body)
        {
            if (!_spawnedObstacles.ContainsKey(body))
            {
                return;
            }
            
            _game.ReclaimObstacle(_spawnedObstacles[body]);
        }

        private void OnObstacleSpawned(IObstacle obstacle) => _spawnedObstacles.Add(obstacle, obstacle);
        private void OnObstacleReclaimed(IObstacle obstacle) => _spawnedObstacles.Remove(obstacle);
    }
}