using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Variable", menuName = "Variable/Vector2", order = 0)]
public class Vector2Variable : Variable
{
    public Vector2 value = Vector2.zero;

    public Vector3 ToVector3(float z = 0) => new Vector3(value.x, value.y, z);

    public override string ToString() => $"{value.x}, {value.y}";
}
