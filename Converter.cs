using UnityEngine;

namespace ArchitectureLibrary
{
    public class Converter
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
    }
}