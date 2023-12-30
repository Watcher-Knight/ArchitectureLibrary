using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Action : MonoBehaviour
    {
        public abstract bool isActive { get; }

        [ContextMenu("Create Stats")] void _CreateStats() => CreateStats();

        protected abstract void CreateStats();
    }
}