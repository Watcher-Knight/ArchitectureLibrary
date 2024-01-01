using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.startEventManager)]
    public class StartEventManager : EventManager
    {
        [SerializeField] private ActionList action;

        private void Start()
        {
            if (CheckConditions()) action.Invoke();
        }
    }
}