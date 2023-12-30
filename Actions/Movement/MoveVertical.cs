using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.moveVertical)]
    public class MoveVertical : AxisAction
    {
        [SerializeField] private FloatVariable moveSpeed;

        protected override void CreateStats()
        {
            string path = AssetPaths.stats;
            string name = $"{transform.root.gameObject.name}MoveSpeed";
            moveSpeed = ScriptableObjectFactory.Create<FloatVariable>(path, name);
        }

        private float direction = 0;
        public override bool isActive { get => direction != 0; }
        private void Update()
        {
            transform.position += Vector3.up * direction * moveSpeed.value * Time.deltaTime;
        }

        public override void Invoke(float value)
        {
            direction = Math.Sign(value);
        }
    }
}