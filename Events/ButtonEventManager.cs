using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Event Managers/Button Event Manager")]
    public class ButtonEventManager : EventManager
    {
        [SerializeField]
        private InputAction button =
            new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        [SerializeField] private UnityEvent effect;
        public bool value => button.ReadValue<float>() > 0;
        public bool pressed => button.triggered;

        private void OnEnable()
        {
            button.Enable();

            button.performed += _ => { if (CheckConditions()) effect.Invoke(); };
        }

        private void OnDisable()
        {
            button.Disable();
        }

        protected override void OnValidate() => base.OnValidate();
    }
}