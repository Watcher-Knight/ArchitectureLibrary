using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

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

    #region AddConditionMenu

    [ContextMenu("Add Condition/Generic Condition")] void AddGenericCondition() { conditions.Add(new Condition()); }
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

#region Conditions

[Serializable]
public class Condition
{
    public virtual bool value => true;
    [SerializeField] private bool negative = false;
    public bool GetValue() => (negative) ? !value : value;
}

[Serializable]
public class ManagerCondition : Condition
{
    [SerializeField] private EventManager eventManager;

    public override bool value => eventManager.CheckConditions();
}

#region Variables

[Serializable]
public abstract class VariableCondition : Condition
{
    [SerializeField]
    [Tooltip("Use scriptable object variable to compare value to instead of regular variable type.")]
    protected bool useObject = false;
}

[Serializable]
public class BoolCondition : VariableCondition
{
    [SerializeField] private BoolVariable boolVariable;
    [SerializeField] private BoolVariable objectComparison;

    public override bool value
    {
        get
        {
            if (useObject) return boolVariable.value == objectComparison.value;
            return boolVariable.value;
        }
    }
}

#region Numbers

[Serializable]
public abstract class NumberCondition : VariableCondition
{
    [SerializeField] protected Comparison comparisonType = Comparison.EqualTo;
}

[Serializable]
public class IntCondition : NumberCondition
{
    [SerializeField] private IntVariable intVariable;
    [SerializeField] private IntVariable objectComparison;
    [SerializeField] private int regularComparison = 0;

    public override bool value
    {
        get
        {
            if (useObject) return Compare(intVariable.value, objectComparison.value, comparisonType);
            return Compare(intVariable.value, regularComparison, comparisonType);
        }
    }

    private bool Compare(int value1, int value2, Comparison comparisonType = Comparison.EqualTo)
    {
        switch (comparisonType)
        {
            case Comparison.EqualTo: default: return value1 == value2;
            case Comparison.LessThan: return value1 < value2;
            case Comparison.GreaterThan: return value1 > value2;
            case Comparison.LessThanOrEqualTo: return value1 <= value2;
            case Comparison.GreaterThanOrEqualTo: return value1 >= value2;
        }
    }
}

[Serializable]
public class FloatCondition : NumberCondition
{
    [SerializeField] private FloatVariable floatVariable;
    [SerializeField] private FloatVariable objectComparison;
    [SerializeField] private float regularComparison = 0;

    public override bool value
    {
        get
        {
            if (useObject) return Compare(floatVariable.value, objectComparison.value, comparisonType);
            return Compare(floatVariable.value, regularComparison, comparisonType);
        }
    }

    private bool Compare(float value1, float value2, Comparison comparisonType = Comparison.EqualTo)
    {
        switch (comparisonType)
        {
            case Comparison.EqualTo: default: return value1 == value2;
            case Comparison.LessThan: return value1 < value2;
            case Comparison.GreaterThan: return value1 > value2;
            case Comparison.LessThanOrEqualTo: return value1 <= value2;
            case Comparison.GreaterThanOrEqualTo: return value1 >= value2;
        }
    }
}

#endregion

#endregion

[Serializable]
public class ButtonCondition : Condition
{
    // [SerializeReference] private string name;
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

    // private void OnValidate()
    // {
    //     name = buttonEventManager.name;
    // }
}

[Serializable]
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

[Serializable]
public class ActionCondition : Condition
{
    [SerializeField] private Action action;
    public override bool value { get => action.isActive; }
}

#endregion