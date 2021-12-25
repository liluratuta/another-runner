using AnotherRunner.Model.Levels;
using AnotherRunner.Model.Players;
using UnityEngine;

namespace AnotherRunner.Model.Simulations
{
    public class PlayerGravitySimulation : ISimulation
    {
        private readonly IJumper _jumper;
        private readonly LevelInfo _levelInfo;
        
        public PlayerGravitySimulation(IJumper jumper, LevelInfo levelInfo)
        {
            _jumper = jumper;
            _levelInfo = levelInfo;
        }

        public void Update(float dt)
        {
            if (IsRest())
            {
                return;
            }

            UpdateJumpSpeed(dt);
            ApplySpeed(dt);
        }

        public void Jump()
        {
            if (!IsGrounded())
            {
                return;
            }

            _jumper.JumpSpeed = Mathf.Sqrt(2 * _jumper.JumpHeight * _levelInfo.gravitation);
        }

        private void UpdateJumpSpeed(float dt)
        {
            _jumper.JumpSpeed -= _levelInfo.gravitation * dt;
        }

        private void ApplySpeed(float dt)
        {
            var position = _jumper.Position;
            position.y += _jumper.JumpSpeed * dt;

            position.y = ClampY(position.y);

            _jumper.Position = position;
            
            if (IsGrounded())
            {
                _jumper.JumpSpeed = 0;
            }
            
            float ClampY(float y)
            {
                var minLevel = _levelInfo.groundLevel + _jumper.Size.y / 2f;
                return y < minLevel ? minLevel : y;
            }
        }

        private bool IsRest()
        {
            return Mathf.Abs(_jumper.JumpSpeed) <= Mathf.Epsilon && IsGrounded();
        }

        private bool IsGrounded()
        {
            return Mathf.Abs(_jumper.FootLevel - _levelInfo.groundLevel) < Mathf.Epsilon;
        }
    }
}