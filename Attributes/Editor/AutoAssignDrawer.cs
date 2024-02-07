using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Text.RegularExpressions;


namespace ArchitectureLibrary
{
    [CustomPropertyDrawer(typeof(AutoAssignAttribute))]
    public class AutoAssignDrawer : PropertyDrawer
    {
        public const float buttonWidth = 20;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type type = Properties.GetPropertyType(property);
            MethodInfo GUIMethod;

            if (typeof(Component).IsAssignableFrom(type))
            {
                GUIMethod = GetType().GetMethod(nameof(ComponentGUI));
                GUIMethod = GUIMethod.MakeGenericMethod(type);
                GUIMethod.Invoke(null, new object[3] { position, property, label });
            }

            if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                GUIMethod = GetType().GetMethod(nameof(ScriptableObjectGUI));
                GUIMethod = GUIMethod.MakeGenericMethod(type);
                GUIMethod.Invoke(null, new object[3] { position, property, label });
            }
        }

        public static void ComponentGUI<T>(Rect position, SerializedProperty property, GUIContent label) where T : Component
        {
            SerializedObject parent = property.serializedObject;
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            if (property.objectReferenceValue == null && parent != null && typeof(Component).IsAssignableFrom(parent.targetObject.GetType()))
            {
                GameObject gameObject = ((Component)parent.targetObject).gameObject;

                if (gameObject.TryGetComponent(out T component))
                {
                    property.objectReferenceValue = component;
                }
                else
                {
                    Rect fieldPosition = new(position);
                    fieldPosition.width -= buttonWidth + 2;

                    Rect buttonPosition = new(position);
                    buttonPosition.x += fieldPosition.width + 2;
                    buttonPosition.width = buttonWidth;

                    property.objectReferenceValue =
                        EditorGUI.ObjectField(fieldPosition, property.objectReferenceValue, typeof(T), true);
                    if (GUI.Button(buttonPosition, "+")) property.objectReferenceValue = gameObject.AddComponent<T>();
                }
            }
            else
            {
                property.objectReferenceValue =
                    EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(T), true);
            }

            EditorGUI.EndProperty();
        }

        public static void ScriptableObjectGUI<T>(Rect position, SerializedProperty property, GUIContent label) where T : ScriptableObject
        {
            SerializedObject parent = property.serializedObject;
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            if (property.objectReferenceValue == null)
            {
                Rect fieldPosition = new(position);
                fieldPosition.width -= buttonWidth + 2;

                Rect buttonPosition = new(position);
                buttonPosition.x += fieldPosition.width + 2;
                buttonPosition.width = buttonWidth;

                property.objectReferenceValue =
                    EditorGUI.ObjectField(fieldPosition, property.objectReferenceValue, typeof(T), true);
                    
                if (GUI.Button(buttonPosition, "+"))
                {
                    string directory = AssetPaths.scriptableObjects;
                    string name = StringFormatter.CapitalizeFirst(property.name);
                    name = Regex.Replace(name, "Data$", "");
                    if (parent != null && typeof(Component).IsAssignableFrom(parent.targetObject.GetType()))
                    {
                        directory += "/" + ((Component) parent.targetObject).name;
                    }

                    property.objectReferenceValue = ScriptableObjectFactory.Create<T>(directory, name);
                }
            }
            else
            {
                property.objectReferenceValue =
                    EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(T), false);
            }

            EditorGUI.EndProperty();
        }
    }
}