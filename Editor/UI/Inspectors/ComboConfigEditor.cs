using System.Collections.Generic;
using LavaLeak.Combo.Editor.Config;
using UnityEditor;
using UnityEngine;

namespace LavaLeak.Combo.Editor.UI.Inspectors
{
    [CustomEditor(typeof(ComboConfig))]
    public class ComboConfigEditor : UnityEditor.Editor
    {
        private static void AddNewTask(Object target)
        {
            var tasksList = new List<ComboTaskConfig>(ComboConfig.Instance.tasksConfig)
                {new ComboTaskConfig()};
            Undo.RecordObject(ComboConfig.Instance, "Add Combo Task Config");
            ComboConfig.Instance.tasksConfig = tasksList.ToArray();
            EditorUtility.SetDirty(target);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Render(serializedObject, target);
            serializedObject.ApplyModifiedProperties();
        }

        public static void Render(SerializedObject obj, Object target)
        {
            var logo = Resources.Load<Texture2D>(Paths.logo);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Box(logo, GUIStyle.none);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(obj.FindProperty("logLevel"));

            var tasks = obj.FindProperty("tasksConfig");

            for (var i = 0; i < tasks.arraySize; i++)
            {
                var property = tasks.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(property);
            }

            if (GUILayout.Button("Add New Task", GUILayout.Height(30)))
            {
                AddNewTask(target);
            }
        }
    }
}