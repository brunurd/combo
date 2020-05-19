using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEditor;

namespace LavaLeak.Combo.Tests.Editor.Cache
{
    public class CacheDataTests
    {
        private string path;

        [SetUp]
        public void Setup([CallerFilePathAttribute] string path = "")
        {
            this.path = path;
        }
        
        [Test]
        public void GetFilesDataTest()
        {
            Assert.AreEqual(path, "a");
        }
    }
}