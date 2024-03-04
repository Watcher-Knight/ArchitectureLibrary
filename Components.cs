using UnityEngine;

namespace ArchitectureLibrary
{
    public static class Components
    {
        public static T GetComponentInGroup<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent(out T component)) return component;
            if (gameObject.TryGetParentComponent(out T parentComponent)) return parentComponent;
            if (gameObject.TryGetChildComponent(out T childComponent)) return childComponent;
            return null;
        }
        public static bool TryGetComponentInGroup<T>(this GameObject gameObject, out T component) where T : Component => (component = gameObject.GetComponentInGroup<T>()) != null;
        public static T GetComponentInGroup<T>(this Component origin) where T : Component => origin.gameObject.GetComponentInGroup<T>();
        public static bool TryGetComponentInGroup<T>(this Component origin, out T component) where T : Component => origin.gameObject.TryGetComponentInGroup(out component);
        public static bool TryGetParentComponent<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = gameObject.GetComponentInParent<T>();
            return component != null;
        }
        public static bool TryGetParentComponent<T>(this Component origin, out T component) where T : Component =>
            origin.gameObject.TryGetParentComponent(out component);
        public static bool TryGetChildComponent<T>(this GameObject gameObject, out T component) where T : Component
        {
            component = gameObject.GetComponentInChildren<T>();
            return component != null;
        }
        public static bool TryGetChildComponent<T>(this Component origin, out T component) where T : Component =>
            origin.gameObject.TryGetChildComponent(out component);

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