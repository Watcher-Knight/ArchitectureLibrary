using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Variable<T> : Variable
    {
        public abstract T value { get; set; }
        public static implicit operator T(Variable<T> variable) => variable.value; 

        public override string ToString() => value.ToString();
        public override float ToFloat() => 0f;
    }

    public abstract class Variable : ScriptableObject
    {
        public new abstract string ToString();
        public abstract float ToFloat();
    }

    public enum Comparison
    {
        EqualTo,
        LessThan,
        GreaterThan,
        LessThanOrEqualTo,
        GreaterThanOrEqualTo
    }
}