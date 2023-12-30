using System.Reflection;
using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "RandomFloat", menuName = RandomFloat.title, order = 0)]
    public class RandomFloat : NumberVariable<float>
    {
        [SerializeField] private float min = 0;
        [SerializeField] private float max = 1;

        public override float value { get => Random.Range(min, max); set { } }

        public const string title = "RandomFloat";
    }
}