using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Variable/Bool", order = 0)]
    public class BoolVariable : Variable<bool>
    {
        [SerializeField] private bool _value = false;
        public override bool value { get => _value; set => _value = value; }

        public override string ToString() => $"{value}";
    }
}