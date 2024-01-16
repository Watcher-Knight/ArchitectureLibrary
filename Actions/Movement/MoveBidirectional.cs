using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.moveBidirectional)]
    public class MoveBidirectional : Move, IAxisControllable2D
    {
        private Vector2 direction = Vector2.zero;

        private void Update()
        {
            if (!usePhysics)
            {
                transform.position += new Vector3(direction.x, direction.y, 0) * speed.value * Time.deltaTime;
            }
        }
        private void FixedUpdate()
        {
            if (usePhysics && rigidbody != null)
            {
                Vector2 targetSpeed = direction * speed.value;
                Vector2 speedDifference = targetSpeed - rigidbody.velocity;
                Vector2 accelerationRate;
                accelerationRate.x = (Math.Abs(targetSpeed.x) > 0) ? acceleration.value : deceleration.value;
                accelerationRate.y = (Math.Abs(targetSpeed.y) > 0) ? acceleration.value : deceleration.value;
                accelerationRate /= Time.fixedDeltaTime;
                Vector2 force;
                force.x = Mathf.Pow(Math.Abs(speedDifference.x) * accelerationRate.x, control) * Math.Sign(speedDifference.x);
                force.y = Mathf.Pow(Math.Abs(speedDifference.y) * accelerationRate.y, control) * Math.Sign(speedDifference.y);
                rigidbody.AddForce(force);
            }
        }

        public void Control(Vector2 value)
        {
            if (Math.Abs(value.x) > 1) value.x = Math.Sign(value.x);
            if (Math.Abs(value.y) > 1) value.y = Math.Sign(value.y);

            direction = value;
        }
    }
}