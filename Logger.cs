using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAssetMenu(fileName = "Logger", menuName = "Debug/Logger", order = 0)]
    public class Logger : ScriptableObject
    {
        public void Log(string message) => Debug.Log(message);
        public void Log(Variable message) => Debug.Log(message.ToString());
    }
}