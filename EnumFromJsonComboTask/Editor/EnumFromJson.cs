using System.IO;
using LavaLeak.Combo.Editor.Task;
using UnityEngine;

namespace LavaLeak.Combo.EnumFromJsonComboTask.Editor
{
    [ComboTask]
    public struct EnumFromJson : IComboSingleFileTask
    {
        public string SearchPattern => "*.json";
        public string Description => "Transform a json file with a enums field to a enum C# script.";

        public void OnSingleFile(TaskFileInputData input)
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
    }
}