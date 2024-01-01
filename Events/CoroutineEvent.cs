using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.coroutineEvent)]
    public class CoroutineEvent : MonoBehaviour, IInvokeable
    {
        [SerializeField] private List<YieldEvent> actions = new List<YieldEvent>();
        private Coroutine current;

        public IEnumerator EventCoroutine()
        {
            foreach (YieldEvent action in actions)
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

                action.Invoke();
            }
        }

        public void Invoke() => current = StartCoroutine(EventCoroutine());
        public void Cancel() { if (current != null) StopCoroutine(current); }
    }

    [Serializable]
    public class YieldEvent
    {
        [SerializeField] private float _yieldTime = 0;
        public float yieldTime { get => _yieldTime; }
        [SerializeField] private TimeMeasurement _yieldType = TimeMeasurement.Seconds;
        public TimeMeasurement yieldType { get => _yieldType; }
        [SerializeField] private ActionList action;
        public void Invoke() => action.Invoke();
    }

    public enum TimeMeasurement
    {
        Seconds,
        Update,
        FixedUpdate
    }
}