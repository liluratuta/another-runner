using System;
using UnityEngine;

namespace AnotherRunner.Model.Levels
{
    [Serializable]
    public struct LevelInfo
    {
        public float groundLevel;
        public float leftSide;
        public float gravitation;
        public Rect worldBounds;
        public Vector2 spawnPoint;
        public Vector2 playerSpawnPoint;
    }
}