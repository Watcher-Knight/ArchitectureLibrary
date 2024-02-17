using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class ReadonlyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>//, IEnumerable<TValue>
    {
        private readonly KeyValuePair<TKey, TValue>[] Pairs;

        public ReadonlyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> pairs) => Pairs = pairs.ToArray();

        public TValue this[TKey key] { get => Pairs.First(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key)).Value; }
        public TKey GetKey(TValue value) => Pairs.First(pair => EqualityComparer<TValue>.Default.Equals(pair.Value, value)).Key;

        //IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => Pairs.Select(pair => pair.Value).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Pairs.AsEnumerable().GetEnumerator();
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        [SerializeField] private IEnumerable<Pair<TKey, TValue>> Pairs;

        public SerializableDictionary(IDictionary<TKey, TValue> dictionary) => Set(dictionary);
        public SerializableDictionary() => Pairs = new Pair<TKey, TValue>[0];
        private Dictionary<TKey, TValue> ToDictionary() => Pairs.ToDictionary(pair => pair.Key, pair => pair.Value);
        private void Set(IDictionary<TKey, TValue> dictionary) => Pairs = dictionary.Select(pair => new Pair<TKey, TValue>(pair.Key, pair.Value));

        public TValue this[TKey key]
        {
            get => Pairs.First(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key)).Value;
            set
            {
                Dictionary<TKey, TValue> dictionary = ToDictionary();
                dictionary[key] = value;
                Set(dictionary);
            }
        }

        public ICollection<TKey> Keys => ToDictionary().Keys;
        public ICollection<TValue> Values => ToDictionary().Values;
        public int Count => Pairs.Count();
        public bool IsReadOnly => false;
        public void Add(TKey key, TValue value) => Pairs = Pairs.Append(new(key, value));
        public void Add(KeyValuePair<TKey, TValue> item) => Pairs = Pairs.Append(new(item.Key, item.Value));
        public void Clear() => Pairs = new Pair<TKey, TValue>[0];
        public bool Contains(KeyValuePair<TKey, TValue> item) => ToDictionary().Contains(item);
        public bool ContainsKey(TKey key) => ToDictionary().ContainsKey(key);
        public bool ContainsValue(TValue value) => ToDictionary().ContainsValue(value);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => ToDictionary().GetEnumerator();

        public bool Remove(TKey key)
        {
            Dictionary<TKey, TValue> dictionary = ToDictionary();
            bool result = dictionary.Remove(key);
            Set(dictionary);
            return result;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            IDictionary<TKey, TValue> dictionary = ToDictionary();
            bool result = dictionary.Remove(item);
            Set(dictionary);
            return result;
        }

        public bool TryGetValue(TKey key, out TValue value) => ToDictionary().TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => ToDictionary().GetEnumerator();

        [Serializable]
        public struct Pair<TK, TV>
        {
            public TK Key;
            public TV Value;
            public Pair(TK key, TV value)
            {
                Key = key;
                Value = value;
            }
            public KeyValuePair<TK, TV> ToKeyValuePair() => new(Key, Value);
        }
    }

    public static class DictionaryExtensions
    {
        public static ReadonlyDictionary<TKey, TValue> Readonly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => new(dictionary);
        public static SerializableDictionary<TKey, TValue> Serializeable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) => new(dictionary);
        public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            bool query(KeyValuePair<TKey, TValue> pair) => EqualityComparer<TValue>.Default.Equals(pair.Value, value);
            if (dictionary.Any(query))
            {
                TKey key = dictionary.First(query).Key;
                dictionary.Remove(key);
                return true;
            }
            return false;
        }

        public static IEnumerable<TKey> GetKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
            pairs.Select(pair => pair.Key);
        public static IEnumerable<TValue> GetValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs) =>
            pairs.Select(pair => pair.Value);
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TKey> keys, IEnumerable<TValue> values) =>
            keys.Zip(values, (key, value) => new { key, value }).ToDictionary(pair => pair.key, pair => pair.value);
    }
}