using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class NumberVariable<T> : Variable<T> where T : IComparable<T>
    {
        public override abstract T value { get; set; }
    }
}