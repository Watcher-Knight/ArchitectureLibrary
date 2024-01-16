using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

namespace ArchitectureLibrary
{
    public abstract class Cast : IEnumerable<Collider2D>
    {
        public Collider2D[] colliders;

        public IEnumerator<Collider2D> GetEnumerator() => ((IEnumerable<Collider2D>)colliders).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => colliders.GetEnumerator();
    }
}