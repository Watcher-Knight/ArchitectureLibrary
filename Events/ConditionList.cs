using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.conditionList)]
    public class ConditionList : MonoBehaviour, ICondition
    {
        [SerializeReference] private List<Condition> list = new List<Condition>();

        private void AddElement<T>() where T : Condition, new() => list.Add(new T());

        public bool conditionValue
        {
            get
            {
                foreach (Condition condition in list)
                {
                    if (!condition.value) return false;
                }
                return true;
            }
        }

        [ContextMenu("Log Condition Values")]
        private void LogValues() => Debug.Log(conditionValue);




        [CustomEditor(typeof(ConditionList))]
        public class ConditionListEditor : Editor
        {
            bool addCondition = false;
            int conditionIndex = 0;

            public override void OnInspectorGUI()
            {
                ConditionList conditionList = (ConditionList)target;

                base.OnInspectorGUI();

                if (addCondition)
                {
                    Dictionary<string, Type> typeSelection = new Dictionary<string, Type>();
                    Assembly assembly = Assembly.GetAssembly(typeof(ConditionListEditor));
                    List<Type> types = new List<Type>();
                    foreach (Type type in assembly.GetTypes()) if (typeof(Condition).IsAssignableFrom(type)) types.Add(type);
                    foreach (Type type in types)
                    {
                        AddListItemAttribute attribute = (AddListItemAttribute)type.GetCustomAttribute(typeof(AddListItemAttribute));
                        if (attribute == null) continue;
                        typeSelection.Add(attribute.name, type);
                    }

                    List<string> names = new List<string>(typeSelection.Keys);

                    conditionIndex = EditorGUILayout.Popup(conditionIndex, names.ToArray());
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add"))
                    {
                        typeof(ConditionList).GetMethod("AddElement", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(typeSelection[names[conditionIndex]]).Invoke(conditionList, null);
                        addCondition = false;
                    }
                    if (GUILayout.Button("Cancel")) addCondition = false;
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    if (GUILayout.Button("Add Condition")) addCondition = true;
                }
            }
        }
    }
}