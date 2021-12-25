using UnityEngine;

namespace AnotherRunner.Model.Bodies
{
    public interface IBody
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; }
    }
}