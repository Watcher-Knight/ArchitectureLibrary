using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;
using UnityEngine.InputSystem;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.buttonEventManager)]
    public class ButtonEventManager : EventManager
    {
        [SerializeField]
        private InputAction button =
            new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        [SerializeField] private UltEvent effect;
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
        public void Invoke() => effect.Invoke();

        protected override void OnValidate() => base.OnValidate();
    }
}