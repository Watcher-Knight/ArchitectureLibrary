using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    [CreateAtPath(AssetPaths.arcLibPrefabs, "PrefabList")]
    public class PrefabList : ScriptableObject
    {
        public static string dataDirectory = Application.dataPath.Replace("Assets", "") + AssetPaths.arcLibPrefabs;
        public static string dataFile = "/PrefabData.txt";
        public static List<string> names = new List<string>()
        {
            "Wall",
            "Projectile",
            "Enemy"
        };

        private static Dictionary<string, GameObject> objects;

        public static GameObject GetObject(string name)
        {
            if (objects == null)
            {
                string dataPath = dataDirectory + dataFile;
                Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

                FileInfo file = new FileInfo(dataPath);
                while (!Directory.Exists(dataDirectory)) file.Directory.Create();
                if (!File.Exists(dataPath)) File.WriteAllText(dataPath, "");
                string json = File.ReadAllText(dataPath);
                if (json != "null" && json != "{}")
                {
                    SerializableDictionary<string, string> serializableDictionary =
                        JsonUtility.FromJson<SerializableDictionary<string, string>>(json) ??
                        new SerializableDictionary<string, string>(new Dictionary<string, string>());

                    Dictionary<string, string> paths = serializableDictionary.ToDictionary();
                    foreach (KeyValuePair<string, string> pair in paths)
                    {
                        string key = pair.Key;
                        GameObject value = (GameObject)AssetDatabase.LoadAssetAtPath(pair.Value, typeof(GameObject));
                        dictionary.Add(key, value);
                    }
                }

                objects = dictionary;
            }

            foreach (string n in names)
                if (!objects.ContainsKey(n)) objects.Add(n, null);

            foreach (KeyValuePair<string, GameObject> pair in objects)
                if (!names.Contains(pair.Key)) objects.Remove(pair.Key);

            return objects[name];
        }

        public static void SetObject(string name, GameObject value)
        {
            if (value != objects[name])
            {
                objects[name] = value;

                string dataPath = dataDirectory + dataFile;
                Dictionary<string, string> paths = new Dictionary<string, string>();
                foreach (KeyValuePair<string, GameObject> pair in objects)
                {
                    string key = pair.Key;
                    string val = AssetDatabase.GetAssetPath(pair.Value);
                    paths.Add(key, val);
                }
                string json = JsonUtility.ToJson(new SerializableDictionary<string, string>(paths));
                FileInfo file = new FileInfo(dataPath);
                while (!Directory.Exists(dataDirectory)) file.Directory.Create();
                File.WriteAllText(dataPath, json);
            }
        }

        [MenuItem("Test/Get Asset Path")]
        private static void GetAssetPath() => Debug.Log(AssetDatabase.GetAssetPath(objects["Wall"]));

        [CustomEditor(typeof(PrefabList))]
        public class PrefabListEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                foreach (string name in PrefabList.names)
                {
                    PrefabList.SetObject(name, (GameObject)EditorGUILayout.ObjectField(name, PrefabList.GetObject(name), typeof(GameObject), false));
                }
            }
        }
    }
}