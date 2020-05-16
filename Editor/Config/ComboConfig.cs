using System.IO;
using LavaLeak.Combo.Editor.Logging;
using UnityEditor;
using UnityEngine;

namespace LavaLeak.Combo.Editor.Config
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
        public void ExecuteTasks()
        {
            if (tasksConfig == null)
            {
                tasksConfig = new ComboTaskConfig[0];
            }
            
            foreach (var taskConfig in tasksConfig)
            {
                taskConfig.UpdateCacheAndExecute();
            }

            AssetDatabase.Refresh();
        }
    }
}