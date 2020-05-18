using System;

namespace LavaLeak.Combo.Editor.Task
{
    [Serializable]
    public struct RegisteredTask
    {
        public string fullName;
        public string shortName;

        [NonSerialized]
        public readonly IComboTask task;

        /// <summary>
        /// A struct to contains a IComboTask implementation.
        /// </summary>
        /// <param name="task"></param>
        public RegisteredTask(IComboTask task)
        {
            this.task = task;
            var type = task.GetType();
            fullName = type.FullName;
            shortName = type.Name;
        }
    }
}