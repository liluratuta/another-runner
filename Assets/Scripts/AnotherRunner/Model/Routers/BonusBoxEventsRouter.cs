using System;
using System.Collections.Generic;
using AnotherRunner.Model.Bodies;
using AnotherRunner.Model.Bonuses.BonusBoxes;
using AnotherRunner.Model.Games;
using AnotherRunner.Model.Simulations;
using AnotherRunner.Model.Spawners;

namespace AnotherRunner.Model.Routers
{
    public class BonusBoxEventsRouter : IDisposable
    {
        private readonly Game _game;
        private readonly ISpawner<IBonusBox> _spawner;
        private readonly RunningSimulation _runningSimulation;
        private readonly CollisionObserver _collisionObserver;

        private readonly Dictionary<IBody, IBonusBox> _spawnedBonusBoxes = new Dictionary<IBody, IBonusBox>();

        public BonusBoxEventsRouter(Game game, ISpawner<IBonusBox> spawner, RunningSimulation runningSimulation, CollisionObserver collisionObserver)
        {
            _game = game;
            _spawner = spawner;
            _runningSimulation = runningSimulation;
            _collisionObserver = collisionObserver;

            _spawner.Spawned += OnBonusBoxSpawned;
            _spawner.Reclaimed += OnBonusBoxReclaimed;
            _runningSimulation.BodyOutside += OnBodyOutside;
            _collisionObserver.CollidedWithPlayer += OnCollidedWithPlayer;
        }

        public void Dispose()
        {
            _spawner.Spawned -= OnBonusBoxSpawned;
            _spawner.Reclaimed -= OnBonusBoxReclaimed;
            _runningSimulation.BodyOutside -= OnBodyOutside;
            _collisionObserver.CollidedWithPlayer -= OnCollidedWithPlayer;
        }

        private void OnCollidedWithPlayer(IBody body)
        {
            if (!_spawnedBonusBoxes.ContainsKey(body))
            {
                return;
            }
            
            _game.OpenBonusBox(_spawnedBonusBoxes[body]);
        }

        private void OnBodyOutside(IBody body)
        {
            if (!_spawnedBonusBoxes.ContainsKey(body))
            {
                return;
            }
            
            _game.ReclaimBonusBox(_spawnedBonusBoxes[body]);
        }

        private void OnBonusBoxSpawned(IBonusBox bonusBox) => _spawnedBonusBoxes.Add(bonusBox, bonusBox);
        private void OnBonusBoxReclaimed(IBonusBox bonusBox) => _spawnedBonusBoxes.Remove(bonusBox);
    }
}