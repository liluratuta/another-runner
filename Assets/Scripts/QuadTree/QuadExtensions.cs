using UnityEngine;

namespace QuadTree
{
    public static class QuadExtensions
    {
        public static void SetFromRect(this ref Quad quad, Rect rect)
        {
            quad.MinX = rect.xMin;
            quad.MaxX = rect.xMax;
            quad.MinY = rect.yMin;
            quad.MaxY = rect.yMax;
        }
    }
}