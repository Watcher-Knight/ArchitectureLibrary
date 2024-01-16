using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.listenerEventManager)]
    public class ListenerEventManager : EventListener
    {
        [SerializeField] private ActionList action;
        public override void Invoke() => action.Invoke();
        public override void Cancel() => action.Cancel();
    }
}