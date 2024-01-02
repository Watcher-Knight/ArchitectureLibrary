using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public class SerializeIfAttribute : PropertyAttribute
    {
        public string condition;
        public bool expectedValue;
        public SerializeIfAttribute(string condition, bool expectedValue = true)
        {
            this.condition = condition;
            this.expectedValue = expectedValue;
        }
    }

    [CustomPropertyDrawer(typeof(SerializeIfAttribute))]
    public class SerializeIfDrawer : PropertyDrawer
    {
        private float propertyHeight = 0f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => propertyHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializeIfAttribute serializeIf = attribute as SerializeIfAttribute;

            Object target = property.serializedObject.targetObject;
            FieldInfo conditionField = target.GetType().GetField(serializeIf.condition, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo conditionProperty = target.GetType().GetProperty(serializeIf.condition, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            bool condition = (bool)(conditionField?.GetValue(target) ?? conditionProperty.GetValue(target) ?? true);

            condition = serializeIf.expectedValue ? condition : !condition;

            if (condition)
            {
                propertyHeight = base.GetPropertyHeight(property, label);
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                propertyHeight = 0f;
            }
        }
    }
}