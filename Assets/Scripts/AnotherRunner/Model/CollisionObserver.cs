using System;
using System.Collections.Generic;
using AnotherRunner.Model.Bodies;
using AnotherRunner.Model.Players;
using AnotherRunner.Model.Simulations;

namespace AnotherRunner.Model
{
    public class CollisionObserver : IDisposable
    {
        public event Action<IBody> CollidedWithPlayer;

        private readonly IPlayer _player;
        private readonly CollisionSimulation _collisionSimulation;
        private readonly HashSet<IBody> _bindedWithPlayer = new HashSet<IBody>();

        public CollisionObserver(IPlayer player, CollisionSimulation collisionSimulation)
        {
            _player = player;
            _collisionSimulation = collisionSimulation;

            _collisionSimulation.CollisionsDetected += OnCollisionsDetected;
        }

        public void Dispose()
        {
            _collisionSimulation.CollisionsDetected -= OnCollisionsDetected;
        }

        public void BindWithPlayer(IBody body)
        {
            _bindedWithPlayer.Add(body);
            _collisionSimulation.AddBody(body);
        }

        public void UntieFromPlayer(IBody body)
        {
            _bindedWithPlayer.Remove(body);
            _collisionSimulation.RemoveBody(body);
        }

        private void OnCollisionsDetected(IEnumerable<(IBody, IBody)> collisions)
        {
            foreach (var collision in collisions)
            {
                if (!IsBindedWithPlayer(collision, out IBody other))
                {
                    continue;
                }
                
                CollidedWithPlayer?.Invoke(other);
            }
        }

        private bool IsBindedWithPlayer((IBody, IBody) collision, out IBody other)
        {
            if (_bindedWithPlayer.Contains(collision.Item1))
            {
                other = collision.Item1;
                return collision.Item2 is IPlayer;
            }
            
            if (_bindedWithPlayer.Contains(collision.Item2))
            {
                other = collision.Item2;
                return collision.Item1 is IPlayer;
            }

            other = null;
            return false;
        }
    }
}