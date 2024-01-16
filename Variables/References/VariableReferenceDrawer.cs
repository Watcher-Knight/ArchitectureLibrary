using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace ArchitectureLibrary
{
    [CustomPropertyDrawer(typeof(VariableReference), true)]
    public class DefaultVariableReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, label, new GUIContent("Can't display this field"));
        }
    }

    public abstract class VariableReferenceDrawer : PropertyDrawer
    {
        protected abstract void VariableField(Rect position, SerializedProperty property);
        protected abstract void ConstantField(Rect position, SerializedProperty property);
        protected abstract void AssignVariable(SerializedProperty property);
        protected void CreateVariable<T>(SerializedProperty property) where T : ScriptableObject
        {
            SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
            SerializedProperty variable = property.FindPropertyRelative("variable");

            Object owner = property.serializedObject?.targetObject;
            object reference = owner?.GetType().GetField(property.name, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(owner);
            FieldInfo constantField = reference?.GetType().GetField("constant", BindingFlags.Instance | BindingFlags.NonPublic);

            if (variable.objectReferenceValue == null)
            {
                string objectName = "";
                if (owner != null)
                {
                    objectName += owner.name;
                    objectName += owner.GetType().Name;
                }
                string path = AssetPaths.variables;
                string name = $"{objectName}{StringFormatter.CapitalizeFirst(property.name)}";

                ScriptableObject newObject = ScriptableObjectFactory.Create<T>(path, name);
                variable.objectReferenceValue = newObject;

                FieldInfo valueField = newObject.GetType().GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic);
                if (constantField != null) valueField?.SetValue(newObject, constantField.GetValue(reference));
            }

            useConstant.boolValue = false;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty constant = property.FindPropertyRelative("constant");
            SerializedProperty variable = property.FindPropertyRelative("variable");
            SerializedProperty useConstant = property.FindPropertyRelative("useConstant");

            float buttonWidth = position.width - position.width * 0.95f;
            Rect useConstantButtonPosition = new(
                position.x + position.width * 0.4f,
                position.y,
                buttonWidth,
                position.height
            );
            Rect assignVariableButtonPosition = new(
                position.x + position.width * 0.946f + 2f,
                position.y,
                buttonWidth,
                position.height
            );

            Rect fieldPosition = new(
                position.x + position.width * 0.45f + 2,
                position.y,
                position.width - position.width * 0.506f,
                position.height
            );

            EditorGUI.LabelField(position, label);

            if (useConstant.boolValue)
            {
                ConstantField(fieldPosition, constant);
            }
            else
            {
                VariableField(fieldPosition, variable);
            }

            string useConstantButtonText = useConstant.boolValue ? "C" : "V";
            if (GUI.Button(useConstantButtonPosition, useConstantButtonText)) useConstant.boolValue = !useConstant.boolValue;

            bool assignVariableButton = GUI.Button(assignVariableButtonPosition, "+");
            if (assignVariableButton)
            {
                if (variable.objectReferenceValue == null)
                {
                    AssignVariable(property);
                }
                else
                {
                    EditorGUIUtility.PingObject(variable.objectReferenceValue);
                }
            }
        }
    }
}