using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace ArchitectureLibrary
{
    public static class EventTagEditor
    {
        public static string JsonPath = Application.dataPath + "/ArchitectureLibrary/Events/Editor/EventTags.json";
        private static List<EventTag> List;
        private static int CurrentId = 1;

        private static EventTag Create(string name)
        {
            CurrentId += 1;
            return new() { Name = name, Id = CurrentId };
        }
        private static EventTag Create(string name, int id)
        {

            return new() { Name = name, Id = id };
        }

        public static EventTag[] GetTags()
        {
            if (List == null)
            {
                Data data = GetData();
                List = new(data.List);
                CurrentId = data.CurrentId;
            }
            if (List.Count == 0)
            {
                List.Add(Create("Dafault"));
                SaveData();
            }
            return List.ToArray();
        }
        public static bool CreateTag(string name)
        {
            if (!List?.Any(tag => tag.Name == name) ?? false)
            {
                List.Add(Create(name));
                SaveData();
                return true;
            }
            return false;
        }
        public static void DeleteTag(EventTag tag)
        {
            List?.Remove(tag);
            SaveData();
        }
        public static void EditTag(int index, string name)
        {
            if (!List?.Any(tag => tag.Name == name) ?? false)
            {
                List[index] = Create(name, List[index].Id);
                SaveData();
            }
        }

        private static Data GetData()
        {
            Data data = new() { List = new(), CurrentId = 0 };
            string json = File.ReadAllText(JsonPath);
            if (json != "" && json != "{}")
            {
                data = JsonConverter.Read<Data>(json);
            }
            return data;
        }
        private static void SaveData()
        {
            Data data = new() { List = List, CurrentId = CurrentId };
            string json = JsonConverter.Convert(data);
            File.WriteAllText(JsonPath, json);
        }

        [Serializable]
        public struct Data
        {
            public List<EventTag> List;
            public int CurrentId;
        }
    }
}