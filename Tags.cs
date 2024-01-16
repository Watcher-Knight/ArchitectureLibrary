using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.tags)]
    public class Tags : MonoBehaviour, IEnumerable<Tag>
    {
        [SerializeField] private List<Tag> tags = new List<Tag>();

        public bool Contains(Tag tag)
        {
            return tag == null || tags.Contains(tag);
        }
        public static bool Contains(GameObject gameObject, Tag tag)
        {
            if (tag == null) return true;
            if (gameObject.TryGetComponent(out Tags tags))
            {
                return tags.Contains(tag);
            }
            return false;
        }
        public static bool Contains(Component component, Tag tag)
        {
            return Contains(component.gameObject, tag);
        }

        public Tag[] GetTags() => tags.ToArray();
        public static Tag[] GetTags(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Tags tags))
            {
                return tags.ToArray();
            }
            return new Tag[0];
        }
        public IEnumerator<Tag> GetEnumerator() => tags.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}