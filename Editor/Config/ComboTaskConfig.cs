using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using LavaLeak.Combo.Editor.Cache;
using LavaLeak.Combo.Editor.Exceptions;
using LavaLeak.Combo.Editor.Helpers;
using LavaLeak.Combo.Editor.Task;
using UnityEngine;
using Logger = LavaLeak.Combo.Editor.Logging.Logger;

namespace LavaLeak.Combo.Editor.Config
{
    [Serializable]
    public sealed class ComboTaskConfig
    {
        public string name;
        public string guid;
        public string classFullName;
        public string searchPattern;
        public string path;
        public string description;

        [SerializeField] internal bool injected;

        public ComboTaskConfig()
        {
            guid = Guid.NewGuid().ToString();
            injected = false;
        }

        /// <summary>
        /// Check if a value match with the configured search pattern.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SearchPatternMatch(string value)
        {
            var regex = new Regex(searchPattern.Replace(".", "\\.").Replace("*", ".*"));
            return regex.Match(value).Success;
        }

        /// <summary>
        /// Update the cache and execute the task with the updated files.
        /// </summary>
        internal void UpdateCacheAndExecute()
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var input = FilterUpdatedFiles();
            var completePath = Path.Combine("Assets", path);

            if (input.Length <= 0)
            {
                return;
            }

            foreach (var registeredTask in RegisteredTasks.Instance.Tasks)
            {
                var type = registeredTask.task.GetType();

                if (registeredTask.fullName != classFullName)
                {
                    break;
                }

                foreach (var iInterface in type.GetInterfaces())
                {
                    if (iInterface == typeof(IComboMultipleFilesTask))
                    {
                        var multipleFilesTask = (IComboMultipleFilesTask) registeredTask.task;
                        var taskName = registeredTask.task.GetType().Name;

                        Logger.MultipleFilesTaskStarted(taskName, searchPattern, completePath);

                        try
                        {
                            multipleFilesTask.OnCreateOrUpdateMultipleFiles(input);
                            Logger.MultipleFilesTaskFinished(taskName, searchPattern, completePath);
                        }
                        catch (Exception e)
                        {
                            Logger.MultipleFilesTaskFailed(taskName, searchPattern, completePath, e);
                        }

                        break;
                    }

                    if (iInterface == typeof(IComboSingleFileTask))
                    {
                        var singleFileTask = (IComboSingleFileTask) registeredTask.task;
                        var taskName = registeredTask.task.GetType().Name;

                        foreach (var updatedFile in input)
                        {
                            Logger.SingleFileTaskStart(taskName, updatedFile.path);

                            try
                            {
                                singleFileTask.OnCreateOrUpdateSingleFile(updatedFile);
                                Logger.SingleFileTaskFinished(taskName, updatedFile.path);
                            }
                            catch (Exception e)
                            {
                                Logger.SingleFileTaskFailed(taskName, updatedFile.path, e);
                            }
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Execute the task over the deleted assets.
        /// </summary>
        /// <param name="deletedAssets"></param>
        internal void ExecuteDeletedEvent(string[] deletedAssets)
        {
            var completePath = Path.Combine("Assets", path);
            var input = new List<TaskFileInputData>();

            if (deletedAssets.Length <= 0)
            {
                return;
            }

            foreach (var deletedPath in deletedAssets)
            {
                var inputFile = new TaskFileInputData(deletedPath);
                if (SearchPatternMatch(inputFile.path))
                {
                    input.Add(inputFile);
                }
            }

            foreach (var registeredTask in RegisteredTasks.Instance.Tasks)
            {
                var type = registeredTask.task.GetType();

                if (registeredTask.fullName != classFullName)
                {
                    break;
                }

                foreach (var iInterface in type.GetInterfaces())
                {
                    if (iInterface == typeof(IComboMultipleFilesTask))
                    {
                        var multipleFilesTask = (IComboMultipleFilesTask) registeredTask.task;
                        var taskName = registeredTask.task.GetType().Name;

                        Logger.MultipleFilesTaskStarted(taskName, searchPattern, completePath);

                        try
                        {
                            multipleFilesTask.OnDeleteMultipleFiles(input.ToArray());
                            Logger.MultipleFilesTaskFinished(taskName, searchPattern, completePath);
                        }
                        catch (Exception e)
                        {
                            Logger.MultipleFilesTaskFailed(taskName, searchPattern, completePath, e);
                        }

                        break;
                    }

                    if (iInterface == typeof(IComboSingleFileTask))
                    {
                        var singleFileTask = (IComboSingleFileTask) registeredTask.task;
                        var taskName = registeredTask.task.GetType().Name;

                        foreach (var updatedFile in input)
                        {
                            Logger.SingleFileTaskStart(taskName, updatedFile.path);

                            try
                            {
                                singleFileTask.OnDeleteSingleFile(updatedFile);
                                Logger.SingleFileTaskFinished(taskName, updatedFile.path);
                            }
                            catch (Exception e)
                            {
                                Logger.SingleFileTaskFailed(taskName, updatedFile.path, e);
                            }
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Return only the updated or new files data input.
        /// </summary>
        /// <returns></returns>
        private TaskFileInputData[] FilterUpdatedFiles()
        {
            TaskFileInputData[] inputFiles = new TaskFileInputData[0];
            var completePath = Path.Combine("Assets", path);

            if (!Directory.Exists(completePath))
            {
                Logger.InputDirectoryDontExists(completePath);

                return inputFiles;
            }

            var cachePath = Path.Combine(Paths.cache, $"{guid}.combo-cache.json");
            var newCache = new CacheData(completePath, searchPattern);
            var cacheFileExists = File.Exists(cachePath);
            var force = !cacheFileExists;
            var oldCache = cacheFileExists ? JsonHelper.LoadJson<CacheData>(cachePath) : newCache;
            JsonHelper.SaveJson(cachePath, newCache);

            if (force)
            {
                inputFiles = new TaskFileInputData[newCache.Files.Length];

                for (var index = 0; index < newCache.Files.Length; index++)
                {
                    inputFiles[index] = new TaskFileInputData(newCache.Files[index].path);
                }

                return inputFiles;
            }

            var updated = new List<FileCacheData>();

            foreach (var fileCache in newCache.Files)
            {
                try
                {
                    var oldFileCache = oldCache[fileCache.path];

                    if (!oldFileCache.Equals(fileCache))
                    {
                        updated.Add(fileCache);
                    }
                }
                catch (FileCacheNotFoundException)
                {
                    updated.Add(fileCache);
                }
            }

            inputFiles = new TaskFileInputData[updated.Count];

            for (var index = 0; index < updated.Count; index++)
            {
                inputFiles[index] = new TaskFileInputData(updated[index].path);
            }

            return inputFiles;
        }
    }
}