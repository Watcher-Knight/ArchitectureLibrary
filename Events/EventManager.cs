using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Event Managers/Event Manager")]
    public class EventManager : MonoBehaviour
    {
#pragma warning disable 0414
        [SerializeField] private string _name = "EventManager";
        public new string name => _name;
#pragma warning restore 0414

        [SerializeField] protected bool debugMode = false;
        [SerializeReference] protected List<Condition> conditions = new List<Condition>();

        public bool CheckConditions()
        {
            if (conditions.Count > 0)
            {
                foreach (Condition condition in conditions)
                {
                    if (!condition.GetValue()) return false;
                }
                return true;
            }
            return true;
        }

        protected virtual void OnValidate() { foreach (Condition condition in conditions) if (condition != null) condition.OnValidate(); }

        #region AddConditionMenu

        [ContextMenu("Add Condition/Generic Condition")] void AddGenericCondition() { conditions.Add(new Condition()); }
        [ContextMenu("Add Condition/Custom Condition")] void AddCustomCondition() { conditions.Add(new CustomCondition()); }
        [ContextMenu("Add Condition/Manager Condition")] void AddManagerCondition() { conditions.Add(new ManagerCondition()); }
        [ContextMenu("Add Condition/Variables/Bool Condition")] void AddBoolCondition() { conditions.Add(new BoolCondition()); }
        [ContextMenu("Add Condition/Variables/Numbers/Int Condition")] void AddIntCondition() { conditions.Add(new IntCondition()); }
        [ContextMenu("Add Condition/Variables/Numbers/Float Condition")] void AddFloatCondition() { conditions.Add(new FloatCondition()); }
        [ContextMenu("Add Condition/Button Condition")] void AddButtonCondition() { conditions.Add(new ButtonCondition()); }
        [ContextMenu(("Add Condition/Trigger Condition"))] void AddTriggerCondition() { conditions.Add(new TriggerCondition()); }
        [ContextMenu(("Add Condition/Action Condition"))] void AddActionCondition() { conditions.Add(new ActionCondition()); }

        // [ContextMenu(nameof(RemoveCondition))]
        // void RemoveCondition() { if (conditions.Count > 0) conditions.RemoveAt(conditions.Count - 1); }

        #endregion

        #region Debug

        [ContextMenu("Debug/Log Condition Value")] void LogConditionsValue() { Debug.Log(CheckConditions()); }

        #endregion
    }
}