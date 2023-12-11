using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Event Managers/Start Event Manager")]
    public class StartEventManager : EventManager
    {
        [SerializeField] private UnityEvent effect = new UnityEvent();

        private void Start()
        {
            if (debugMode)
            {
                if (CheckConditions()) Debug.Log("Conditions are all positive.");
            }

            if (CheckConditions()) effect.Invoke();
        }
        protected override void OnValidate() => base.OnValidate();

        public void AddEvent(UnityAction action) => UnityEditor.Events.UnityEventTools.AddPersistentListener(effect, action);
        public void Test() => Debug.Log("Testing");
    }
}