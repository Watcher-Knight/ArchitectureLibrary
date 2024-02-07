using System;
using System.Data;
using System.Reflection;
using UnityEditor;

namespace ArchitectureLibrary
{
    public static class Properties
    {
        public static readonly BindingFlags bindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static Type GetPropertyType(this SerializedProperty property)
        {
            FieldInfo fi = GetField(property);
            return fi.FieldType;
        }

        public static FieldInfo GetField(this SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            string[] fullPath = property.propertyPath.Split('.');
            FieldInfo fi = parentType.GetField(fullPath[0], bindingFlags);
            for (int i = 1; i < fullPath.Length; i++)
            {
                fi = fi.FieldType.GetField(fullPath[i], bindingFlags);
            }
            return fi;
        }

        public static object GetParentObject(this SerializedProperty property)
        {
            SerializedObject parentObject = property.serializedObject;
            string[] fullPath = property.propertyPath.Split('.');
            if (fullPath.Length < 2) return parentObject.targetObject;
            object parent = parentObject.FindProperty(fullPath[0]);
            for (int i = 0; i < fullPath.Length; i++)
            {
                FieldInfo field = parent.GetType().GetField(fullPath[i], bindingFlags);
                parent = field.GetValue(parent);
            }
            return parent;
        }

        public static void SetValue(this SerializedProperty property, object value)
        {
            FieldInfo field = property.GetField();
            object parent = property.GetParentObject();
            field.SetValue(parent, value);
        }

        public static T GetObject<T>(this SerializedProperty property)
        {
            object targetObject = property.serializedObject.targetObject;
            string[] fullPath = property.propertyPath.Split('.');
            for (int i = 0; i < fullPath.Length; i++)
            {
                FieldInfo field = targetObject.GetType().GetField(fullPath[i], bindingFlags);
                targetObject = field.GetValue(targetObject);
            }

            return Convert<T>(targetObject);
        }

        public static T Convert<T>(this object obj)
        {
            if (obj != null)
            {
            if (typeof(T).IsAssignableFrom(obj.GetType())) return (T)obj;
            throw new InvalidCastException($"Cannot convert from type \"{obj.GetType()}\" to type \"{typeof(T)}.\"");
            }
            if (!typeof(T).IsValueType || Nullable.GetUnderlyingType(typeof(T)) != null)
                return default;
            throw new NoNullAllowedException($"Type \"{typeof(T)}\" cannot be null.");
        }
    }
}