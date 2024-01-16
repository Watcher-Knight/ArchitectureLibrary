using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class EventListener : MonoBehaviour, IInvokeable
    {
        [SerializeField] private EventTag _eventTag;
        public EventTag eventTag => _eventTag;
        public abstract void Invoke();
        public virtual void Cancel() { }
    }

    public abstract class EventListener<T> : MonoBehaviour, IInvokeable<T>
    {
        [SerializeField] private EventTag _eventTag;
        public EventTag eventTag => _eventTag;
        public abstract void Invoke(T value);
        public virtual void Cancel() { }
    }
}