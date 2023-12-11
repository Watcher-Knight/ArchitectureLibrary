using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Action : MonoBehaviour
    {
        public abstract bool isActive { get; }

        [ContextMenu("Create Stats")] void _CreateStats() => CreateStats();

        public virtual void CreateStats() {}

        protected T AssignComponent<T>() where T : Component
        {
            if (GetComponent<T>() != null) return GetComponent<T>();
            if (transform.root.gameObject.GetComponent<T>() != null) return transform.root.GetComponent<T>();
            return transform.root.gameObject.AddComponent<T>();
        }
        protected T AssignComponent<T>(GameObject target) where T : Component
        {
            if (target.GetComponent<T>() != null) return target.GetComponent<T>();
            return target.AddComponent<T>();
        }
    }
}