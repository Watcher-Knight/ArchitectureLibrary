using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.startEventManager)]
    public class StartEventManager : EventManager
    {
        [SerializeField] private UltEvent effect = new UltEvent();

        private void Start()
        {
            if (debugMode)
            {
                if (CheckConditions()) Debug.Log("Conditions are all positive.");
            }

            if (CheckConditions()) effect.Invoke();
        }
        public void Invoke() => effect.Invoke();
        protected override void OnValidate() => base.OnValidate();

        public void Test() => Debug.Log("Testing");
    }
}