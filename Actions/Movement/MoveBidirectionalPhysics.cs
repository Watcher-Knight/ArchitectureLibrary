using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.moveBidirectionalPhysics)]
    public class MoveBidirectionalPhysics : V2AxisAction
    {
        [SerializeField] private MoveStats stats;
        [SerializeField] new private Rigidbody2D rigidbody;

        protected override void CreateStats()
        {
            string path = AssetPaths.stats;
            string name = $"{transform.root.name}Move";
            stats = ScriptableObjectFactory.Create<MoveStats>(path, name);
        }

        private float control = 1;
        private Vector2 direction = Vector2.zero;
        public override bool isActive => direction != Vector2.zero && control > 0;

        private void Start()
        {
            if (stats == null) stats = ScriptableObject.CreateInstance<MoveStats>();

            if (rigidbody == null)
            {
                bool shouldRemoveGravity = (!transform.root.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb));
                rigidbody = Components.AssignRootComponent<Rigidbody2D>(gameObject);
                if (shouldRemoveGravity) rigidbody.gravityScale = 0;
            }
        }

        private void FixedUpdate()
        {
            Vector2 targetSpeed = direction * stats.speed;
            Vector2 speedDifference = targetSpeed - rigidbody.velocity;
            Vector2 accelerationRate;
            accelerationRate.x = (Math.Abs(targetSpeed.x) > 0) ? stats.acceleration : stats.deceleration;
            accelerationRate.y = (Math.Abs(targetSpeed.y) > 0) ? stats.acceleration : stats.deceleration;
            accelerationRate /= Time.fixedDeltaTime;
            Vector2 force;
            force.x = Mathf.Pow(Math.Abs(speedDifference.x) * accelerationRate.x, control) * Math.Sign(speedDifference.x);
            force.y = Mathf.Pow(Math.Abs(speedDifference.y) * accelerationRate.y, control) * Math.Sign(speedDifference.y);
            rigidbody.AddForce(force);
        }

        public override void Invoke(Vector2 value)
        {
            if (Math.Abs(value.x) > 1) value.x = Math.Sign(value.x);
            if (Math.Abs(value.y) > 1) value.y = Math.Sign(value.y);

            direction = value;
        }

        public void SetControl(float value)
        {
            if (value < 0) value = 0;
            if (value > 1) value = 1;
            control = value;
        }
    }
}
