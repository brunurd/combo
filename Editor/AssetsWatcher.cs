using Combo.Editor.Config;
using UnityEditor;

namespace Combo.Editor
{
    public class AssetsWatcher : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            Paths.Initialize();
            ComboConfig.Instance.ExecuteTasks(true, deletedAssets);
        }
    }
}