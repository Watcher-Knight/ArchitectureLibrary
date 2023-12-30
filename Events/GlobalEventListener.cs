using UnityEngine;
using UltEvents;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.globalEventListener)]
    public class GlobalEventListener : EventManager
    {
        [SerializeField] private GlobalEvent _globalEvent;
        public GlobalEvent globalEvent
        {
            get
            {
                if (_globalEvent != null) return _globalEvent;
                _globalEvent = ScriptableObject.CreateInstance<GlobalEvent>();
                return _globalEvent;
            }
        }
        [SerializeField] private UltEvent effect;

        private void OnEnable() => globalEvent.effect += Invoke;

        private void OnDisable() => globalEvent.effect -= Invoke;

        public void Invoke() { if (CheckConditions()) effect.Invoke(); }
    }
}