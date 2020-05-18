namespace LavaLeak.Combo.Editor.Task
{
    /// <summary>
    /// A task to run over every single file.
    /// </summary>
    public interface IComboSingleFileTask : IComboTask
    {
        /// <summary>
        /// Execute a task for a single file.
        /// On create or update file.
        /// </summary>
        /// <param name="input"></param>
        void OnSingleFile(TaskFileInputData input);
    }
}