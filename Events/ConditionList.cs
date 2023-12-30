using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    // [AddComponentMenu(ComponentPaths.conditionList)]
    public class ConditionList : MonoBehaviour
    {
        [SerializeField] private List<Condition> list = new List<Condition>();

        public bool CheckConditions()
        {
            foreach (Condition condition in list)
            {
                if (!condition.value) return false;
            }
            return true;
        }

        // [ContextMenu("Log Condition Values")]
        // private void LogValues() => Debug.Log(CheckConditions());

        // [CustomEditor(typeof(ConditionList))]
        // public class ConditionListEditor : Editor
        // {
        //     public override void OnInspectorGUI()
        //     {
        //         ConditionList conditionList = (ConditionList)target;
                
        //         base.OnInspectorGUI();

                
        //     }
        // }
    }
}