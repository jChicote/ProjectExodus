using UnityEngine;

namespace ProjectExodus.Management.InputManager
{

    /// <summary>
    /// Responsible for coordinating input bindings and input control management for different hardware.
    /// </summary>
    public class InputManager : MonoBehaviour, IInputManager
    {

        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            InputManager[] _InputManager = Object.FindObjectsByType<InputManager>(FindObjectsSortMode.None);
            if (_InputManager.Length > 1)
                Debug.LogError($"Multiple {nameof(InputManager)}s detected. " +
                               $"Only one {nameof(InputManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IInputManager.InitialiseInputManager()
        {
            Debug.Log("AudioManager initialised."); // Temp debug only
        }

        #endregion Methods
  
    }

}