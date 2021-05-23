using System;
using UnityEngine;

namespace Source.Common
{
    [Serializable]
    public class Bounds2D
    {
        public Vector2 center;
        public Vector2 extents;

        public Vector2 Size => 2 * extents;
        public Vector2 Max => center + extents;
        public Vector2 Min => center - extents;

        public bool Contains(Vector2 other)
        {
            return Max.x >= other.x &&
                   Min.x <= other.x &&
                   Max.y >= other.y &&
                   Min.y <= other.y;
        }
    }
}