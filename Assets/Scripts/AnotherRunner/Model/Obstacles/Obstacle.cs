using UnityEngine;

namespace AnotherRunner.Model.Obstacles
{
    public class Obstacle : IObstacle
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; }

        public Obstacle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
    }
}