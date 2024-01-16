using UnityEngine;

namespace ArchitectureLibrary
{
    public static class Events
    {
        public static void SendMessage(GameObject gameObject, EventTag tag, bool cancel = false)
        {
            if (gameObject != null)
            {
                EventListener[] listeners = gameObject.GetComponents<EventListener>();
                foreach (EventListener listener in listeners)
                {
                    if (listener.eventTag != null && listener.eventTag == tag) if (cancel) listener.Cancel(); else listener.Invoke();
                }
            }
        }
        public static void SendMessage(Component component, EventTag tag, bool cancel = false)
        {
            SendMessage(component?.gameObject, tag, cancel);
        }

        public static void SendMessage<T>(GameObject gameObject, EventTag tag, T arg, bool cancel = false)
        {
            if (gameObject != null)
            {
                EventListener<T>[] listeners = gameObject.GetComponents<EventListener<T>>();
                foreach (EventListener<T> listener in listeners)
                {
                    if (listener.eventTag != null && listener.eventTag == tag) if (cancel) listener.Cancel(); else listener.Invoke(arg);
                }
            }
        }
        public static void SendMessage<T>(Component component, EventTag tag, T arg, bool cancel = false)
        {
            SendMessage(component?.gameObject, tag, arg, cancel);
        }
    }
}