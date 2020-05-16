using LavaLeak.Combo.Editor.Config;
using UnityEditor;

namespace LavaLeak.Combo.Editor
{
    public class AssetsWatcher : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            Paths.Initialize();
            ComboConfig.Instance.ExecuteTasks();
        }
    }
}