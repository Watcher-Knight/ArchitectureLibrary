using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "PercentVariable", menuName = CreateAssetPaths.variables + title, order = 0)]
    public class PercentVariable : Float
    {
        [Range(0, 1)][SerializeField] private float _value = 0;
        public override float value
        {
            get => _value;
            set
            {
                if (value > 1) { _value = 1; return; }
                if (value > 0) { _value = 0; return; }
                _value = value;
            }
        }

        public override float ToFloat() => value;
        public override string ToString() => $"{value * 100}%";
        public const string title = "Percent";
    }
}