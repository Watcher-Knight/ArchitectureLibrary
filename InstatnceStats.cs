using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.instanceStats)]
    public class InstanceStats : MonoBehaviour
    {
        [SerializeField] private List<string> keys = new List<string>();
        [SerializeField] private List<float> values = new List<float>();
        [SerializeField] private List<Variable> references = new List<Variable>();

        public float GetVariable(string name) => values[keys.IndexOf(name)];
        public Variable GetReference(string name) => references[keys.IndexOf(name)];
        public void SetVariable(string name, float value) => values[keys.IndexOf(name)] = value;
        public void AddToVariable(string name, float value) => values[keys.IndexOf(name)] += value;
        public void SubtractFromVariable(string name, float value) => values[keys.IndexOf(name)] -= value;
        public void ResetVariable(string name) => values[keys.IndexOf(name)] = references[keys.IndexOf(name)]?.ToFloat() ?? 0;
        public void SetReference(string name, Variable value)
        {
            if (references[keys.IndexOf(name)] != value)
            {
                references[keys.IndexOf(name)] = value;
                values[keys.IndexOf(name)] = value?.ToFloat() ?? 0;
            }
        }
        public void AddVariable(string name)
        {
            if (!keys.Contains(name))
            {
                keys.Add(name);
                values.Add(0);
                references.Add(null);
            }
        }
        public void RemoveVariable(string name)
        {
            if (keys.Contains(name))
            {
                values.RemoveAt(keys.IndexOf(name));
                references.RemoveAt(keys.IndexOf(name));
                keys.Remove(name);
            }
        }
        public void ShiftVariable(string name, int amount)
        {
            int oldIndex = keys.IndexOf(name);
            int newIndex = oldIndex + amount;
            if (newIndex < 0) newIndex = 0;
            if (newIndex >= keys.Count) newIndex = keys.Count - 1;
            float value = values[oldIndex];
            Variable reference = references[oldIndex];

            keys.RemoveAt(oldIndex);
            values.RemoveAt(oldIndex);
            references.RemoveAt(oldIndex);
            keys.Insert(newIndex, name);
            values.Insert(newIndex, value);
            references.Insert(newIndex, reference);
        }

        private void Awake()
        {
            foreach (string key in keys)
            {
                ResetVariable(key);
            }
        }

        [CustomEditor(typeof(InstanceStats))]
        public class InstanceStatsEditor : Editor
        {
            private bool addVariable = false;
            private string newVariableName = "New Variable";
            public override void OnInspectorGUI()
            {
                InstanceStats stats = (InstanceStats)target;

                foreach (string key in stats.keys)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(key, GUILayout.Width(CustomInspector.GetRelativeWidth(30)));
                    EditorGUILayout.LabelField("|", GUILayout.Width(CustomInspector.GetRelativeWidth(2)));
                    EditorGUILayout.LabelField(stats.GetVariable(key).ToString(), CustomInspector.boldLabelStyle, GUILayout.Width(CustomInspector.GetRelativeWidth(15)));
                    stats.SetReference(key, (Variable)EditorGUILayout.ObjectField(stats.GetReference(key), typeof(Variable), false, GUILayout.Width(CustomInspector.GetRelativeWidth(45))));
                    if (GUILayout.Button("-", GUILayout.Width(CustomInspector.GetRelativeWidth(5)))) { stats.RemoveVariable(key); return; }

                    EditorGUILayout.EndHorizontal();

                    if (!EditorApplication.isPlaying) stats.ResetVariable(key);
                }

                if (addVariable) newVariableName = EditorGUILayout.TextField(newVariableName);
                if (!addVariable)
                {
                    if (GUILayout.Button("Add Variable")) addVariable = true;
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add")) { stats.AddVariable(newVariableName); addVariable = false; }
                    if (GUILayout.Button("Cancel")) addVariable = false;
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }
}