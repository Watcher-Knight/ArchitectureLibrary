using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class VariableReference { }

    [Serializable]
    public class VariableReference<T> : VariableReference
    {
        [SerializeField] protected bool useConstant = false;
        [SerializeField] protected T constant;
        [SerializeField] protected Variable<T> variable;
        public T value => (useConstant || variable == null) ? constant : variable.value;
        public static implicit operator T(VariableReference<T> reference) => reference.value;
        public static implicit operator VariableReference<T>(T value) => new(value);
        public VariableReference(T value) => constant = value;
        public VariableReference() { }
    }
}