using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class Tags : MonoBehaviour
    {
        [SerializeField] private List<Tag> tags = new List<Tag>();

        public bool Contains(Tag tag) => tags.Contains(tag);

        public List<Tag> GetTags() => tags;
    }
}