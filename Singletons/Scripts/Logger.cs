using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [CreateAtPath(AssetPaths.singletons, "Logger")]
    public class Logger : ScriptableObject, IInvokeable, IInvokeable<string>
    {
        public void Log(string message) => Debug.Log(message);
        public void Log(Variable message) => Debug.Log(message.ToString());

        public void Invoke() => Log("Success!");
        public void Invoke(string message) => Log(message);
    }
}