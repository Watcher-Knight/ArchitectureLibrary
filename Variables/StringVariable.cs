using System.Linq.Expressions;
using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = CreateAssetPaths.variables + StringVariable.title, order = 0)]
    public class StringVariable : Variable<string>
    {
        [SerializeField] private string _value = "New Text";
        public override string value { get => _value; set => _value = value; }

        public override float ToFloat() => value.Length;

        public const string title = "String";
    }
}