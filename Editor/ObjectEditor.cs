using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Object))]
public class ObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();        
    }
}