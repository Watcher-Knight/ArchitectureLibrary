using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ArchitectureLibrary
{
    [Serializable]
    public class Condition
    {
        public virtual bool value => true;
        [SerializeField] private bool negative = false;
        public bool GetValue() => (negative) ? !value : value;

        public virtual void OnValidate() { }
    }

    public class ManagerCondition : Condition
    {
        [SerializeField] private string managerName;
        [SerializeField] private EventManager eventManager;

        public override bool value => (eventManager != null) ? eventManager.CheckConditions() : false;

        public override void OnValidate()
        {
            managerName = (eventManager != null) ? eventManager.name : "None";
        }
    }

    public class CustomCondition : Condition
    {
        public override bool value
        {
            get
            {
                return CodeConverter.Compare
                (
                    CodeConverter.GetValue(value1),
                    CodeConverter.GetValue(value2),
                    comparison
                );
            }
        }
        [SerializeField] private string value1 = "1";
        [SerializeField] private Comparison comparison = Comparison.EqualTo;
        [SerializeField] private string value2 = "1";
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
    public class IntCondition : NumberCondition<IntVariable, int> {}
    public class FloatCondition : NumberCondition<FloatVariable, float> {}

    // [Serializable]
    // public class IntCondition : NumberCondition<IntVariable, int>
    // {
    //     [SerializeField] private IntVariable _variable;
    //     public override IntVariable variable => _variable;
    //     [SerializeField] private IntVariable _objectComparison;
    //     public override IntVariable objectComparison => _objectComparison;
    //     [SerializeField] private int _regularComparison = 0;
    //     public override int regularComparison => _regularComparison;
    // }

    // [Serializable]
    // public class FloatCondition : NumberCondition<FloatVariable, float>
    // {
    //     [SerializeField] private FloatVariable _variable;
    //     public override FloatVariable variable => _variable;
    //     [SerializeField] private FloatVariable _objectComparison;
    //     public override FloatVariable objectComparison => _objectComparison;
    //     [SerializeField] private float _regularComparison = 0;
    //     public override float regularComparison => _regularComparison;
    // }

    #endregion

    public class ButtonCondition : Condition
    {
        [SerializeField] private string managerName;
        [SerializeField] private ButtonEventManager buttonEventManager;
        [SerializeField] private bool OnlyActivateOnPress = false;

        public override bool value
        {
            get
            {
                if (OnlyActivateOnPress) return buttonEventManager.pressed;
                return buttonEventManager.value;
            }
        }

        public override void OnValidate()
        {
            managerName = (buttonEventManager != null) ? buttonEventManager.name : "None";
        }
    }

    public class TriggerCondition : Condition
    {
        [SerializeField] private TriggerEventManager triggerEventManager;

        [SerializeField] private TriggerEventType eventType = TriggerEventType.Continuous;
        [SerializeField] private List<Tag> triggerTags = new List<Tag>();

        public override bool value
        {
            get
            {
                foreach (Tag triggerTag in triggerTags)
                {
                    if (triggerEventManager.eventConditions[eventType].Contains(triggerTag)) return true;
                }
                return false;
            }
        }
    }

    public class ActionCondition : Condition
    {
        [SerializeField] private Action action;
        public override bool value { get => action.isActive; }
    }
}