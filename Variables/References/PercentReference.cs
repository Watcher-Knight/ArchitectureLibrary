using System;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [Serializable]
    public class PercentReference : VariableReference<float> { }

    [CustomPropertyDrawer(typeof(PercentReference))]
    public class PercentReferenceDrawer : VariableReferenceDrawer
    {
        protected override void ConstantField(Rect position, SerializedProperty property)
        {
            property.floatValue = EditorGUI.Slider(
                position,
                property.floatValue,
                0, 1
            );
        }
        protected override void VariableField(Rect position, SerializedProperty property)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(
                position,
                property.objectReferenceValue,
                typeof(PercentVariable),
                false
            );
        }
        protected override void AssignVariable(SerializedProperty property) => CreateVariable<PercentVariable>(property);
    }
}