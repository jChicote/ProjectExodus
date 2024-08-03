using UnityEngine;

namespace ProjectExodus.Management.AudioManager
{

    /// <summary>
    /// Responsible for high-level management actions of audio components.
    /// </summary>
    public class AudioManager : MonoBehaviour, IAudioManager
    {

        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            AudioManager[] _AudioManager = Object.FindObjectsByType<AudioManager>(FindObjectsSortMode.None);
            if (_AudioManager.Length > 1)
                Debug.LogError($"Multiple {nameof(AudioManager)}s detected. " +
                               $"Only one {nameof(AudioManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        void IAudioManager.InitialiseAudioManager()
        {
            Debug.Log("AudioManager initialised."); // Temp debug only
        }

        #endregion Methods

    }

}