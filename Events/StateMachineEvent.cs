using UnityEditor.TerrainTools;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AddComponentMenu(ComponentPaths.stateMachineEvent)]
    public class StateMachineEventListener : EventListener, IAxisControllable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ParameterType parameterType = ParameterType.Bool;
        [SerializeField] private string parameter;
        private bool useFloat => parameterType == ParameterType.Float;
        private bool useInt => parameterType == ParameterType.Integer;
        [SerializeField][DrawIf("useFloat")] private float floatValue = 0f;
        [SerializeField][DrawIf("useInt")] private int intValue = 0;

        public override void Invoke()
        {
            if (animator != null)
            {
                switch (parameterType)
                {
                    case ParameterType.Bool: default: animator.SetBool(parameter, true); break;
                    case ParameterType.Trigger: animator.SetTrigger(parameter); break;
                    case ParameterType.Float: animator.SetFloat(parameter, floatValue); break;
                    case ParameterType.Integer: animator.SetInteger(parameter, intValue); break;
                }
            }
        }

        public override void Cancel()
        {
            if (animator != null && parameterType == ParameterType.Bool)
            {
                animator.SetBool(parameter, false);
            }
        }

        public void Control(float value)
        {
            switch (parameterType)
            {
                case ParameterType.Float: default: animator.SetFloat(parameter, value); break;
                case ParameterType.Integer: animator.SetInteger(parameter, (int)value); break;
            }
        }
    }
}