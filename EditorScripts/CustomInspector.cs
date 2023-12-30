using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public static class CustomInspector
    {
        private static GUIStyle _titleStyle;
        public static GUIStyle titleStyle
        {
            get
            {
                if (_titleStyle == null)
                {
                    _titleStyle = new GUIStyle(GUI.skin.label);
                    _titleStyle.fontSize = 20;
                    _titleStyle.fontStyle = FontStyle.Bold;
                    _titleStyle.alignment = TextAnchor.MiddleCenter;
                }
                return _titleStyle;
            }
        }

        private static GUIStyle _boldLabelStyle;
        public static GUIStyle boldLabelStyle
        {
            get
            {
                if (_boldLabelStyle == null)
                {
                    _boldLabelStyle = new GUIStyle(GUI.skin.label);
                    _boldLabelStyle.fontStyle = FontStyle.Bold;
                }
                return _boldLabelStyle;
            }
        }

        public static void Title(string title, string subTitle = null)
        {
            GUILayout.Space(10f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label(title, titleStyle);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            if (subTitle != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUILayout.Label(subTitle);

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(20f);
        }

        public static float GetRelativeWidth(float percentage)
        {
            return (Screen.width - 26) * percentage * .01f;
        }
    }
}