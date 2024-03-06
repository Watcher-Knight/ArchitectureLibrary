using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary.Editor
{
    [CustomEditor(typeof(Object), true)]
    public class ObjectEditor : CustomInspector
    {
        public bool UpdateEveryFrame = false;
        public override bool RequiresConstantRepaint() => UpdateEveryFrame;
        public override void OnInspectorGUI()
        {
            if (target.GetType().HasAttribute<UpdateEditorAttribute>())
                UpdateEveryFrame = EditorGUILayout.Toggle("Update Every Frame", UpdateEveryFrame);

            base.OnInspectorGUI();

            GetDisplayProperties().ForEach(p => EditorGUILayout.LabelField(p.Name.ToTitleCase(), p.GetValue(target).ToString()));
            ShowButtons();
        }
    }
}