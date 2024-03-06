using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary.Editor
{
    public static class JsonConverter
    {
        public static string Convert(object data)
        {
            return JsonUtility.ToJson(data);
        }
        public static T Read<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ConvertEnumerable<T>(IEnumerable<T> enumerable)
        {
            return JsonUtility.ToJson(new CEnum<T>(enumerable));
        }

        public static IEnumerable<T> ReadEnumerable<T>(string json)
        {
            return JsonUtility.FromJson<CEnum<T>>(json);
        }

        [Serializable]
        public struct CEnum<T> : IEnumerable<T>
        {
            public List<T> list;
            public CEnum(IEnumerable<T> enumerable) => list = new(enumerable);
            public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}