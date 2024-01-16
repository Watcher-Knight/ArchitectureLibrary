using System;
using System.Collections;
using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Move : MonoBehaviour
    {
        [SerializeField] protected bool usePhysics;
        [SerializeField] protected VariableReference<float> speed;
        [SerializeField][DrawIf("usePhysics")] new protected Rigidbody2D rigidbody;
        [SerializeField][DrawIf("usePhysics")] protected PercentReference acceleration;
        [SerializeField][DrawIf("usePhysics")] protected PercentReference deceleration;

        protected float control = 1f;
        private Coroutine setControlCoroutine;

        public void SetControl(float value, float seconds = 0)
        {
            if (setControlCoroutine != null) StopCoroutine(setControlCoroutine);
            setControlCoroutine = StartCoroutine(_SetControl(value, seconds));
        }

        private IEnumerator _SetControl(float value, float seconds)
        {
            float target = value;
            if (target < 0) target = 0;
            if (target > 1) target = 1;
            float difference = target - control;

            int sign = Math.Sign(difference);
            while (control * sign < target * sign)
            {
                yield return new WaitForFixedUpdate();
                control += difference / seconds * Time.fixedDeltaTime;
            }

            if (control != target) control = target;
        }
    }
}