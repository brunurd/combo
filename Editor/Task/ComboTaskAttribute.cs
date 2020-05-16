using System;

namespace LavaLeak.Combo.Editor.Task
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComboTaskAttribute : Attribute
    {
        /// <summary>
        /// Add a IComboTask implementation to the Combo flow.
        /// </summary>
        public ComboTaskAttribute() : base()
        {
        }
    }
}