using System.IO;

namespace LavaLeak.Combo.Editor.Task
{
    public struct TaskFileInputData
    {
        public readonly string path;
        public readonly string directoryName;
        public readonly string fileName;
        public readonly string extension;
        public readonly string contents;

        /// <summary>
        /// A file data to be used by a task.
        /// </summary>
        /// <param name="path"></param>
        public TaskFileInputData(string path)
        {
            this.path = path;
            fileName = Path.GetFileNameWithoutExtension(path);
            directoryName = Path.GetDirectoryName(path);
            extension = Path.GetExtension(path);
            contents = File.ReadAllText(path);
        }
    }
}