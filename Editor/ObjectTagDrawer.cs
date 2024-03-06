using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary.Editor
{
    [CustomPropertyDrawer(typeof(ObjectTag))]
    public class ObjectTagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty nameField = property.FindPropertyRelative("Name");
            nameField.stringValue = EditorGUI.TagField(position, label, nameField.stringValue);
        }
    }
}