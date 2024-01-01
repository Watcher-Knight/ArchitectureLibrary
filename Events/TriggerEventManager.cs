// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UltEvents;

// namespace ArchitectureLibrary
// {
//     [AddComponentMenu(ComponentPaths.triggerEventManager)]
//     public class TriggerEventManager : EventManager
//     {
//         [SerializeField] private List<TriggerEvent> events = new List<TriggerEvent>();
//         public List<Collider2D> colliders { get; private set; } = new List<Collider2D>();
//         public Dictionary<TriggerEventType, List<Tag>> eventConditions { get; private set; } = new Dictionary<TriggerEventType, List<Tag>>()
//         {
//             { TriggerEventType.Enter, new List<Tag>() },
//             { TriggerEventType.Exit, new List<Tag>() },
//             { TriggerEventType.Continuous, new List<Tag>() }
//         };

//         private void OnTriggerEnter2D(Collider2D other)
//         {
//             colliders.Add(other);

//             CheckForEvent(other, TriggerEventType.Enter);

//             AddConditions(other, TriggerEventType.Enter);
//             AddConditions(other, TriggerEventType.Continuous);
//         }

//         private void OnTriggerExit2D(Collider2D other)
//         {
//             colliders.Remove(other);

//             CheckForEvent(other, TriggerEventType.Exit);

//             AddConditions(other, TriggerEventType.Exit);
//             RemoveConditions(other, TriggerEventType.Continuous);
//         }

//         private void Update()
//         {
//             foreach (Collider2D other in colliders)
//             {
//                 CheckForEvent(other, TriggerEventType.Continuous);
//             }
//         }

//         private void LateUpdate()
//         {
//             eventConditions[TriggerEventType.Enter].Clear();
//             eventConditions[TriggerEventType.Exit].Clear();
//         }

//         protected override void OnValidate() => base.OnValidate();

//         private void CheckForEvent(Collider2D other, TriggerEventType eventType)
//         {
//             if (CheckConditions())
//             {
//                 Tags objectTags = other.gameObject.GetComponent<Tags>();
//                 if (objectTags != null)
//                 {
//                     foreach (TriggerEvent triggerEvent in events)
//                     {
//                         if (triggerEvent.eventType == eventType)
//                         {
//                             foreach (Tag tag in triggerEvent.triggerTags)
//                             {
//                                 if (objectTags.GetTags().Contains(tag))
//                                 {
//                                     triggerEvent.Invoke();
//                                     break;
//                                 }
//                             }
//                         }
//                     }
//                 }
//             }
//         }

//         private void AddConditions(Collider2D other, TriggerEventType eventType)
//         {
//             Tags objectTags = other.gameObject.GetComponent<Tags>();
//             if (objectTags != null)
//             {
//                 foreach (Tag objectTag in objectTags.GetTags())
//                 {
//                     eventConditions[eventType].Add(objectTag);
//                 }
//             }
//         }
//         private void RemoveConditions(Collider2D other, TriggerEventType eventType)
//         {
//             Tags objectTags = other.gameObject.GetComponent<Tags>();
//             if (objectTags != null)
//             {
//                 foreach (Tag objectTag in objectTags.GetTags())
//                 {
//                     eventConditions[eventType].Remove(objectTag);
//                 }
//             }
//         }
//     }

//     [Serializable]
//     public class TriggerEvent
//     {
//         [SerializeField] private TriggerEventType _eventType = TriggerEventType.Enter;
//         public TriggerEventType eventType { get { return _eventType; } }
//         [SerializeField] private List<Tag> _triggerTags = new List<Tag>();
//         public List<Tag> triggerTags { get { return _triggerTags; } }
//         [SerializeField] private UltEvent effect;

//         public void Invoke() { effect.Invoke(); }
//     }

//     public enum TriggerEventType
//     {
//         Enter,
//         Exit,
//         Continuous
//     }
// }