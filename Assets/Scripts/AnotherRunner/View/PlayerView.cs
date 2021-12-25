using System;
using AnotherRunner.Model.Players;
using UnityEngine;

namespace AnotherRunner.View
{
    public class PlayerView : MonoBehaviour
    {
        private IPlayer _player;
        private Transform _transform;

        public void Init(IPlayer player)
        {
            _player = player;
        }

        private void Awake() => _transform = transform;

        private void Start()
        {
            _transform.localScale = _player.Size;
        }

        private void Update()
        {
            _transform.position = _player.Position;
        }
    }
}
