using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class DebugAction : Action
    {
        [SerializeField] private StringVariable message;

        private void Start()
        {
            if (message == null) message = ScriptableObject.CreateInstance<StringVariable>();
        }

        private bool IsLogging = false;
        public override bool isActive => IsLogging;
        public void Invoke()
        {
            Debug.Log("Success!");
            IsLogging = true;
        }

        private void LateUpdate()
        {
            IsLogging = false;
        }
    }
}