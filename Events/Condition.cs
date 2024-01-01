using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public interface ICondition
    {
        bool conditionValue { get; }
    }

    [Serializable]
    public class Condition
    {
        public virtual bool value => true;
        [SerializeField] private bool negative = false;
        public bool GetValue() => (negative) ? !value : value;
    }

    [AddListItem("Manager Condition")]
    public class ManagerCondition : Condition
    {
        [SerializeField] private string managerName;
        [SerializeField] private EventManager eventManager;

        public override bool value => (eventManager != null) ? eventManager.CheckConditions() : false;
    }

    [AddListItem("Stats Condition")]
    public class StatsCondition : Condition
    {
        [SerializeField] private InstanceStats stats;
        [SerializeField] private string variableName;
        [SerializeField] private Comparison comparisonType;
        [SerializeField] private float comparison;

        public override bool value => CodeConverter.Compare<float>(stats.GetVariable(variableName), comparison, comparisonType);
    }

    #region Variables

    public abstract class VariableCondition<T> : Condition where T : Variable
    {
        [SerializeField]
        [Tooltip("Use scriptable object variable to compare value to instead of regular variable type.")]
        protected bool useObject = false;

        public abstract T variable { get; }
        public abstract T objectComparison { get; }
    }

    public class BoolCondition : VariableCondition<BoolVariable>
    {
        [SerializeField] private BoolVariable _variable;
        public override BoolVariable variable => _variable;
        [SerializeField] private BoolVariable _objectComparison;
        public override BoolVariable objectComparison => _objectComparison;

        public override bool value
        {
            get
            {
                if (variable != null)
                {
                    if (useObject) return (objectComparison != null) ? variable.value == objectComparison.value : false;
                    return variable.value;
                }
                return false;
            }
        }
    }

    public class NumberCondition<T, t> : VariableCondition<T> where T : NumberVariable<t> where t : IComparable<t>
    {
        [SerializeField] private T _variable;
        public override T variable => _variable;
        [SerializeField] private T _objectComparison;
        public override T objectComparison => _objectComparison;
        [SerializeField] private t regularComparison;
        [SerializeField] protected Comparison comparisonType = Comparison.EqualTo;

        public override bool value
        {
            get
            {
                if (variable != null)
                {
                    if (useObject) return (objectComparison != null) ?
                        CodeConverter.Compare<t>(variable.value, objectComparison.value, comparisonType) : false;
                    return CodeConverter.Compare<t>(variable.value, regularComparison, comparisonType);
                }
                return false;
            }
        }
    }
    [AddListItem("Int Condition")] public class IntCondition : NumberCondition<IntVariable, int> { }
    [AddListItem("Float Condition")] public class FloatCondition : NumberCondition<FloatVariable, float> { }

    #endregion

    // public class ButtonCondition : Condition
    // {
    //     [SerializeField] private string managerName;
    //     [SerializeField] private ButtonEventManager buttonEventManager;
    //     [SerializeField] private bool OnlyActivateOnPress = false;

    //     public override bool value
    //     {
    //         get
    //         {
    //             if (OnlyActivateOnPress) return buttonEventManager.pressed;
    //             return buttonEventManager.value;
    //         }
    //     }

    //     public override void OnValidate()
    //     {
    //         managerName = (buttonEventManager != null) ? buttonEventManager.name : "None";
    //     }
    // }

    // public class TriggerCondition : Condition
    // {
    //     [SerializeField] private TriggerEventManager triggerEventManager;

    //     [SerializeField] private TriggerEventType eventType = TriggerEventType.Continuous;
    //     [SerializeField] private List<Tag> triggerTags = new List<Tag>();

    //     public override bool value
    //     {
    //         get
    //         {
    //             foreach (Tag triggerTag in triggerTags)
    //             {
    //                 if (triggerEventManager.eventConditions[eventType].Contains(triggerTag)) return true;
    //             }
    //             return false;
    //         }
    //     }
    // }

    // public class CollisionCondition : Condition
    // {
    //     [SerializeField] private CollisionEventManager collisionEventManager;

    //     [SerializeField] private CollisionEventType eventType = CollisionEventType.Continuous;
    //     [SerializeField] private List<Tag> collisionTags = new List<Tag>();

    //     public override bool value
    //     {
    //         get
    //         {
    //             foreach (Tag collisionTag in collisionTags)
    //             {
    //                 if (collisionEventManager.eventConditions[eventType].Contains(collisionTag)) return true;
    //             }
    //             return false;
    //         }
    //     }
    // }

    [AddListItem("Action Condition")]
    public class ActionCondition : Condition
    {
        [SerializeField] private Action action;
        public override bool value { get => action.isActive; }
    }
}