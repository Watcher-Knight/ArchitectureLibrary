using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviorEditor : ObjectEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}