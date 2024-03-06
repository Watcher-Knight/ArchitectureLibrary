using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ArchitectureLibrary
{
    public class LabelAttribute : PropertyAttribute
    {
        public string Text;
        public LabelAttribute(string text) => Text = text;
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelDrawer : PropertyDrawer
    {
        private float propertyHeight = 0f;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => propertyHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LabelAttribute newLabel = attribute as LabelAttribute;

            PropertyDrawer drawer = null; //PropertyDrawerFinder.Find(property);

            if (drawer != null)
            {
                drawer.OnGUI(position, property, new GUIContent(newLabel.Text));
                propertyHeight = drawer.GetPropertyHeight(property, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, new GUIContent(newLabel.Text));
                propertyHeight = base.GetPropertyHeight(property, label);
            }
        }
    }
#endif
}