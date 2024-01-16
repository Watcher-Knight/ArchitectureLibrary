using UnityEngine;
using UnityEditor;
using System.Linq.Expressions;

namespace ArchitectureLibrary
{
    public static class Components
    {
        public static T AssignComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out T component)) return component;
            return gameObject.AddComponent<T>();
        }
        public static T AssignRootComponent<T>(GameObject gameObject) where T : Component
        {
            GameObject root = gameObject.transform.root.gameObject;
            if (root.TryGetComponent<T>(out T component)) return component;
            return root.AddComponent<T>();
        }
    }


    public static class ComponentPaths
    {
        public const string events = "Event Managers";

        public const string eventManager = events + "/Event Manager";
        public const string startEventManager = events + "/Start Event Manager";
        public const string updateEventManager = events + "/Update Event Manager";
        public const string buttonEventManager = events + "/Button Event Manager";
        public const string axisEventManager = events + "/Axis Event Manager";
        public const string v2AxisEventManager = events + "/Axis Event Manager (Vector2)";
        public const string triggerEventManager = events + "/Trigger Event Manager";
        public const string collisionEventManager = events + "/Collision Event Manager";
        public const string listenerEventManager = events + "/Listener Event Manager";
        public const string coroutineEvent = events + "/Coroutine Event Manager";
        public const string globalEventListener = events + "/Global Event Listener";
        public const string conditionList = events + "/Conditions";


        public const string stateMachine = "State Machine";

        public const string stateMachineEvent = stateMachine + "/State Machine Event";


        public const string movement = "Movement";

        public const string moveUnidirectional = movement + "/Move Unidirectional";
        public const string moveBidirectional = movement + "/Move Bidirectional";
        public const string launch = movement + "/Launch";


        public const string tags = "Tags";
        public const string instanceStats = "Stats";
    }
}