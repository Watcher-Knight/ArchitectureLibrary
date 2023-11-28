using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxisEventManager : EventManager
{
    [SerializeField] private InputAction axis = new InputAction(expectedControlType: "Axis");
    [SerializeField] private AxisAction action;

    private void OnValidate()
    {
        if (axis.bindings.Count == 0)
            axis.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/rightArrow").With("Negative", "<Keyboard>/LeftArrow");
    }

    private void OnEnable()
    {
        axis.Enable();
    }

    private void OnDisable()
    {
        axis.Disable();
    }

    private void Update()
    {
        action.Invoke(axis.ReadValue<float>());
    }
}
