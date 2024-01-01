// using System;
// using UnityEngine;

// namespace ArchitectureLibrary
// {
//     [CreateAssetMenu(fileName = "GlobalEvent", menuName = "Events/GlobalEvent", order = 0)]
//     public class GlobalEvent : ScriptableObject
//     {
//         public delegate void Action();
//         public event Action effect;

//         public void Invoke()
//         {
//             effect?.Invoke();
//         }
//     }
// }