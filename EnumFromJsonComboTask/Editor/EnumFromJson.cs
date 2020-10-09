using System.IO;
using Combo.Editor.Task;
using UnityEditor;
using UnityEngine;

namespace Combo.EnumFromJsonComboTask.Editor
{
    [ComboTask]
    public struct EnumFromJson : IComboSingleFileTask
    {
        public string SearchPattern => "*.json";
        public string Description => "Transform a json file with a enums field to a enum C# script.";

        public void OnCreateOrUpdateSingleFile(TaskFileInputData input)
        {
            var json = JsonUtility.FromJson<JsonFile>(input.contents);
            var name = input.fileName.Capitalize();
            var enumFile = new EnumFile(name, json);
            var directory = Path.Combine("Assets", "Scripts", "Enums");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, $"{name}.cs");
            File.WriteAllText(path, enumFile.ToString());
        }

        public void OnDeleteSingleFile(TaskFileInputData input)
        {
            var name = input.fileName.Capitalize();
            var directory = Path.Combine("Assets", "Scripts", "Enums");
            var path = Path.Combine(directory, $"{name}.cs");

            if (!Directory.Exists(directory))
            {
                return;
            }

            foreach (var file in Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories))
            {
                if (file == path)
                {
                    AssetDatabase.DeleteAsset(path);
                }
            }
        }
    }
}