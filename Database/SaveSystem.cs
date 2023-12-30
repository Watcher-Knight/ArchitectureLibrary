using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

namespace ArchitectureLibrary
{
    public static class SaveSystem
    {
        // public static void Save<T>(T data, string path) where T : new()
        // {
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     string path = $"{Application.dataPath}/{path}";
        //     FileStream stream = new FileStream(path, FileMode.Create);

        //     GameData data = new GameData(d);

        //     formatter.Serialize(stream, data);
        //     stream.Close();
        // }

        // public static T Load(string path) where T : new()
        // {
        //     string path = Application.persistentDataPath + relativePath;
        //     if (File.Exists(path))
        //     {
        //         BinaryFormatter formatter = new BinaryFormatter();
        //         FileStream stream = new FileStream(path, FileMode.Open);

        //         GameData data = formatter.Deserialize(stream) as GameData;
        //         stream.Close();

        //         return data;
        //     }
        //     else
        //     {
        //         Debug.LogError($"Save file not found in {path}");
        //         return null;
        //     }
        // }
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        public List<TKey> keys = new List<TKey>();
        public List<TValue> values = new List<TValue>();

        public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary.Add(keys[i], values[i]);
            }
            return dictionary;
        }
    }
}
