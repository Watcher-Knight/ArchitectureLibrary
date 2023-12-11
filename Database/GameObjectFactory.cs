using System.IO;
using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    class GameObjectFactory
    {
        public static PrefabList prefabList;

        [MenuItem("Assets/Create/Template/Projectile", false, 1)] static void CreateProjectilePrefab() => CreatePrefab(prefabList.projectile, "NewProjectilePrefab");
        [MenuItem("GameObject/Template/Projectile", false, 0)] static void CreateProjectileObject() => CreateObject(prefabList.projectile, "NewProjectile");

        private static void CreateObject(GameObject template, string name)
        {
            GameObject gameObject;
            if (Selection.activeTransform != null)
            {
                gameObject = Object.Instantiate(template, Selection.activeTransform);
            }
            else
            {
                Vector3 position = SceneView.lastActiveSceneView.pivot;
                gameObject = Object.Instantiate(template, new Vector3(position.x, position.y, 0), Quaternion.identity);
            }
            gameObject.name = name;
        }

        private static void CreatePrefab(GameObject template, string name)
        {
            string directory = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (directory == null) return;
            string path = $"{directory}/{name}.prefab";
            path = AssetDatabase.GenerateUniqueAssetPath(path);

            GameObject gameObject = template;
            PrefabUtility.SaveAsPrefabAsset(gameObject, path);
        }
    }
}