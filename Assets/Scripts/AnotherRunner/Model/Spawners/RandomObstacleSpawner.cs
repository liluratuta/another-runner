using System;
using AnotherRunner.Model.Levels;
using AnotherRunner.Model.Obstacles;
using AnotherRunner.Model.Simulations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnotherRunner.Model.Spawners
{
    public class RandomObstacleSpawner : ISpawner<IObstacle>
    {
        public event Action<IObstacle> Spawned;
        public event Action<IObstacle> Reclaimed;

        private const float MinSide = 0.5f;
        private const float MaxSide = 2f;
        
        private readonly RunningSimulation _runningSimulation;
        private readonly CollisionObserver _collisionObserver;
        private readonly LevelInfo _levelInfo;

        public RandomObstacleSpawner(RunningSimulation runningSimulation, CollisionObserver collisionObserver, LevelInfo levelInfo)
        {
            _runningSimulation = runningSimulation;
            _collisionObserver = collisionObserver;
            _levelInfo = levelInfo;
        }

        public void Spawn()
        {
            var obstacle = CreateObstacle();

            _runningSimulation.AddBody(obstacle);
            _collisionObserver.BindWithPlayer(obstacle);

            Spawned?.Invoke(obstacle);
        }

        public void Reclaim(IObstacle reclaimable)
        {
            _runningSimulation.RemoveBody(reclaimable);
            _collisionObserver.UntieFromPlayer(reclaimable);
            
            Reclaimed?.Invoke(reclaimable);
        }

        private IObstacle CreateObstacle()
        {
            var randomSide = Random.Range(MinSide, MaxSide);
            var size = new Vector2(randomSide, randomSide);
            
            var position = _levelInfo.spawnPoint;
            position.y = _levelInfo.groundLevel + size.y / 2f;
            
            var obstacle = new Obstacle(position, size);
            return obstacle;
        }
    }
}