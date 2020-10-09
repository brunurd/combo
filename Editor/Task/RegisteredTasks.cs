using System;
using System.Collections.Generic;
using UnityEngine;
using Logger = Combo.Editor.Logging.Logger;

namespace Combo.Editor.Task
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

                _instance = new RegisteredTasks();

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

                                break;
                            }
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Get a RegisteredTask instance in the tasks array by it full name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RegisteredTask GetRegisteredTaskByName(string name)
        {
            foreach (var task in tasks)
            {
                if (task.fullName == name)
                {
                    return task;
                }
            }

            Logger.UnfoundedRegisteredTaskWithName(name);

            return default;
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
                if (registeredTask.fullName != task.GetType().FullName)
                {
                    tasksList.Add(registeredTask);
                }
            }

            tasks = tasksList.ToArray();
        }
    }
}