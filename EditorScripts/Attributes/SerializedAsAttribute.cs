using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public class SerializedAsAttribute : PropertyAttribute
    {
        public string label;
        public SerializedAsAttribute(string label) => this.label = label;
    }

    [CustomPropertyDrawer(typeof(SerializedAsAttribute))]
    public class SerializedAsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedAsAttribute serializedAs = attribute as SerializedAsAttribute;

            EditorGUI.PropertyField(position, property, new GUIContent(serializedAs.label));
        }
    }
}