using UnityEngine;

namespace ArchitectureLibrary
{
    public static class TransformExtensions
    {
        public static void Flip(this Transform transform) =>
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}