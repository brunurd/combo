using System.Collections.Generic;
using System.IO;
using Combo.Editor.Config;
using UnityEditor;
using UnityEngine;

namespace Combo.Editor
{
    [InitializeOnLoad]
    public static class ComboManager
    {
        static ComboManager()
        {
            Paths.Initialize();
            ComboConfig.Instance.ExecuteTasks(false);
        }

        /// <summary>
        /// Inject a pre-defined task config to the ComboConfig.
        /// </summary>
        /// <param name="taskConfig"></param>
        public static void InjectTask(ComboTaskConfig taskConfig)
        {
            foreach (var ownTaskConfig in ComboConfig.Instance.tasksConfig)
            {
                if (ownTaskConfig.name == taskConfig.name && ownTaskConfig.injected)
                {
                    return;
                }
            }

            taskConfig.injected = true;
            var tasksList = new List<ComboTaskConfig>(ComboConfig.Instance.tasksConfig) {taskConfig};
            ComboConfig.Instance.tasksConfig = tasksList.ToArray();
        }
    }
}