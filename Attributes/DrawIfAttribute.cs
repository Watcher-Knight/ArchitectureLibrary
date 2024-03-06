using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ArchitectureLibrary
{
    public class DrawIfAttribute : PropertyAttribute
    {
        public string Condition;
        public bool ExpectedValue;
        public DrawIfAttribute(string condition, bool expectedValue = true)
        {
            Condition = condition;
            ExpectedValue = expectedValue;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DrawIfAttribute))]
    public class DrawIfDrawer : PropertyDrawer
    {
        private float propertyHeight = 0f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => propertyHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawIfAttribute serializeIf = attribute as DrawIfAttribute;

            Object target = property.serializedObject.targetObject;
            FieldInfo conditionField = target.GetType().GetField(serializeIf.Condition, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo conditionProperty = target.GetType().GetProperty(serializeIf.Condition, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            bool condition = (bool)(conditionField?.GetValue(target) ?? conditionProperty.GetValue(target) ?? true);

            condition = serializeIf.ExpectedValue ? condition : !condition;

            if (condition)
            {
                PropertyDrawer drawer = null; //PropertyDrawerFinder.Find(property);

                if (drawer != null)
                {
                    propertyHeight = drawer.GetPropertyHeight(property, label);
                    drawer.OnGUI(position, property, label);
                }
                else
                {
                    propertyHeight = base.GetPropertyHeight(property, label);
                    EditorGUI.PropertyField(position, property, label);
                }
            }
            else
            {
                propertyHeight = 0f;
            }
        }
    }
    #endif
}