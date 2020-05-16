using System.IO;
using LavaLeak.Combo.Editor.Config;
using UnityEditor;

namespace LavaLeak.Combo.Editor
{
    [InitializeOnLoad]
    public static class ComboManager
    {
        private static FileSystemWatcher _watcher;

        static ComboManager()
        {
            Paths.Initialize();
            ComboConfig.Instance.ExecuteTasks();
        }
    }
}