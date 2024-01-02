using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    public static class Collisions
    {
        public static Collider2D[] GetContactColliders(Collider2D collider, Tag tag, Direction direction)
        {
            Vector3 directionVector;
            float width;
            float length;
            switch (direction)
            {
                case Direction.up:
                default:
                    directionVector = Vector3.up;
                    length = collider.bounds.size.y;
                    width = collider.bounds.size.x;
                    break;
                case Direction.down:
                    directionVector = Vector3.down;
                    length = collider.bounds.size.y;
                    width = collider.bounds.size.x;
                    break;
                case Direction.left:
                    directionVector = Vector3.left;
                    length = collider.bounds.size.x;
                    width = collider.bounds.size.y;
                    break;
                case Direction.right:
                    directionVector = Vector3.right;
                    length = collider.bounds.size.x;
                    width = collider.bounds.size.y;
                    break;
            }

            RaycastHit2D[] casts = Physics2D.BoxCastAll(
                collider.bounds.center + directionVector * length * 0.5f,
                new Vector2(width, 0.02f),
                0f,
                directionVector,
                0.02f
            );

            List<Collider2D> colliders = new();

            foreach (RaycastHit2D cast in casts)
            {
                Tags tags = cast.collider.GetComponent<Tags>();
                if (tags != null)
                {
                    if (tags.Contains(tag)) colliders.Add(cast.collider);
                }
            }

            return colliders.ToArray();
        }

        public static Collider2D GetContactCollider(Collider2D collider, Tag tag, Direction direction)
        {
            Collider2D[] colliders = GetContactColliders(collider, tag, direction);

            return colliders[0];
        }

        public static bool IsTouching(Collider2D collider, Tag tag, Direction direction)
        {
            Collider2D[] colliders = GetContactColliders(collider, tag, direction);

            return colliders.Length > 0;
        }
    }
}