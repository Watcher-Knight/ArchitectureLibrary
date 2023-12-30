using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreateAtPathAttribute : Attribute
    {
        string directory;
        string name;

        public CreateAtPathAttribute(string directory, string name)
        {
            this.directory = directory;
            this.name = name;
        }

        [InitializeOnLoadMethod]
        private static void CreateInstances()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(CreateAtPathAttribute));
            foreach (Type type in assembly.GetTypes())
            {
                object[] attributes = type.GetCustomAttributes(typeof(CreateAtPathAttribute), true);
                if (attributes.Length > 0)
                {
                    string directory = ((CreateAtPathAttribute)attributes[0]).directory;
                    string name = ((CreateAtPathAttribute)attributes[0]).name;
                    typeof(CreateAtPathAttribute).GetMethod("CreateSingleton", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(type).Invoke(null, new object[] {directory, name});
                }
            }
        }

        private static void CreateSingleton<T>(string directory, string name) where T : ScriptableObject
        {
            if (!File.Exists($"{directory}/{name}.asset"))
                ScriptableObjectFactory.Create<T>(directory, name);
        }
    }
}