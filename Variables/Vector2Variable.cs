using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "Vector2Variable", menuName = CreateAssetPaths.variables + Vector2Variable.title, order = 0)]
    public class Vector2Variable : Variable<Vector2>
    {
        [SerializeField] private Vector2 _value = Vector2.zero;
        public override Vector2 value { get => _value; set => _value = value; }

        public Vector3 ToVector3(float z = 0) => new Vector3(value.x, value.y, z);

        public override string ToString() => $"{value.x}, {value.y}";
        public override float ToFloat() => value.x;

        public const string title = "Vector 2";
    }
}