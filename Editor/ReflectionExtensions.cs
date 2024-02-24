using System;
using System.Linq;
using System.Reflection;

namespace ArchitectureLibrary
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this MemberInfo element) where T : Attribute => element.GetCustomAttribute<T>() != null;
        public static FieldInfo[] GetFieldsWithAttribute<T>(this Type type, BindingFlags bindingFlags = SerializedProperties.BindingFlags) where T : Attribute =>
            type.GetFields(bindingFlags).Where(f => f.HasAttribute<T>()).ToArray();
        public static PropertyInfo[] GetPropertiesWithAttribute<T>(this Type type, BindingFlags bindingFlags = SerializedProperties.BindingFlags) where T : Attribute =>
            type.GetProperties(bindingFlags).Where(p => p.HasAttribute<T>()).ToArray();
        public static MethodInfo[] GetMethodsWithAttribute<T>(this Type type, BindingFlags bindingFlags = SerializedProperties.BindingFlags) where T : Attribute =>
            type.GetMethods(bindingFlags).Where(m => m.HasAttribute<T>()).ToArray();
            
        public static object Default(this Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}