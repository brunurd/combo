using System;
using System.IO;
using FileNotFoundException = LavaLeak.Combo.Editor.Exceptions.FileNotFoundException;

namespace LavaLeak.Combo.Editor.Cache
{
    [Serializable]
    public struct FileCacheData
    {
        public string path;
        public long size;
        public DateTime updatedAt;

        /// <summary>
        /// Generate a file cache with specific file state.
        /// Without the file contents.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="System.IO.FileNotFoundException">Throws if the path is invalid.</exception>
        public FileCacheData(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            var fileInfo = new FileInfo(path);

            this.path = path;
            size = fileInfo.Length;
            updatedAt = File.GetLastWriteTime(path);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            try
            {
                var fileCache = (FileCacheData) obj;

                return fileCache.path == path && fileCache.size == size && fileCache.updatedAt.Equals(updatedAt);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}