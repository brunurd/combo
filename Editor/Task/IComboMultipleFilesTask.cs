namespace Combo.Editor.Task
{
    /// <summary>
    /// A task to run once on every files in a task.
    /// </summary>
    public interface IComboMultipleFilesTask : IComboTask
    {
        /// <summary>
        /// Execute a task over created or updated multiple files.
        /// </summary>
        /// <param name="input"></param>
        void OnCreateOrUpdateMultipleFiles(TaskFileInputData[] input);

        /// <summary>
        /// Execute a task over deleted multiple files.
        /// </summary>
        /// <param name="input"></param>
        void OnDeleteMultipleFiles(TaskFileInputData[] input);
    }
}