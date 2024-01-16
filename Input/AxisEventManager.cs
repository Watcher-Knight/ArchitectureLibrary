using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.axisEventManager)]
    public class AxisEventManager : EventManager
    {
        [SerializeField] private InputAction axis = new InputAction(expectedControlType: "Axis");
        [SerializeField][RestrictTo(typeof(IAxisControllable))] private Object _action;
        private IAxisControllable action => (_action != null) ? (IAxisControllable)_action : null;

        protected void OnValidate()
        {
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
            if (CheckConditions() && action != null) action.Control(axis.ReadValue<float>());
        }
    }
}