using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace ArchitectureLibrary
{
    [CustomEditor(typeof(ScriptableObject), true)]
    public class ScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            string title =
                (string)target.GetType().GetField("title", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ??
                target.GetType().Name;

            BaseEditorHeader(target);

            base.OnInspectorGUI();
        }
        public static void BaseEditorHeader(object target)
        {
            string type =
                (string)target.GetType().GetField("title", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) ??
                target.GetType().Name;
            string title = StringFormatter.ToTitleCase(((ScriptableObject)target).name);

            CustomInspector.Title(title, type);
        }
    }
}