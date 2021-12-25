using System;
using AnotherRunner.Model.Bonuses;

namespace AnotherRunner.Model.Spawners
{
    public interface ISpawner<T>
    {
        public event Action<T> Spawned;
        public event Action<T> Reclaimed;
        
        void Spawn();
        void Reclaim(T reclaimable);
    }
}