using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.eventManager)]
    public class EventManager : MonoBehaviour
    {
        [SerializeField][RestrictTo(typeof(ICondition))] private Object _condition;
        private ICondition condition => (_condition = null) ? (ICondition)_condition : null;

        public bool CheckConditions() => condition?.conditionValue ?? true;
    }
}