using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [CustomPropertyDrawer(typeof(Percentage))]
    public class PercentageDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            FieldInfo valueField = Properties.GetField(property);
            object parent = Properties.GetParentObject(property);
            float currentValue = (float) Properties.GetObject<Percentage>(property);
            Percentage newValue = EditorGUI.Slider(position, label, currentValue, 0f, 1f);
            valueField.SetValue(parent, newValue);

            if (currentValue != (float) newValue) EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }
}