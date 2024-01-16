using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Integer : NumberVariable<int> { }

    [CreateAssetMenu(fileName = "IntVariable", menuName = CreateAssetPaths.variables + IntVariable.title, order = 0)]
    public class IntVariable : Integer
    {
        [SerializeField] private int _value = 0;
        public override int value { get => _value; set => _value = value; }

        public override string ToString() => $"{value}";
        public override float ToFloat() => (float)value;

        public const string title = "Integer";
    }
}