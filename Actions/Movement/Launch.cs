using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.launch)]
    public class Launch : Action
    {
        [SerializeField] private FloatVariable force;
        [SerializeField] private new Rigidbody2D rigidbody;

        protected override void CreateStats()
        {
            string path = AssetPaths.stats;
            string name = $"{transform.root.gameObject.name}LaunchForce";
            force = ScriptableObjectFactory.Create<FloatVariable>(path, name);
        }

        public override bool isActive => Math.Abs(rigidbody.velocity.x) > 0.01f || Math.Abs(rigidbody.velocity.y) > 0.01f;
        private Vector2 targetDirection = Vector2.zero;

        private void Awake()
        {
            if (force == null) force = ScriptableObject.CreateInstance<FloatVariable>();

            if (rigidbody == null)
            {
                bool shouldRemoveGravity = (!transform.root.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb));
                rigidbody = Components.AssignRootComponent<Rigidbody2D>(gameObject);
                if (shouldRemoveGravity) rigidbody.gravityScale = 0;
            }
        }

        private void _Invoke(Vector2 direction)
        {
            rigidbody.AddForce(direction * force.value, ForceMode2D.Impulse);
        }

        public void Invoke()
        {
            Vector2 direction = Converter.RotationToDirection(rigidbody.transform.eulerAngles.z);
            _Invoke(direction);
        }
        public void Invoke(Vector2 direction)
        {
            _Invoke(Converter.GetDirection(direction));
        }
        public void Invoke(Vector2Variable direction)
        {
            _Invoke(Converter.GetDirection(direction.value));
        }
        public void Invoke(Vector3Variable direction)
        {
            _Invoke(Converter.GetDirection(new Vector2(direction.value.x, direction.value.y)));
        }
        public void Invoke(float rotation)
        {
            Vector2 direction = Converter.RotationToDirection(rotation);
            _Invoke(direction);
        }
        public void Invoke(FloatVariable rotation)
        {
            Vector2 direction = Converter.RotationToDirection(rotation.value);
            _Invoke(direction);
        }

        public void InvokeRelative(Axis axis = Axis.all)
        {
            switch (axis)
            {
                case Axis.x: _Invoke(Vector2.right * rigidbody.velocity.x); break;
                case Axis.y: _Invoke(Vector2.up * rigidbody.velocity.y); break;
                case Axis.all: default: _Invoke(rigidbody.velocity); break;
            }
        }
        public void InvokeRelativeReverse(Axis axis = Axis.all)
        {
            switch (axis)
            {
                case Axis.x: _Invoke(Vector2.left * rigidbody.velocity.x); break;
                case Axis.y: _Invoke(Vector2.down * rigidbody.velocity.y); break;
                case Axis.all: default: _Invoke(-rigidbody.velocity); break;
            }
        }
    }


    // Not currently being used
    //[CreateAssetMenu(fileName = "LaunchStats", menuName = "Stats/Launch", order = 0)]
    public class LaunchStats : ScriptableObject
    {
        [SerializeField] private float _force;
        public float force => _force;
    }
}