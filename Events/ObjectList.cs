using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace ArchitectureLibrary
{
    public abstract class ObjectList { }
    [Serializable]
    public class ObjectList<T> : ObjectList, IEnumerable<T>
    {
        public List<Object> items = new();
        public IEnumerator<T> GetEnumerator()
        {
            List<T> list = new();
            foreach (Object item in items) list.Add((T)(object)item);
            return list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [CustomPropertyDrawer(typeof(ObjectList), true)]
    public class ObjectListDrawer : PropertyDrawer
    {
        private bool foldout = false;
        private float propertyHeight = 0f;
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => propertyHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type propertyType = PropertyDrawerFinder.GetPropertyType(property);
            Type[] genericTypes = propertyType.GetGenericArguments();
            if (genericTypes.Length == 0)
            {
                base.OnGUI(position, property, label);
                propertyHeight = base.GetPropertyHeight(property, label);
                return;
            }

            BaseGUI(position, property, label, genericTypes[0]);
        }

        protected void BaseGUI(Rect position, SerializedProperty property, GUIContent label, Type type)
        {
            SerializedProperty list = property.FindPropertyRelative("items");

            float itemHeight = base.GetPropertyHeight(property, label);
            float itemSpacing = itemHeight + 2;
            Rect currentPosition = new(position.x, position.y, position.width, itemHeight);

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
                    Rect buttonPosition = new(
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
                            type, true
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