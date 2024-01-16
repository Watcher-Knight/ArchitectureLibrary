using UnityEngine;
using UnityEditor;
using ArchitectureLibrary;

namespace ArchitectureLibrary
{
    [CustomPropertyDrawer(typeof(VariableReference<int>))]
    public class IntReferenceDrawer : VariableReferenceDrawer
    {
        protected override void ConstantField(Rect position, SerializedProperty property)
        {
            property.intValue = EditorGUI.IntField(
                position,
                property.intValue
            );
        }
        protected override void VariableField(Rect position, SerializedProperty property)
        {
            property.objectReferenceValue = EditorGUI.ObjectField(
                position,
                property.objectReferenceValue,
                typeof(Integer),
                false
            );
        }
        protected override void AssignVariable(SerializedProperty property) => CreateVariable<IntVariable>(property);
    }
}