using System;
using System.IO;
using FileNotFoundException = Combo.Editor.Exceptions.FileNotFoundException;

namespace Combo.Editor.Cache
{
    [Serializable]
    public struct FileCacheData
    {
        public string path;
        public long size;

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

                return fileCache.path == path && fileCache.size == size;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}