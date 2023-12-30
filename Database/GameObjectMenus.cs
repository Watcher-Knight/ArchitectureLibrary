using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public static class GameObjectMenus
    {
        [MenuItem("Assets/Create/Template/Wall", false, 1)] static void CreateWallPrefab() => GameObjectFactory.CreatePrefab(PrefabList.GetObject("Wall"), "NewWallPrefab");
        [MenuItem("GameObject/Template/Wall", false, 0)] static void CreateWallObject() => GameObjectFactory.CreateObject(PrefabList.GetObject("Wall"), "NewWall");
        [MenuItem("Assets/Create/Template/Projectile", false, 1)] static void CreateProjectilePrefab() => GameObjectFactory.CreatePrefab(PrefabList.GetObject("Projectile"), "NewProjectilePrefab");
        [MenuItem("GameObject/Template/Projectile", false, 0)] static void CreateProjectileObject() => GameObjectFactory.CreateObject(PrefabList.GetObject("Projectile"), "NewProjectile");
    }
}