using System;

namespace Combo.Editor.Exceptions
{
    public class FileCacheNotFoundException : Exception
    {
        public FileCacheNotFoundException(string path) : base(
            $"File cache not found in the cache list for the path: {path}")
        {
        }
    }
}