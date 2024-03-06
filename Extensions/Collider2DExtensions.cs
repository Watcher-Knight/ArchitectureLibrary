using UnityEngine;

namespace ArchitectureLibrary
{
    public static class Collider2DExtensions
    {
        public static Rigidbody2D GetRigidbody(this Collider2D collider)
        {
            if (collider.attachedRigidbody != null)
                return collider.attachedRigidbody;
            return collider.AddRigidbody2D();
        }
    }
}