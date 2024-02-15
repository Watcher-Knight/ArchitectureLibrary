using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour))]
public class MonoBehaviorEditor : ObjectEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}