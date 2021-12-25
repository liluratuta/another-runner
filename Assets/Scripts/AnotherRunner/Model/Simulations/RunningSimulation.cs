using System;
using System.Collections.Generic;
using AnotherRunner.Model.Bodies;
using AnotherRunner.Model.Levels;
using AnotherRunner.Model.Players;
using UnityEngine;

namespace AnotherRunner.Model.Simulations
{
    public class RunningSimulation : ISimulation
    {
        public event Action<IBody> BodyOutside; 

        private readonly List<IBody> _bodies = new List<IBody>();
        private readonly IRunner _runner;
        private readonly LevelInfo _levelInfo;
        private readonly Vector2 _runningDirection = Vector2.left;
        private readonly Queue<IBody> _outsideObjects = new Queue<IBody>();

        public RunningSimulation(IRunner runner, LevelInfo levelInfo)
        {
            _runner = runner;
            _levelInfo = levelInfo;
        }

        public void Update(float dt)
        {
            PerformBodiesSimulation(dt);
        }

        public void AddBody(IBody body) => _bodies.Add(body);

        public void RemoveBody(IBody body) => _bodies.Remove(body);

        private void PerformBodiesSimulation(float dt)
        {
            foreach (var body in _bodies)
            {
                body.Position += _runningDirection * _runner.RunningSpeed * dt;

                if (body.Position.x > _levelInfo.leftSide)
                {
                    continue;
                }

                _outsideObjects.Enqueue(body);
            }

            while (_outsideObjects.Count != 0)
            {
                BodyOutside?.Invoke(_outsideObjects.Dequeue());
            }
        }
    }
}