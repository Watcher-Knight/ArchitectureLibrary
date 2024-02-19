using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace ArchitectureLibrary
{
    [CustomEditor(typeof(Object), true)]
    public class ObjectEditor : CustomInspector
    {
        public bool UpdateEveryFrame = false;
        public override bool RequiresConstantRepaint() => UpdateEveryFrame;
        public override void OnInspectorGUI()
        {
            if (target.GetType().GetCustomAttribute<UpdateEditorAttribute>() != null)
                UpdateEveryFrame = EditorGUILayout.Toggle("Update Every Frame", UpdateEveryFrame);

            base.OnInspectorGUI();

            GetDisplayProperties().ForEach(p => EditorGUILayout.LabelField(p.Name.ToTitleCase(), p.GetValue(target).ToString()));
        }
    }
}