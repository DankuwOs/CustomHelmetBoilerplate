using UnityEngine;

namespace CustomHelmetBoilerplate;

public class Logger
{
    private static readonly string ModName = "CustomHelmetBoilerplate";

    public static void Log(object message)
    {
        Debug.Log($"[{ModName}] [INFO]: {message}");
    }

    public static void LogWarn(object obj)
    {
        Debug.LogWarning($"[{ModName}] [WARN]: {obj}");
    }

    public static void LogError(object message)
    {
        Debug.LogError($"[{ModName}] [ERROR]: {message}");
    }
}