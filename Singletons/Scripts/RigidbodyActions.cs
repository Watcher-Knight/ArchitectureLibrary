using UnityEngine;

namespace ArchitectureLibrary
{
    [CreateAtPath(AssetPaths.singletons, "RigidbodyActions")]
    public class RigidbodyActions : ScriptableObject
    {
        public void FullStop(Rigidbody2D rigidbody, Axis axis)
        {
            switch (axis)
            {
                case Axis.x: rigidbody.velocity = new Vector2(0, rigidbody.velocity.y); break;
                case Axis.y: rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0); break;
                case Axis.all: default: rigidbody.velocity = Vector2.zero; break;
            }
        }
    }
}