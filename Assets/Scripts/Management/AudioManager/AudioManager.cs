using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.Management.AudioManager
{

    /// <summary>
    /// Responsible for high-level management actions of audio components.
    /// </summary>
    public class AudioManager : MonoBehaviour, IAudioManager, IAudioControls
    {

        #region - - - - - - Fields - - - - - -

        [Header("Audio Components")]
        [SerializeField] private AudioSource m_AudioSource;

        #endregion Fields
  
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            AudioManager[] _AudioManager = Object.FindObjectsByType<AudioManager>(FindObjectsSortMode.None);
            if (_AudioManager.Length > 1)
                Debug.LogError($"Multiple {nameof(AudioManager)}s detected. " +
                               $"Only one {nameof(AudioManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Events - - - - - -

        void IAudioControls.OnPause() 
            => this.m_AudioSource.Pause();

        void IAudioControls.OnPlay() 
            => this.m_AudioSource.Play();

        void IAudioControls.OnStop() 
            => Debug.LogWarning("[Warning] - Behavior is not implemented.");

        void IAudioControls.OnSetVolume()
            => Debug.LogWarning("[Warning] - Behavior is not implemented.");

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        void IAudioManager.InitialiseAudioManager() 
            => Debug.Log("AudioManager initialised."); // Temp debug only

        void IAudioControls.SetAudioClip(AudioClip audioClip)
            => this.m_AudioSource.resource = audioClip;

        #endregion Methods

    }

}