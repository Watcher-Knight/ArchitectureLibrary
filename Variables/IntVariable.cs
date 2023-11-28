using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/Int", order = 0)]
public class IntVariable : Variable
{
    public int value = 0;

    public override string ToString() => $"{value}";
}