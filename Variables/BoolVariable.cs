using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = CreateAssetPaths.variables + BoolVariable.title, order = 0)]
    public class BoolVariable : Variable<bool>
    {
        [SerializeField] private bool _value = false;
        public override bool value { get => _value; set => _value = value; }

        public override string ToString() => $"{value}";
        public override float ToFloat() => (value) ? 1 : 0;

        public const string title = "Bool";
    }
}