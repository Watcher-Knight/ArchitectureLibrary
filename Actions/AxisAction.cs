using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class AxisAction : Action
    {
        public override abstract bool isActive { get; }
        public abstract void Invoke(float value);
    }
}