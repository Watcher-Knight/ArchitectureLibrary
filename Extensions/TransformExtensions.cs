using UnityEngine;

namespace ArchitectureLibrary
{
    public static class TransformExtensions
    {
        public static void Flip(this Transform transform) =>
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
    }
}