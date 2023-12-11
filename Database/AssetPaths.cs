using System.IO;
using UnityEditor;

public static class AssetPaths
{
    public readonly static string master = "Assets";

    public readonly static string scenes = $"{master}/Scenes";
    public readonly static string prefabs = $"{master}/Prefabs";
    public readonly static string animations = $"{master}/Animations";
    public readonly static string stats = $"{master}/Stats";

    public static void CreateDirectory(string directory)
    {
        string folder = "";
        string path = "";
        foreach (char c in directory)
        {
            if (c != '/' && c != '\\')
            {
                folder += c;
            }
            else
            {
                if (!Directory.Exists($"{path}/{folder}")) AssetDatabase.CreateFolder(path, folder);
                path += (path == "") ? $"{folder}" : $"/{folder}";
                folder = "";
            }
        }
        if (!Directory.Exists($"{path}/{folder}")) AssetDatabase.CreateFolder(path, folder);
    }
}