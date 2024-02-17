using UnityEngine;

namespace ArchitectureLibrary
{
    public static class FilePaths
    {
        public static string Project = Application.dataPath.Replace("/Assets", "");

        public static string Settings = Project + "/ProjectSettings";
    }
}