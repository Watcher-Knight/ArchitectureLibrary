using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartEventManager : EventManager
{
    [SerializeField] private UnityEvent effect;

    private void Start()
    {
        if (debugMode)
        {
            if (CheckConditions()) Debug.Log("Conditions are all positive.");
        }

        if (CheckConditions()) effect.Invoke();
    }
}
