using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Event Managers/Axis Event Manager")]
    public class AxisEventManager : EventManager
    {
        [SerializeField] private InputAction axis = new InputAction(expectedControlType: "Axis");
        [SerializeField] private AxisAction action;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (axis.bindings.Count == 0)
                axis.AddCompositeBinding("1DAxis").With("Positive", "<Keyboard>/rightArrow").With("Negative", "<Keyboard>/leftArrow");
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
            if (CheckConditions() && action != null) action.Invoke(axis.ReadValue<float>());
        }
    }
}