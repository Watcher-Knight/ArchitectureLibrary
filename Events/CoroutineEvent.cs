using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineEvent : EventManager
{
    [SerializeField] private List<EditorCoroutine> coroutines = new List<EditorCoroutine>();

    public void Invoke()
    {
        if (CheckConditions()) StartCoroutine(coroutines[0].EventCoroutine());
    }
    public void Invoke(string name)
    {
        if (CheckConditions())
        {
            foreach (EditorCoroutine coroutine in coroutines)
            {
                if (coroutine.name == name)
                {
                    StartCoroutine(coroutine.EventCoroutine());
                    return;
                }
            }
            StartCoroutine(coroutines[0].EventCoroutine());
        }
    }

    [ContextMenu(nameof(AddCoroutine))] void AddCoroutine() => coroutines.Add(new EditorCoroutine());
}

[Serializable]
public class EditorCoroutine
{
    [SerializeField] private string _name = "Coroutine";
    public string name { get => _name; }
    [SerializeField] private List<YieldEventPair> actions = new List<YieldEventPair>();

    public IEnumerator EventCoroutine()
    {
        foreach (YieldEventPair action in actions)
        {
            switch (action.yieldType)
            {
                case TimeMeasurement.Seconds:
                default:
                    yield return new WaitForSeconds(action.yieldTime);
                    break;
                case TimeMeasurement.Update:
                    for (int i = 0; i < (int)Math.Floor(action.yieldTime); i++)
                    {
                        yield return null;
                    }
                    break;
                case TimeMeasurement.FixedUpdate:
                    for (int i = 0; i < (int)Math.Floor(action.yieldTime); i++)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    break;
            }

            action.effect.Invoke();
        }
    }
}

[Serializable]
public class YieldEventPair
{
    [SerializeField] private float _yieldTime = 0;
    public float yieldTime { get => _yieldTime; }
    [SerializeField] private TimeMeasurement _yieldType = TimeMeasurement.Seconds;
    public TimeMeasurement yieldType { get => _yieldType; }
    [SerializeField] private UnityEvent _effect;
    public UnityEvent effect { get => _effect; }
}

public enum TimeMeasurement
{
    Seconds,
    Update,
    FixedUpdate
}