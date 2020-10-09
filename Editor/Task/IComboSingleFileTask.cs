namespace Combo.Editor.Task
{
    /// <summary>
    /// A task to run over every single file.
    /// </summary>
    public interface IComboSingleFileTask : IComboTask
    {
        /// <summary>
        /// Execute a task for a created or updated single file.
        /// </summary>
        /// <param name="input"></param>
        void OnCreateOrUpdateSingleFile(TaskFileInputData input);

        /// <summary>
        /// Execute a task for a deleted single file.
        /// </summary>
        /// <param name="input"></param>
        void OnDeleteSingleFile(TaskFileInputData input);
    }
}