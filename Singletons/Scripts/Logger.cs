using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [CreateAtPath(AssetPaths.singletons, "Logger")]
    public class Logger : ScriptableObject
    {
        public void Log(string message) => Debug.Log(message);
        public void Log(Variable message) => Debug.Log(message.ToString());
    }
}