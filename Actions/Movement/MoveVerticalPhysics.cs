using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Movement/Move Vertical (Physics)")]
    public class MoveVerticalPhysics : AxisAction
    {
        [SerializeField] private MoveStats stats;
        [SerializeField] private new Rigidbody2D rigidbody;

        public override void CreateStats()
        {
            string path = AssetPaths.stats;
            string name = $"{transform.root.gameObject.name}Move";
            stats = ScriptableObjectFactory.Create<MoveStats>(path, name);
        }

        private float control = 1;
        private float direction = 0;
        public override bool isActive => direction != 0 && control > 0;

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
            float targetSpeed = direction * stats.speed;
            float speedDifference = targetSpeed - rigidbody.velocity.y;
            float accelerationRate = (Math.Abs(targetSpeed) > 0) ? stats.acceleration : stats.deceleration;
            accelerationRate /= Time.fixedDeltaTime;
            float force = Mathf.Pow(Math.Abs(speedDifference) * accelerationRate, control) * Math.Sign(speedDifference);
            rigidbody.AddForce(Vector2.up * force);
        }

        public override void Invoke(float value)
        {
            direction = Math.Sign(value);
        }

        public void SetControl(float value)
        {
            if (value < 0) value = 0;
            if (value > 1) value = 1;
            control = value;
        }
    }
}
