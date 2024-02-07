using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [CustomPropertyDrawer(typeof(EventTag))]
    public class EventTagDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2f;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EventTag oldTag = property.GetObject<EventTag>();
            EventTag[] tags = EventTagEditor.GetTags();
            string[] tagOptions = tags.Select(tag => tag.Name).ToArray();

            EventTag newTag = tags.Contains(oldTag) ? oldTag : tags[0];
            
            int index = Array.IndexOf(tags, newTag);
            index = EditorGUILayout.Popup(label, index, tagOptions);

            newTag = tags[index];

            property.SetValue(newTag);

            if (oldTag != newTag) EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }
}