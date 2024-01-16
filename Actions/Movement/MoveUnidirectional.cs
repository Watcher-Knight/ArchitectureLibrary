using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.moveUnidirectional)]
    public class MoveUnidirectional : Move, IAxisControllable
    {
        [SerializeField] private SingleAxis axis = SingleAxis.x;

        private float direction = 0;
        private void Update()
        {
            if (!usePhysics)
            {
                switch (axis)
                {
                    case SingleAxis.x: default: transform.position += Vector3.right * direction * speed.value * Time.deltaTime; break;
                    case SingleAxis.y: transform.position += Vector3.up * direction * speed.value * Time.deltaTime; break;
                }
            }
        }
        private void FixedUpdate()
        {
            if (usePhysics && rigidbody != null)
            {
                float targetSpeed;
                float speedDifference;
                float accelerationRate;
                float force;
                switch (axis)
                {
                    case SingleAxis.x:
                    default:
                        targetSpeed = direction * speed.value;
                        speedDifference = targetSpeed - rigidbody.velocity.x;
                        accelerationRate = (Math.Abs(targetSpeed) > 0) ? acceleration.value : deceleration.value;
                        accelerationRate /= Time.fixedDeltaTime;
                        force = Mathf.Pow(Math.Abs(speedDifference) * accelerationRate, control) * Math.Sign(speedDifference);
                        rigidbody.AddForce(Vector2.right * force);
                        break;
                    case SingleAxis.y:
                        targetSpeed = direction * speed.value;
                        speedDifference = targetSpeed - rigidbody.velocity.y;
                        accelerationRate = (Math.Abs(targetSpeed) > 0) ? acceleration.value : deceleration.value;
                        accelerationRate /= Time.fixedDeltaTime;
                        force = Mathf.Pow(Math.Abs(speedDifference) * accelerationRate, control) * Math.Sign(speedDifference);
                        rigidbody.AddForce(Vector2.up * force);
                        break;
                }
            }
        }

        public void Control(float value)
        {
            direction = Math.Sign(value);
        }
    }
}
