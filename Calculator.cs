using Unity.VisualScripting;
using UnityEngine;

namespace ArchitectureLibrary
{
    public class Calculator
    {
        public static Vector2 RotationToDirection(float degrees)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, -degrees);
            Vector2 direction = rotation * Vector3.up;

            return direction;
        }

        public static float DirectionToRotation(Vector2 direction)
        {
            return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }

        public static Vector2 GetDirection(Vector2 value)
        {
            if (value == Vector2.zero) return Vector2.zero;
            return RotationToDirection(DirectionToRotation(value));
        }

        public static float GetDistance(Vector2 v1, Vector2 v2)
        {
            return Mathf.Sqrt(Mathf.Pow(v2.x - v1.x, 2) + Mathf.Pow(v2.y - v1.y, 2));
        }

        public static Vector2 Sign(Vector2 value) => new Vector2(Mathf.Sign(value.x), Mathf.Sign(value.y));
        public static Vector2 Inverse(Vector2 value) => new Vector2(value.y, value.x);
    }
}