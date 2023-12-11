using UnityEngine;

namespace ArchitectureLibrary
{
    public class PrefabList : ScriptableObject
    {
        public GameObject projectile;

        private void OnEnable()
        {
            GameObjectFactory.prefabList = this;
        }
    }
}