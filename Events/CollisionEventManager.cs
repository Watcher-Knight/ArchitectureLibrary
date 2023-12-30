using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.collisionEventManager)]
    public class CollisionEventManager : EventManager
    {
        [SerializeField] private List<CollisionEvent> events = new List<CollisionEvent>();
        public List<Collider2D> colliders { get; private set; } = new List<Collider2D>();
        public Dictionary<CollisionEventType, List<Tag>> eventConditions { get; private set; } = new Dictionary<CollisionEventType, List<Tag>>()
        {
            { CollisionEventType.Enter, new List<Tag>() },
            { CollisionEventType.Exit, new List<Tag>() },
            { CollisionEventType.Continuous, new List<Tag>() }
        };

        private void OnCollisionEnter2D(Collision2D other)
        {
            colliders.Add(other.collider);

            CheckForEvent(other.collider, CollisionEventType.Enter);

            AddConditions(other.collider, CollisionEventType.Enter);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            colliders.Remove(other.collider);

            CheckForEvent(other.collider, CollisionEventType.Exit);

            AddConditions(other.collider, CollisionEventType.Exit);
        }

        private void Update()
        {
            foreach (Collider2D other in colliders)
            {
                CheckForEvent(other, CollisionEventType.Continuous);

                AddConditions(other, CollisionEventType.Continuous);
            }
        }

        private void LateUpdate()
        {
            foreach (KeyValuePair<CollisionEventType, List<Tag>> eventCondition in eventConditions)
            {
                eventCondition.Value.Clear();
            }
        }

        protected override void OnValidate() => base.OnValidate();

        private void CheckForEvent(Collider2D other, CollisionEventType eventType)
        {
            if (CheckConditions())
            {
                Tags objectTags = other.gameObject.GetComponent<Tags>();
                if (objectTags != null)
                {
                    foreach (CollisionEvent CollisionEvent in events)
                    {
                        if (CollisionEvent.eventType == eventType)
                        {
                            foreach (Tag tag in CollisionEvent.CollisionTags)
                            {
                                if (objectTags.GetTags().Contains(tag))
                                {
                                    CollisionEvent.Invoke();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddConditions(Collider2D other, CollisionEventType eventType)
        {
            Tags objectTags = other.gameObject.GetComponent<Tags>();
            if (objectTags != null)
            {
                foreach (Tag objectTag in objectTags.GetTags())
                {
                    eventConditions[eventType].Add(objectTag);
                }
            }
        }
    }

    [Serializable]
    public class CollisionEvent
    {
        [SerializeField] private CollisionEventType _eventType = CollisionEventType.Enter;
        public CollisionEventType eventType { get { return _eventType; } }
        [SerializeField] private List<Tag> _CollisionTags = new List<Tag>();
        public List<Tag> CollisionTags { get { return _CollisionTags; } }
        [SerializeField] private UltEvent effect;

        public void Invoke() { effect.Invoke(); }
    }

    public enum CollisionEventType
    {
        Enter,
        Exit,
        Continuous
    }
}