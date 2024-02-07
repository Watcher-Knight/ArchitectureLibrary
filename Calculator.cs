using UnityEngine;

namespace ArchitectureLibrary
{
    public static class Calculator
    {
        public static Vector2 ToDirection(this float degrees)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, -degrees);
            Vector2 direction = rotation * Vector3.up;

            return direction;
        }

        public static float ToRotation(this Vector2 direction)
        {
            return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }

        public static float GetDistance(this Vector2 v1, Vector2 v2)
        {
            return Mathf.Sqrt(Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.y - v1.y, 2));
        }

        public static Vector2 Sign(this Vector2 value) => new Vector2(Mathf.Sign(value.x), Mathf.Sign(value.y));
        public static Vector2 Inverse(this Vector2 value) => new Vector2(value.y, value.x);
    }
}