using UnityEngine;

[CreateAssetMenu(fileName = "Vector3Variable", menuName = "Variable/Vector3", order = 0)]
public class Vector3Variable : Variable
{
    public Vector3 value = new Vector3(0, 0, 0);

    public Vector2 ToVector2() => new Vector2(value.x, value.y);

    public override string ToString() => $"{value.x}, {value.y}, {value.z}";
}