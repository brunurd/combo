using System.Collections.Generic;
using LavaLeak.Combo.Editor.Config;
using LavaLeak.Combo.Editor.ExtensionMethods;
using LavaLeak.Combo.Editor.Logging;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LavaLeak.Combo.Editor.Inspector.ComboConfig
{
    [CustomEditor(typeof(Config.ComboConfig))]
    public class ComboConfigEditor : UnityEditor.Editor
    {
        public void SetLogLevel(LogLevel logLevel)
        {
            var config = (Config.ComboConfig) target;
            Undo.RecordObject(target, "Set ComboConfig.logLevel");
            config.logLevel = logLevel;
            EditorUtility.SetDirty(target);
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            root.StylePadding(20, 0);

            var config = (Config.ComboConfig) target;

            var logLevel = new LogLevelComponent(config.logLevel, SetLogLevel);

            root.Add(logLevel);

            var list = new PropertyField(serializedObject.FindProperty("tasksConfig"));
            root.Add(list);

            var addButton = new Button(() =>
            {
                var tasksList = new List<ComboTaskConfig>(Config.ComboConfig.Instance.tasksConfig)
                    {new ComboTaskConfig()};
                Undo.RecordObject(Config.ComboConfig.Instance, "Add Combo Task Config");
                Config.ComboConfig.Instance.tasksConfig = tasksList.ToArray();
                EditorUtility.SetDirty(target);
            }) {text = "Add New Task"};
            root.Add(addButton);

            return root;
        }
    }
}