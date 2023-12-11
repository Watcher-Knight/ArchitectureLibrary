using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Event Managers/Update Event Manager")]
    public class UpdateEventManager : EventManager
    {
        [SerializeField] private UnityEvent effect;

        private void Update()
        {
            if (debugMode)
            {
                if (CheckConditions()) Debug.Log("Conditions are all positive.");
            }

            if (CheckConditions()) effect.Invoke();
        }

        protected override void OnValidate() => base.OnValidate();
    }
}