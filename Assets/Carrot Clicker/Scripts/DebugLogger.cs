using UnityEngine;

// This class provides a simple logging utility that can be enabled or disabled based on the build configuration.
// It uses preprocessor directives to conditionally compile the logging methods only in the editor or development builds.
// This allows for cleaner production builds without debug logs, while still providing useful logging during development.
// The `EnableDebug` flag can be set to true or false to control whether debug logs are printed.
// Usage example:
// DebugLogger.Log("This is a debug message.");
// DebugLogger.LogWarning("This is a warning message.");
// DebugLogger.LogError("This is an error message.");

public static class DebugLogger
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public static bool EnableDebug = true;
#else
    public static bool EnableDebug = false;
#endif

    public static void Log(string message)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (EnableDebug)
            Debug.Log(message);
#endif
    }

    public static void LogWarning(string message)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (EnableDebug)
            Debug.LogWarning(message);
#endif
    }

    public static void LogError(string message)
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (EnableDebug)
            Debug.LogError(message);
#endif
    }
}
