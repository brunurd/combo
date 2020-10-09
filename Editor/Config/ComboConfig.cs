using System.Collections.Generic;
using System.IO;
using Combo.Editor.Logging;
using UnityEditor;
using UnityEngine;

namespace Combo.Editor.Config
{
    internal sealed class ComboConfig : ScriptableObject
    {
        [SerializeField]
        internal LogLevel logLevel = LogLevel.Log | LogLevel.Warning | LogLevel.Error;

        /// <summary>
        /// Return the tasks configuration from Config.asset.
        /// </summary>
        [SerializeField]
        internal ComboTaskConfig[] tasksConfig;

        private static ComboConfig _instance;

        /// <summary>
        /// Property to get the ComboConfig project Singleton reference.
        /// </summary>
        public static ComboConfig Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                if (!File.Exists(Paths.config))
                {
                    _instance = CreateInstance<ComboConfig>();
                    AssetDatabase.CreateAsset(_instance, Paths.config);
                }
                else
                {
                    _instance = AssetDatabase.LoadAssetAtPath<ComboConfig>(Paths.config);
                }

                return _instance;
            }
        }

        /// <summary>
        /// Execute all config tasks.
        /// </summary>
        public void ExecuteTasks(bool refresh = true, string[] deletedAssets = null)
        {
            if (tasksConfig == null)
            {
                tasksConfig = new ComboTaskConfig[0];
            }

            foreach (var taskConfig in tasksConfig)
            {
                taskConfig.UpdateCacheAndExecute();

                if (deletedAssets != null)
                {
                    taskConfig.ExecuteDeletedEvent(deletedAssets);
                }
            }

            if (refresh)
            {
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Get a Combo Task Config index by it guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public int GetTaskIndexByGuid(string guid)
        {
            for (var index = 0; index < tasksConfig.Length; index++)
            {
                if (tasksConfig[index].guid == guid)
                {
                    return index;
                }
            }

            Logging.Logger.UnfoundedTaskWithGuid(guid);

            return -1;
        }

        /// <summary>
        /// Remove a Combo Task Config by it guid.
        /// </summary>
        /// <param name="guid"></param>
        public void RemoveTaskByGuid(string guid)
        {
            var tasksList = new List<ComboTaskConfig>(tasksConfig);

            for (var index = 0; index < tasksList.Count; index++)
            {
                var task = tasksList[index];

                if (task.guid != guid)
                {
                    continue;
                }

                tasksList.Remove(task);

                break;
            }

            tasksConfig = tasksList.ToArray();
        }
    }
}