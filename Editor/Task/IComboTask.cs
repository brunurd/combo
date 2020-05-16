namespace LavaLeak.Combo.Editor.Task
{
    /// <summary>
    /// A task to run in the Combo flow.
    /// </summary>
    public interface IComboTask
    {
        /// <summary>
        /// A pattern to search in the files in the watched directory.
        /// </summary>
        string SearchPattern { get; }
    }
}