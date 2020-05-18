using System.IO;

namespace LavaLeak.Combo.Editor
{
    internal static class Paths
    {
        private const string CONFIG_ASSET_NAME = "Config.asset";
        private static readonly string _combo = Path.Combine("Assets", "Editor", "Combo");

        internal static readonly string config = Path.Combine(_combo, CONFIG_ASSET_NAME);
        internal static readonly string cache = Path.Combine(_combo, ".cache");
        internal static readonly string logo = "Combo-Logo-Banner_CC-BY-ND_by-Bruno-Araujo_Inspector";

        /// <summary>
        /// Generate paths if don't exists.
        /// </summary>
        internal static void Initialize()
        {
            if (!Directory.Exists(_combo))
            {
                Directory.CreateDirectory(_combo);
                File.WriteAllText(Path.Combine(_combo, ".gitignore"), "Editor/Combo/.cache/\n*.combo-cache.json");
            }

            if (!Directory.Exists(cache))
            {
                Directory.CreateDirectory(cache);
            }
        }
    }
}