using UnityEngine;
using UnityEditor;
using ArchitectureLibrary;

namespace ArchitectureLibrary
{
    [CustomPropertyDrawer(typeof(VariableReference<float>))]
    public class FloatReferenceDrawer : VariableReferenceDrawer
    {
        protected override void ConstantField(Rect position, SerializedProperty property)
        {
            property.floatValue = EditorGUI.FloatField(
                position,
                property.floatValue
            );
        }
        protected override void VariableField(Rect position, SerializedProperty property)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(
                position,
                property.objectReferenceValue,
                typeof(Float),
                false
            );
        }
        protected override void AssignVariable(SerializedProperty property) => CreateVariable<FloatVariable>(property);
    }
}