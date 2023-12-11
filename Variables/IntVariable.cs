using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variable/Int", order = 0)]
    public class IntVariable : NumberVariable<int>
    {
        [SerializeField] private int _value = 0;
        public override int value { get => _value; set => _value = value; }

        public override string ToString() => $"{value}";
    }
}