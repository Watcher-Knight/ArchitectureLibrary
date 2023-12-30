using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;

namespace ArchitectureLibrary
{
    class GameObjectFactory
    {
        public static void CreateObject(GameObject template, string name)
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

        public static void CreatePrefab(GameObject template, string name)
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