using System.IO;
using UnityEngine;

namespace LavaLeak.Combo.Editor.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Save a serializable data direct to json file in path.
        /// </summary>
        /// <param name="path">The path with filename and extension.</param>
        /// <param name="data">A serializable data type instance.</param>
        public static void SaveJson(string path, object data)
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Deserialize a json file to a data instance.
        /// </summary>
        /// <param name="path">The path with filename and extension.</param>
        /// <typeparam name="T">A serializable data type.</typeparam>
        /// <returns>The deserialized data instance.</returns>
        public static T LoadJson<T>(string path)
        {
            var contents = File.ReadAllText(path);

            return JsonUtility.FromJson<T>(contents);
        }

        /// <summary>
        /// Deserialize a json file to a data instance or create it.
        /// </summary>
        /// <param name="path">The path with filename and extension.</param>
        /// <typeparam name="T">A serializable data type.</typeparam>
        /// <returns>The deserialized data instance.</returns>
        public static T LoadJsonSafely<T>(string path)
        {
            if (File.Exists(path))
            {
                return LoadJson<T>(path);
            }

            var data = default(T);

            SaveJson(path, data);

            return data;
        }
    }
}