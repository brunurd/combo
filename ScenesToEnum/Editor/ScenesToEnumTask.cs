using System.Collections.Generic;
using System.IO;
using System.Text;
using LavaLeak.Combo.Editor.Task;
using UnityEngine;

namespace LavaLeak.ComboTasks.ScenesToEnum.Editor
{
    [ComboTask]
    public class ScenesToEnumTask : IComboMultipleFilesTask
    {
        private static readonly string SCENES_DIRECTORY = Path.Combine("Assets", "Scripts", "ScenesAssembly");
        private static readonly string FILE_NAME = Path.Combine(SCENES_DIRECTORY, "Scenes.cs");
        private static readonly string JSON_FILE_NAME = Path.Combine(SCENES_DIRECTORY, "ScenesNames.json");
        private static readonly string ASSEMBLY_FILE_NAME = Path.Combine(SCENES_DIRECTORY, "ScenesAssembly.asmdef");

        public const string ASSEMBLY_CONTENT = "{\t\"name\": \"ScenesAssembly\",\n\t\"references\": [],\n\t\"includePlatforms\": [],\n\t\"excludePlatforms\": [],\n\t\"allowUnsafeCode\": false,\n\t\"overrideReferences\": false,\n\t\"precompiledReferences\": [],\n\t\"autoReferenced\": true,\n\t\"defineConstraints\": [],\n\t\"versionDefines\": [],\n\t\"noEngineReferences\": false\n}";
        
        public string SearchPattern => "*.unity";
        public string Description => "Attach a scene name to a specific enum declaration.";

        private string ToEnumName(string name)
        {
            name = name
                .Replace('-', ' ')
                .Replace('_', ' ');

            var words = name.Split(' ');
            var newName = "";

            foreach (var word in words)
            {
                for (var index = 0; index < word.Length; index++)
                {
                    if (word[index] == ' ')
                    {
                        continue;
                    }

                    if (index == 0)
                    {
                        newName += char.ToUpper(word[index]);
                        continue;
                    }

                    newName += word[index];
                }
            }

            return newName;
        }

        private void CreateScenesEnum(TaskFileInputData[] input, bool deleteTask)
        {
            if (!Directory.Exists(SCENES_DIRECTORY))
            {
                Directory.CreateDirectory(SCENES_DIRECTORY);
            }

            ScenesData scenesData = File.Exists(JSON_FILE_NAME) ? 
                JsonUtility.FromJson<ScenesData>(File.ReadAllText(JSON_FILE_NAME)) : 
                new ScenesData{ names = new List<string>()};
            
            if (!File.Exists(ASSEMBLY_FILE_NAME))
            {
                File.WriteAllText(ASSEMBLY_FILE_NAME, ASSEMBLY_CONTENT);
            }
            
            var builder = new StringBuilder();
            builder.AppendLine("public enum Scenes {");

            foreach (var fileInputData in input)
            {
                var name = ToEnumName(fileInputData.fileName);
                
                if (deleteTask && scenesData.names.Contains(name))
                {
                    scenesData.names.Remove(name);
                }
                else if (!scenesData.names.Contains(name))
                {
                    scenesData.names.Add(name);
                }
            }

            foreach (var name in scenesData.names)
            {
                builder.AppendLine($"\t{name},");
            }

            builder.AppendLine("}");

            File.WriteAllText(JSON_FILE_NAME, JsonUtility.ToJson(scenesData));
            File.WriteAllText(FILE_NAME, builder.ToString());
        }
        
        public void OnCreateOrUpdateMultipleFiles(TaskFileInputData[] input)
        {
            CreateScenesEnum(input, false);
        }

        public void OnDeleteMultipleFiles(TaskFileInputData[] input)
        {
            CreateScenesEnum(input, true);
        }
    }
}