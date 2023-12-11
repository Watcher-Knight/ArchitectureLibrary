using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu("Movement/Move Bidirectional (Simple)")]
    public class MoveBidirectional : V2AxisAction
    {
        [SerializeField] private FloatVariable moveSpeed;

        public override void CreateStats()
        {
            string path = AssetPaths.stats;
            string name = $"{transform.root.gameObject.name}MoveSpeed";
            moveSpeed = ScriptableObjectFactory.Create<FloatVariable>(path, name);
        }

        private Vector2 direction = Vector2.zero;
        public override bool isActive { get => direction != Vector2.zero; }

        private void Start()
        {
            if (moveSpeed == null) moveSpeed = ScriptableObject.CreateInstance<FloatVariable>();
        }
        private void Update()
        {
            transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed.value * Time.deltaTime;
        }

        public override void Invoke(Vector2 value)
        {
            if (Math.Abs(value.x) > 1) value.x = Math.Sign(value.x);
            if (Math.Abs(value.y) > 1) value.y = Math.Sign(value.y);

            direction = value;
        }
    }
}