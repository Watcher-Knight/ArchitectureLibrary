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
        public bool GetValue() => Animator.GetBool(Name);
        public bool Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }
    [Serializable]
    public class AnimatorFloatParameter : AnimatorParameter
    {
        public void SetValue(float value) => Animator.SetFloat(Name, value);
        public float GetValue() => Animator.GetFloat(Name);
        public float Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }
    [Serializable]
    public class AnimatorIntParameter : AnimatorParameter
    {
        public void SetValue(int value) => Animator.SetInteger(Name, value);
        public int GetValue() => Animator.GetInteger(Name);
        public int Value
        {
            get => GetValue();
            set => SetValue(value);
        }
    }
    [Serializable]
    public class AnimatorTriggerParameter : AnimatorParameter
    {
        public void Activate() => Animator.SetTrigger(Name);
    }
}