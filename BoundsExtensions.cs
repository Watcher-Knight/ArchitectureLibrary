using UnityEngine;

namespace ArchitectureLibrary
{
    public static class BoundsExtensions
    {
        public static Vector2 BottomCenter(this Bounds bounds) =>
            bounds.center - new Vector3(0, bounds.extents.y, 0);
    }
}