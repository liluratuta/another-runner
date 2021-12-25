using AnotherRunner.Model.Bodies;
using UnityEngine;

namespace AnotherRunner.Model.Players
{
    public interface IJumper : IBody
    {
        public float JumpSpeed { get; set; }
        public float JumpHeight { get; }
        public float FootLevel { get; }
    }
}