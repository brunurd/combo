using LavaLeak.Combo.Editor.Config;
using UnityEditor;

namespace LavaLeak.Combo.Editor
{
    [InitializeOnLoad]
    public static class ComboManager
    {
        static ComboManager()
        {
            Paths.Initialize();
            ComboConfig.Instance.ExecuteTasks(false);
        }
    }
}