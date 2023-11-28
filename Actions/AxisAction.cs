using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AxisAction : Action
{
    public override abstract bool isActive { get; }
    public abstract void Invoke(float value);
}
