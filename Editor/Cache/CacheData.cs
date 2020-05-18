using System;
using System.IO;
using LavaLeak.Combo.Editor.Exceptions;
using UnityEngine;

namespace LavaLeak.Combo.Editor.Cache
{
    [Serializable]
    public struct CacheData
    {
        [SerializeField]
        private FileCacheData[] files;

        /// <summary>
        /// The cached array of every file in the root path.
        /// </summary>
        public FileCacheData[] Files => files;

        public FileCacheData this[string path]
        {
            get
            {
                foreach (var file in files)
                {
                    if (file.path.Equals(path))
                    {
                        return file;
                    }
                }

                throw new FileCacheNotFoundException(path);
            }
        }

        /// <summary>
        /// Generate a cache from a path root.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern">The file pattern to match on path.</param>
        public CacheData(string path, string searchPattern)
        {
            var paths = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            files = new FileCacheData[paths.Length];

            for (var i = 0; i < paths.Length; i++)
            {
                files[i] = new FileCacheData(paths[i]);
            }
        }
    }
}