using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public static class Components
    {
        public static T AssignComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out T component)) return component;
            return gameObject.AddComponent<T>();
        }
        public static T AssignRootComponent<T>(GameObject gameObject) where T : Component
        {
            GameObject root = gameObject.transform.root.gameObject;
            if (root.TryGetComponent<T>(out T component)) return component;
            return root.AddComponent<T>();
        }
    }
}