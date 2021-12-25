using System.Collections.Generic;
using AnotherRunner.Model.Simulations;
using UnityEngine;

namespace AnotherRunner.Model.Core
{
    public class SimulationsUpdater : MonoBehaviour
    {
        private readonly List<ISimulation> _simulations = new List<ISimulation>();
        [SerializeField, Range(0f, 4f)] private float _deltaTimeScale = 1f;

        public void Add(ISimulation simulation) => _simulations.Add(simulation);

        private void Update()
        {
            foreach (var simulation in _simulations)
            {
                simulation.Update(Time.deltaTime * _deltaTimeScale);
            }
        }
    }
}