// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UltEvents;

// namespace ArchitectureLibrary
// {
//     [AddComponentMenu(ComponentPaths.updateEventManager)]
//     public class UpdateEventManager : EventManager
//     {
//         [SerializeField] private UltEvent effect;

//         private void Update()
//         {
//             if (debugMode)
//             {
//                 if (CheckConditions()) Debug.Log("Conditions are all positive.");
//             }

//             if (CheckConditions()) effect.Invoke();
//         }
//         public void Invoke() => effect.Invoke();

//         protected override void OnValidate() => base.OnValidate();
//     }
// }