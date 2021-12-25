using System;
using AnotherRunner.Model.Bonuses;
using AnotherRunner.Model.Bonuses.BonusBoxes;
using AnotherRunner.Model.Levels;
using AnotherRunner.Model.Simulations;
using UnityEngine;

namespace AnotherRunner.Model.Spawners
{
    public class BonusBoxSpawner : ISpawner<IBonusBox>
    {
        public event Action<IBonusBox> Spawned;
        public event Action<IBonusBox> Reclaimed;
        
        private readonly RunningSimulation _runningSimulation;
        private readonly CollisionObserver _collisionObserver;
        private readonly LevelInfo _levelInfo;
        private readonly Vector2 _bonusBoxSize = Vector2.one;

        public BonusBoxSpawner(RunningSimulation runningSimulation, CollisionObserver collisionObserver, LevelInfo levelInfo)
        {
            _runningSimulation = runningSimulation;
            _collisionObserver = collisionObserver;
            _levelInfo = levelInfo;
        }

        public void Spawn()
        {
            var bonusBox = CreateBonusBox();

            _runningSimulation.AddBody(bonusBox);
            _collisionObserver.BindWithPlayer(bonusBox);
            
            Spawned?.Invoke(bonusBox);
        }

        public void Reclaim(IBonusBox reclaimable)
        {
            _runningSimulation.RemoveBody(reclaimable);
            _collisionObserver.UntieFromPlayer(reclaimable);
            
            Reclaimed?.Invoke(reclaimable);
        }

        private RandomBonusBox CreateBonusBox()
        {
            var position = _levelInfo.spawnPoint;
            position.y = _levelInfo.groundLevel + _bonusBoxSize.y / 2f;

            var bonusBox = new RandomBonusBox(position, _bonusBoxSize);
            
            return bonusBox;
        }
    }
}