using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public abstract class BaseEditor : Editor
    {
        private static GUIStyle _titleStyle;
        public static GUIStyle titleStyle
        {
            get
            {
                if (_titleStyle == null)
                {
                    _titleStyle = new GUIStyle(GUI.skin.label);
                    _titleStyle.fontSize = 15;
                    _titleStyle.fontStyle = FontStyle.Bold;
                    _titleStyle.alignment = TextAnchor.MiddleCenter;
                }
                return _titleStyle;
            }
        }

        private TestAttribute testAttribute;

        private void OnEnable()
        {
            if (testAttribute == null) testAttribute = GetTestAttribute(target);
        }

        public override void OnInspectorGUI()
        {
            HeaderGUI(testAttribute);

            base.OnInspectorGUI();
        }

        public static TestAttribute GetTestAttribute(UnityEngine.Object obj)
        {
            return Attributes.GetCustomAttribute<TestAttribute>(obj.GetType()) ?? new TestAttribute(obj.GetType().ToString());
        }

        public static void HeaderGUI(TestAttribute testAttribute)
        {
            if (testAttribute != null)
            {
                GUILayout.Space(10f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label(testAttribute.name, titleStyle);

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Box(testAttribute.description, GUILayout.Width(Screen.width * 0.8f));

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
            }
        }
    }

    //[CustomEditor(typeof(MonoBehaviour), true)]
    public class ComponentEditor : BaseEditor { }

    public static class Attributes
    {
        public static T GetCustomAttribute<T>(this Type type) where T : Attribute
        {
            object[] attributes = type.GetCustomAttributes(false);
            return attributes.OfType<T>().FirstOrDefault();
        }
    }
}