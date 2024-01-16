using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace ArchitectureLibrary
{
    [Serializable]
    public class ActionList : IInvokeable
    {
        [SerializeField] private List<Object> items = new();
        public void Invoke() { foreach (Object item in items) ((IInvokeable)item).Invoke(); }
        public void Cancel() { foreach (Object item in items) ((IInvokeable)item).Cancel(); }
        

        [CustomPropertyDrawer(typeof(ActionList))]
        public class ActionDrawer : ObjectListDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                BaseGUI(position, property, label, typeof(IInvokeable));
            }
        }
    }
}