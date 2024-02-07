using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [Serializable]
    public struct Percentage
    {
        [SerializeField] private float b_Value;
        private float Value
        {
            get => b_Value;
            set
            {
                if (value > 1) value = 1;
                if (value < 0) value = 0;
                b_Value = value;
            }
        }
        public bool Max => Value == 1;
        public bool Min => Value == 0;

        public static implicit operator float(Percentage percentage) => percentage.Value;
        //public static explicit operator int(Percentage percentage) => Math.Sign(percentage);
        public static implicit operator Percentage(float value) => new() { Value = value };
        public static implicit operator Percentage(int value) => new() { Value = value };

        public static Percentage operator +(Percentage a, Percentage b) => new() { Value = a.Value + b.Value };
        public static Percentage operator -(Percentage a, Percentage b) => new() { Value = a.Value - b.Value };
        public static float operator +(Percentage p, float f) => p.Value + f;
        public static float operator +(float f, Percentage p) => p.Value + f;
        public static float operator -(Percentage p, float f) => p.Value - f;
        public static float operator -(float f, Percentage p) => f - p.Value;
        public static Percentage operator *(Percentage a, Percentage b) => new() { Value = a.Value * b.Value };
        public static Percentage operator /(Percentage a, Percentage b) => new() { Value = a.Value / b.Value };
        public static float operator *(Percentage p, float f) => p.Value * f;
        public static float operator *(float f, Percentage p) => p.Value * f;
        public static float operator /(Percentage p, float f) => p.Value / f;
        public static float operator /(float f, Percentage p) => f / p.Value;
        public override string ToString() => $"%{Value * 100}";
    }
}