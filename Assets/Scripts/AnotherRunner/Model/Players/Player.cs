using UnityEngine;

namespace AnotherRunner.Model.Players
{
    public class Player : IPlayer
    {
        public float RunningSpeed => MultipliableRunningSpeed.Value;
        public float JumpHeight => MultipliableJumpHeight.Value;
        public Vector2 Position { get; set; }
        public Vector2 Size { get; }
        public float JumpSpeed { get; set; }
        public float FootLevel => Position.y - Size.y / 2f;

        public float HP { get; private set; }
        public Wallet Wallet { get; } = new Wallet(0);
        public MultipliableValue MultipliableRunningSpeed { get; }
        public MultipliableValue MultipliableJumpHeight { get; }

        public Player(Vector2 position, Vector2 size, float runningSpeed, float jumpHeight)
        {
            Position = position;
            Size = size;
            HP = 100f;

            MultipliableRunningSpeed = new MultipliableValue(runningSpeed);
            MultipliableJumpHeight = new MultipliableValue(jumpHeight);
        }

        public void ApplyDamage(float damage)
        {
            HP -= damage;
        }
    }
}
