using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class Raycast : Cast
    {
        public Raycast(Vector2 origin, Vector2 direction, float distance, bool draw = false)
        {
            colliders = GetColliders(origin, direction, distance);
            if (draw) Debug.DrawRay(origin, direction.normalized * distance, Color.red);
        }
        public Raycast(Vector2 origin, Vector2 direction, float distance, Tag tag, bool draw = false)
        {
            Collider2D[] oldColliders = GetColliders(origin, direction, distance);
            List<Collider2D> newColliders = new();
            foreach (Collider2D collider in oldColliders)
            {
                if (Tags.Contains(collider, tag)) newColliders.Add(collider);
            }
            colliders = newColliders.ToArray();

            if (draw) DrawRay(origin, direction, distance);
        }

        private Collider2D[] GetColliders(Vector2 origin, Vector2 direction, float distance)
        {
            List<Collider2D> colliders = new();
            RaycastHit2D[] raycasts = Physics2D.RaycastAll(origin, direction, distance);
            foreach (RaycastHit2D raycast in raycasts) colliders.Add(raycast.collider);
            return colliders.ToArray();
        }
        private void DrawRay(Vector2 origin, Vector2 direction, float distance)
        {
            Debug.DrawRay(origin, direction.normalized * distance, Color.red);
        }
    }
}