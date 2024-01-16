using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.buttonEventManager)]
    public class ButtonEventManager : EventManager
    {
        [SerializeField]
        private InputAction button =
            new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        [SerializeField] private ActionList effect;
        public bool value => button.ReadValue<float>() > 0;
        public bool pressed => button.triggered;

        private void OnEnable()
        {
            button.Enable();

            button.performed += _ => { if (CheckConditions()) effect.Invoke(); };
            button.canceled += _ => { if (CheckConditions()) effect.Cancel(); };
        }

        private void OnDisable()
        {
            button.Disable();
        }
        public void Invoke() => effect.Invoke();
    }
}