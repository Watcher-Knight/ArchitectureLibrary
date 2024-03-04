using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;

namespace ArchitectureLibrary
{
    public abstract class AnimatorParameterDrawer : PropertyDrawer
    {
        protected abstract AnimatorControllerParameterType ParameterType { get; }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedObject parent = property.serializedObject;
            SerializedProperty animator = property.FindPropertyRelative("Animator");
            SerializedProperty name = property.FindPropertyRelative("Name");

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            Rect animatorPosition = new(position);
            animatorPosition.width = animatorPosition.width / 2 - 1;

            Rect selectionPosition = new(position);
            selectionPosition.width = selectionPosition.width / 2 - 1;
            selectionPosition.x += animatorPosition.width + 2;

            if (
                typeof(Component).IsAssignableFrom(parent.targetObject.GetType()) &&
                animator.objectReferenceValue == null &&
                (parent.targetObject as Component).TryGetComponentInGroup(out Animator newAnimatorValue)
            ) animator.objectReferenceValue = newAnimatorValue;
            animator.objectReferenceValue = EditorGUI.ObjectField(animatorPosition, animator.objectReferenceValue, typeof(Animator), true);

            if (animator.objectReferenceValue != null)
            {
                AnimatorController controller = (animator.objectReferenceValue as Animator).runtimeAnimatorController as AnimatorController;

                if (controller != null)
                {
                    int index;
                    string[] parameters = controller.parameters.Where(p => p.type == ParameterType).Select(parameter => parameter.name).ToArray();
                    if (parameters.Length > 0)
                    {
                        index = parameters.Contains(name.stringValue) ? parameters.IndexOf(name.stringValue) : 0;
                        index = EditorGUI.Popup(selectionPosition, index, parameters);
                        name.stringValue = parameters[index];
                    }
                    else NoParameterGUI(selectionPosition);
                }
                else NoParameterGUI(selectionPosition);
            }
            else NoParameterGUI(selectionPosition);

            EditorGUI.EndProperty();
        }

        private void NoParameterGUI(Rect position) => EditorGUI.LabelField(position, "None");
    }

    [CustomPropertyDrawer(typeof(AnimatorBoolParameter))]
    public class AnimatorBoolParameterDrawer : AnimatorParameterDrawer
    {
        protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Bool;
    }

    [CustomPropertyDrawer(typeof(AnimatorFloatParameter))]
    public class AnimatorFloatParameterDrawer : AnimatorParameterDrawer
    {
        protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Float;
    }

    [CustomPropertyDrawer(typeof(AnimatorIntParameter))]
    public class AnimatorIntParameterDrawer : AnimatorParameterDrawer
    {
        protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Int;
    }

    [CustomPropertyDrawer(typeof(AnimatorTriggerParameter))]
    public class AnimatorTriggerParameterDrawer : AnimatorParameterDrawer
    {
        protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Trigger;
    }

}