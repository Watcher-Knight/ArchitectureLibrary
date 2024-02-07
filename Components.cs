using UnityEngine;

namespace ArchitectureLibrary
{
    public static class Components
    {
        public static T AddComponent<T>(this Component component) where T : Component =>
            component.gameObject.AddComponent<T>();

        public static Rigidbody2D AddRigidbody2D(this GameObject gameObject, float gravityScale = 0)
        {
            Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
            return rigidbody;
        }
        public static Rigidbody2D AddRigidbody2D(this Component component, float gravityScale = 0) =>
            component.gameObject.AddRigidbody2D(gravityScale);
        
        public static Rigidbody2D GetRigidbody(this Collider2D collider)
        {
            if (collider.attachedRigidbody != null)
                return collider.attachedRigidbody;
            return collider.AddRigidbody2D();
        }
    }
}