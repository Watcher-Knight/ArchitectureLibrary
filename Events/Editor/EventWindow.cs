using UnityEngine;
using UnityEditor;

namespace ArchitectureLibrary
{
    public class EventWindow : EditorWindow
    {
        private bool addTag = false;
        private static bool tagFoldout = false;
        private int tagEdit = -1;
        string newTagName = "NewTag";
        [MenuItem("Architecture Library/Event Settings")]
        private static void ShowWindow()
        {
            var window = GetWindow<EventWindow>();
            window.titleContent = new GUIContent("Event Window");
            window.Show();
        }

        private void OnGUI()
        {
            tagFoldout = EditorGUILayout.Foldout(tagFoldout, "Event Tags");

            if (tagFoldout)
            {
                int index = 0;
                foreach (EventTag tag in EventTagEditor.GetTags())
                {
                    GUILayout.BeginHorizontal();

                    if (tagEdit == -1)
                    {
                        EditorGUILayout.LabelField($"Tag #{index + 1}:", tag.Name);
                        if (GUILayout.Button("Edit"))
                        {
                            tagEdit = index;
                            newTagName = tag.Name;
                        }
                    }
                    else
                    {
                        if (tagEdit == index)
                        {
                            EditorGUILayout.LabelField($"Tag #{index + 1}");
                            newTagName = EditorGUILayout.TextField(newTagName);
                            if (GUILayout.Button("Apply"))
                            {
                                EventTagEditor.EditTag(index, newTagName);
                                tagEdit = -1;
                            }
                            if (GUILayout.Button("Cancel")) tagEdit = -1;
                            if (EventTagEditor.GetTags().Length > 1 && GUILayout.Button("Delete"))
                            {
                                EventTagEditor.DeleteTag(tag);
                                tagEdit = -1;
                            }
                        }
                        else
                        {
                            EditorGUILayout.LabelField($"Tag #{index + 1}:", tag.Name);
                        }
                    }

                    GUILayout.EndHorizontal();

                    index += 1;
                }

                if (tagEdit == -1)
                {
                    if (!addTag)
                    {
                        if (GUILayout.Button("Add New")) addTag = true;
                    }
                    else
                    {
                        newTagName = EditorGUILayout.TextField(newTagName);


                        GUILayout.BeginHorizontal();

                        if (GUILayout.Button("Add"))
                        {
                            EventTagEditor.CreateTag(newTagName);
                            addTag = false;
                            newTagName = "NewTag";
                        }
                        if (GUILayout.Button("Cancel"))
                        {
                            addTag = false;
                            newTagName = "NewTag";
                        }

                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}