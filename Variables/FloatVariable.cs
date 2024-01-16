using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Float : NumberVariable<float> { }

    [CreateAssetMenu(fileName = "FloatVariable", menuName = CreateAssetPaths.variables + title, order = 0)]
    public class FloatVariable : Float
    {
        [SerializeField] private float _value = 0f;
        public override float value { get => _value; set => _value = value; }

        public void Set(float number)
        {
            value = number;
        }

        public void Add(float number)
        {
            value += number;
        }

        public override float ToFloat() => value;

        public const string title = "Float";
    }
}