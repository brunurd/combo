using System;
using LavaLeak.Combo.Editor.Config;
using UnityEngine;

namespace LavaLeak.Combo.Editor.Logging
{
    public static class Logger
    {
        public static void Log(string message)
        {
            if ((ComboConfig.Instance.logLevel & LogLevel.Log) == LogLevel.Log)
            {
                Debug.Log($"[Combo] {message}");
            }
        }

        public static void LogWarning(string message)
        {
            if ((ComboConfig.Instance.logLevel & LogLevel.Warning) == LogLevel.Warning)
            {
                Debug.LogWarning($"[Combo] {message}");
            }
        }

        public static void LogError(string message)
        {
            if ((ComboConfig.Instance.logLevel & LogLevel.Error) == LogLevel.Error)
            {
                Debug.LogError($"[Combo] {message}");
            }
        }

        internal static void InputDirectoryDontExists(string directoryName)
        {
            LogWarning($"The input directory \"{directoryName}\" don't exists yet...");
        }

        internal static void MultipleFilesTaskStarted(string taskName, string searchPattern, string path)
        {
            Log(
                $"The Combo task \"{taskName}\" was started for all the files with \"{searchPattern}\" in \"{path}\".");
        }

        internal static void MultipleFilesTaskFinished(string taskName, string searchPattern, string path)
        {
            Log(
                $"The Combo task \"{taskName}\" was successfully finished for all the files with \"{searchPattern}\" in \"{path}\".");
        }

        internal static void MultipleFilesTaskFailed(string taskName, string searchPattern, string path, Exception e)
        {
            LogError(
                $"The Combo task \"{taskName}\" was failed for all the files with \"{searchPattern}\" in \"{path}\". Error: {e.Message}");
        }

        internal static void SingleFileTaskStart(string taskName, string filePath)
        {
            Log($"The Combo task \"{taskName}\" was started for the file {filePath}.");
        }

        internal static void SingleFileTaskFinished(string taskName, string filePath)
        {
            Log($"The Combo task \"{taskName}\" was successfully finished for the file \"{filePath}\".");
        }

        internal static void SingleFileTaskFailed(string taskName, string filePath, Exception e)
        {
            LogError($"The Combo task \"{taskName}\" was failed for the file \"{filePath}\". Error: {e.Message}");
        }

        internal static void UnfoundedTaskWithGuid(string guid)
        {
            LogError($"No task with the guid: {guid}");
        }

        internal static void UnfoundedRegisteredTaskWithName(string name)
        {
            Logger.LogError($"Can't find the registered task with the name: {name}");
        }
    }
}