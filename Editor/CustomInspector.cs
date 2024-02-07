using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public class CustomInspector : Editor
    {
        public T GetField<T>(params string[] path)
        {
            object targetObject = target;
            foreach (string f in path)
            {
                FieldInfo field = targetObject.GetType().GetField(f, Properties.bindingFlags);
                targetObject = field.GetValue(targetObject);
            }
            return Properties.Convert<T>(targetObject);
        }

        public void SetField(object value, params string[] path)
        {
            object targetObject = target;
            FieldInfo field = targetObject.GetType().GetField(path[0], Properties.bindingFlags);
            for (int i = 1; i > path.Length - 1; i++)
            {
                targetObject = field.GetValue(targetObject);
                field = targetObject.GetType().GetField(path[i], Properties.bindingFlags);
            }
            if (field.FieldType.IsAssignableFrom(targetObject.GetType()))
            { field.SetValue(targetObject, value); return; }
            throw new InvalidCastException($"Cannot convert from type \"{targetObject.GetType()}\" to type \"{field.FieldType}\"");
        }

        private static GUIStyle b_TitleStyle;
        public static GUIStyle TitleStyle
        {
            get
            {
                b_TitleStyle ??= new GUIStyle(GUI.skin.label)
                {
                    fontSize = 20,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter
                };
                return b_TitleStyle;
            }
        }

        private static GUIStyle b_BoldLabelStyle;
        public static GUIStyle BoldLabelStyle
        {
            get
            {
                b_BoldLabelStyle ??= new GUIStyle(GUI.skin.label)
                {
                    fontStyle = FontStyle.Bold
                };
                return b_BoldLabelStyle;
            }
        }

        public static void Title(string title, string subTitle = null)
        {
            GUILayout.Space(10f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label(title, TitleStyle);

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