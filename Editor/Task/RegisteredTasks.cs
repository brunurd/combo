using System;
using System.Collections.Generic;
using LavaLeak.Combo.Editor.Helpers;
using UnityEngine;
using Logger = LavaLeak.Combo.Editor.Logging.Logger;

namespace LavaLeak.Combo.Editor.Task
{
    [Serializable]
    public struct RegisteredTasks
    {
        [SerializeField]
        private RegisteredTask[] tasks;

        private static bool _instantiated;
        private static RegisteredTasks _instance;

        /// <summary>
        /// Get all the IComboTask implementation with the ComboTaskAttribute.
        /// </summary>
        internal IEnumerable<RegisteredTask> Tasks => tasks;

        /// <summary>
        /// Access the registeredTasks.json data.
        /// </summary>
        public static RegisteredTasks Instance
        {
            get
            {
                if (_instantiated)
                {
                    return _instance;
                }

                _instantiated = true;
                _instance = JsonHelper.LoadJsonSafely<RegisteredTasks>(Paths.registeredTasks);
                Logger.RegisteredTasksLoaded();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        foreach (var _interface in type.GetInterfaces())
                        {
                            var isComboTask = _interface == typeof(IComboTask);

                            var getComboTaskAttribute =
                                type.GetCustomAttributes(typeof(ComboTaskAttribute), true).Length > 0;

                            if (isComboTask && getComboTaskAttribute)
                            {
                                var instance = (IComboTask) Activator.CreateInstance(type);
                                _instance.AddTask(instance);
                                Logger.ComboTaskInitialized(type.Name);

                                break;
                            }
                        }
                    }
                }

                JsonHelper.SaveJson(Paths.registeredTasks, _instance);

                return _instance;
            }
        }

        /// <summary>
        /// Add a IComboTask implementation to the TasksConfig array.
        /// </summary>
        /// <param name="task"></param>
        private void AddTask(IComboTask task)
        {
            if (task == null)
            {
                return;
            }

            if (tasks == null)
            {
                tasks = new RegisteredTask[0];
            }

            var tasksList = new List<RegisteredTask>();
            tasksList.Add(new RegisteredTask(task));

            foreach (var registeredTask in tasks)
            {
                if (registeredTask.name != task.GetType().FullName)
                {
                    tasksList.Add(registeredTask);
                }
            }

            tasks = tasksList.ToArray();
        }
    }
}