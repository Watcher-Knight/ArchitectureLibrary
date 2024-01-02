using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.tags)]
    public class Tags : MonoBehaviour, IEnumerable<Tag>
    {
        [SerializeField] private List<Tag> tags = new List<Tag>();

        public bool Contains(Tag tag) => tags.Contains(tag);

        public List<Tag> GetTags() => tags;
        public IEnumerator<Tag> GetEnumerator() => tags.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}