using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class DebugAction : Action
    {
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