using System.Collections.Generic;
using LavaLeak.Combo.Editor.Config;
using LavaLeak.Combo.Editor.Task;
using UnityEditor;
using UnityEngine;

namespace LavaLeak.Combo.Editor.UI.Drawers
{
    [CustomPropertyDrawer(typeof(ComboTaskConfig))]
    public class ComboTaskConfigDrawer : PropertyDrawer
    {
        private SerializedProperty _property;

        private static GUIContent[] ComboTasksNames
        {
            get
            {
                var contents = new List<GUIContent> {new GUIContent("None")};

                foreach (var task in RegisteredTasks.Instance.Tasks)
                {
                    contents.Add(new GUIContent(task.name));
                }

                return contents.ToArray();
            }
        }

        private ComboTaskConfig GetTaskConfig()
        {
            var task = new ComboTaskConfig();

            if (_property == null)
            {
                return task;
            }

            task.name = _property.FindPropertyRelative("name").stringValue;
            task.guid = _property.FindPropertyRelative("guid").stringValue;
            task.classFullName = _property.FindPropertyRelative("classFullName").stringValue;
            task.searchPattern = _property.FindPropertyRelative("searchPattern").stringValue;
            task.path = _property.FindPropertyRelative("path").stringValue;

            return task;
        }

        private static void RemoveTask(SerializedProperty property)
        {
            var guid = property.FindPropertyRelative("guid").stringValue;
            Undo.RecordObject(ComboConfig.Instance, "Remove Combo Config");
            ComboConfig.Instance.RemoveTaskByGuid(guid);
            EditorUtility.SetDirty(ComboConfig.Instance);
        }

        private void TaskPopUp(SerializedProperty property, ComboTaskConfig taskConfig)
        {
            var names = ComboTasksNames;
            var index = 0;

            for (var i = 0; i < names.Length; i++)
            {
                if (names[i].text == taskConfig.classFullName)
                {
                    index = i;

                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            index = EditorGUILayout.Popup(new GUIContent("Task Type: "), index, names);

            if (!EditorGUI.EndChangeCheck())
            {
                return;
            }

            if (index == 0)
            {
                Undo.RecordObject(ComboConfig.Instance, "Change Task Type");
                property.FindPropertyRelative("classFullName").stringValue = string.Empty;
                property.FindPropertyRelative("searchPattern").stringValue = string.Empty;
                EditorUtility.SetDirty(ComboConfig.Instance);

                return;
            }

            var name = names[index].text;

            Undo.RecordObject(ComboConfig.Instance, "Change Task Type");
            property.FindPropertyRelative("classFullName").stringValue = name;

            property.FindPropertyRelative("searchPattern").stringValue =
                RegisteredTasks.Instance.GetRegisteredTaskByName(name).task.SearchPattern;
            EditorUtility.SetDirty(ComboConfig.Instance);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _property = property;

            var taskConfig = GetTaskConfig();
            var taskIndex = ComboConfig.Instance.GetTaskIndexByGuid(taskConfig.guid);
            var boxRect = EditorGUILayout.BeginVertical(GUI.skin.box);

            var title = string.IsNullOrEmpty(taskConfig.name)
                ? $"Task {taskIndex} - {taskConfig.classFullName}"
                : taskConfig.name;

            position.height = boxRect.height;
            GUI.Box(position, new GUIContent(title));

            property.FindPropertyRelative("name").stringValue =
                EditorGUILayout.TextField(new GUIContent("Name: "), taskConfig.name);

            TaskPopUp(property, taskConfig);

            if (!string.IsNullOrEmpty(taskConfig.classFullName))
            {
                property.FindPropertyRelative("path").stringValue =
                    EditorGUILayout.TextField(new GUIContent("Input path: Assets/"), taskConfig.path);

                EditorGUILayout.SelectableLabel($"Search Pattern: {taskConfig.searchPattern}");
            }

            if (GUILayout.Button("Remove"))
            {
                RemoveTask(property);
            }

            EditorGUILayout.EndVertical();
        }
    }
}