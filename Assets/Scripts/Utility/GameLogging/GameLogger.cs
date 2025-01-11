using System.Linq;
using UnityEngine;

namespace ProjectExodus.Utility.GameLogging
{

    public static class GameLogger
    {

        #region - - - - - - Methods - - - - - -

        public static void Log(string message)
        {
            #if UNITY_EDITOR
                Debug.Log($"[LOG]: {message}");
            #endif
        }

        public static void LogWarning(string message)
        {
            #if UNITY_EDITOR
                Debug.LogWarning($"[WARNING]: {message}");
            #endif
        }

        public static void LogError(string message)
        {
            #if UNITY_EDITOR
                Debug.LogError($"[ERROR]: {message}");
            #endif
        }

        #endregion Methods
  
        #region - - - - - - Custom Logging Methods - - - - - -

        public static void Log(params (string paramName, object value)[] parameters)
        {
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.Log(string.Join(", ", parameters.Select(p => $"{p.paramName}: {p.value}")));
            #endif
        }

        #endregion Custom Logging Methods
        
    }

}