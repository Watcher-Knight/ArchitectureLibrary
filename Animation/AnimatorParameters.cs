using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [Serializable]
    public class AnimatorParameter
    {
        [SerializeField] protected Animator Animator;
        [SerializeField] protected string Name;
    }

    [Serializable]
    public class AnimatorBoolParameter : AnimatorParameter
    {
        public void SetValue(bool value) => Animator.SetBool(Name, value);
    }
    [Serializable]
    public class AnimatorFloatParameter : AnimatorParameter
    {
        public void SetValue(float value) => Animator.SetFloat(Name, value);
    }
    [Serializable]
    public class AnimatorIntParameter : AnimatorParameter
    {
        public void SetValue(int value) => Animator.SetInteger(Name, value);
    }
    [Serializable]
    public class AnimatorTriggerParameter : AnimatorParameter
    {
        public void Activate() => Animator.SetTrigger(Name);
    }
}