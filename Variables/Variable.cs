using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Variable<T> : Variable
    {
        public abstract T value { get; set; }

        public override string ToString()
        {
            string errorMessage = "Cannot convert to string";
            Debug.LogError(errorMessage);
            return errorMessage;
        }
    }

    public abstract class Variable : ScriptableObject
    {
        public new abstract string ToString();
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