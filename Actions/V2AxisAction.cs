using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class V2AxisAction : Action
    {
        public override abstract bool isActive { get; }
        public abstract void Invoke(Vector2 value);
    }
}