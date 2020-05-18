namespace LavaLeak.Combo.Editor.Task
{
    /// <summary>
    /// A task to run once on every files in a task.
    /// </summary>
    public interface IComboMultipleFilesTask : IComboTask
    {
        /// <summary>
        /// Execute a task over multiple files.
        /// </summary>
        /// <param name="input"></param>
        void OnMultipleFiles(TaskFileInputData[] input);
    }
}