using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace ArchitectureLibrary
{
    [Serializable]
    public class ActionList
    {
        [SerializeField] private List<Object> list = new List<Object>();
        public void Invoke() { foreach (Object item in list) ((IInvokeable)item).Invoke(); }
        [SerializeField] private bool foldout;




        [CustomPropertyDrawer(typeof(ActionList))]
        public class ActionDrawer : PropertyDrawer
        {
            private bool foldout = false;
            private float propertyHeight = 0f;
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => propertyHeight;

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                SerializedProperty list = property.FindPropertyRelative("list");

                float itemHeight = base.GetPropertyHeight(property, label);
                float itemSpacing = itemHeight + 2;
                Rect currentPosition = new Rect(position.x, position.y, position.width, itemHeight);

                foldout = EditorGUI.Foldout(currentPosition, foldout, label, true);
                currentPosition.x += 20;
                currentPosition.width -= 20;

                propertyHeight = itemHeight;

                if (foldout)
                {
                    int index = 0;
                    IEnumerator enumerator = list.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        currentPosition.y += itemSpacing;
                        propertyHeight += itemSpacing;
                        Rect fieldPosition = currentPosition;
                        fieldPosition.width -= 22;
                        Rect buttonPosition = new Rect(
                            currentPosition.x + fieldPosition.width + 2,
                            currentPosition.y,
                            20,
                            currentPosition.height
                        );

                        list.GetArrayElementAtIndex(index).objectReferenceValue =
                            EditorGUI.ObjectField(
                                fieldPosition,
                                $"Item #{index + 1}",
                                list.GetArrayElementAtIndex(index).objectReferenceValue,
                                typeof(IInvokeable), true
                                );
                        if (GUI.Button(buttonPosition, "-")) list.DeleteArrayElementAtIndex(index);
                        index++;
                    }

                    currentPosition.y += itemSpacing;
                    propertyHeight += itemSpacing;
                    if (GUI.Button(currentPosition, "Add")) list.InsertArrayElementAtIndex(list.arraySize);

                    propertyHeight += itemSpacing;
                }
            }
        }
    }
}