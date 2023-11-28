using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "Variable/Bool", order = 0)]
public class BoolVariable : Variable
{
    public bool value = false;

    public override string ToString() => $"{value}";
}