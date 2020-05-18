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

        private static RegisteredTask[] ComboTasksNames
        {
            get
            {
                var contents = new List<RegisteredTask> {new RegisteredTask {shortName = "None"}};

                foreach (var task in RegisteredTasks.Instance.Tasks)
                {
                    contents.Add(task);
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
            task.injected = _property.FindPropertyRelative("injected").boolValue;
            task.classFullName = _property.FindPropertyRelative("classFullName").stringValue;
            task.searchPattern = _property.FindPropertyRelative("searchPattern").stringValue;
            task.path = _property.FindPropertyRelative("path").stringValue;
            task.description = _property.FindPropertyRelative("description").stringValue;

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
            var registeredTasks = ComboTasksNames;
            var index = 0;
            var guiContents = new List<GUIContent>();

            foreach (var task in registeredTasks)
            {
                guiContents.Add(new GUIContent(task.shortName));
            }

            for (var i = 0; i < registeredTasks.Length; i++)
            {
                if (registeredTasks[i].fullName == taskConfig.classFullName)
                {
                    index = i;
                    break;
                }
            }

            var contentNames = guiContents.ToArray();

            EditorGUI.BeginChangeCheck();

            index = EditorGUILayout.Popup(new GUIContent("Task Type: "), index, contentNames);

            if (!EditorGUI.EndChangeCheck())
            {
                return;
            }

            if (index == 0)
            {
                Undo.RecordObject(ComboConfig.Instance, "Change Task Type");
                property.FindPropertyRelative("classFullName").stringValue = string.Empty;
                property.FindPropertyRelative("searchPattern").stringValue = string.Empty;
                property.FindPropertyRelative("description").stringValue = string.Empty;
                EditorUtility.SetDirty(ComboConfig.Instance);

                return;
            }

            var registeredTask = RegisteredTasks.Instance.GetRegisteredTaskByName(registeredTasks[index].fullName);

            Undo.RecordObject(ComboConfig.Instance, "Change Task Type");
            property.FindPropertyRelative("classFullName").stringValue = registeredTasks[index].fullName;

            property.FindPropertyRelative("searchPattern").stringValue =
                registeredTask.task.SearchPattern;

            property.FindPropertyRelative("description").stringValue =
                registeredTask.task.Description;

            EditorUtility.SetDirty(ComboConfig.Instance);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _property = property;

            var taskConfig = GetTaskConfig();
            var taskIndex = ComboConfig.Instance.GetTaskIndexByGuid(taskConfig.guid);
            var boxRect = EditorGUILayout.BeginVertical(GUI.skin.box);

            var title = string.IsNullOrEmpty(taskConfig.name)
                ? $"Task {taskIndex}"
                : taskConfig.name;

            var titleContent = new GUIContent(title);
            titleContent.tooltip = taskConfig.description;

            position.height = boxRect.height;
            GUI.Box(position, titleContent);

            if (taskConfig.injected)
            {
                EditorGUILayout.HelpBox(
                    "This is a pre-defined externally injected task configuration.\nIf you edit or remove you lose the task default behaviour.",
                    MessageType.Warning);
            }

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