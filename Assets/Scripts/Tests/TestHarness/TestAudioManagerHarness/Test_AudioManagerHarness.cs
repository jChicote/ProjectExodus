using ProjectExodus.Management.AudioManager;
using UnityEngine;

namespace ProjectExodus.Tests.TestHarness.TestAudioManagerHarness
{

    public class Test_AudioManagerHarness : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public AudioManager AudioManager;
        public AudioSource AudioSource;
        public AudioClip TestAudioClip;

        private IAudioControls m_AudioControls;

        #endregion Fields

        #region - - - - - - Unity Methods - - - - - -

        private void Start() 
            => this.m_AudioControls = this.AudioManager;

        #endregion Unity Methods

        #region - - - - - - Tests - - - - - -

        public void OnPause_PausingAudioManager_AllSoundsArePaused()
        {
            // Arrange
            
            // Act
            this.m_AudioControls.OnPause();
            
            // Assert
            if (this.AudioSource.resource != null)
                Debug.Log(!this.AudioSource.isPlaying
                        ? "[SUCCESS] Audio are now paused." 
                        : "[FAIL] Audio is still paused.");
            else
                Debug.Log("[FAIL] Cannot pause the audio if not audio clip has been set.");
        }

        public void OnPlay_PlayingAudioManager_AnyActiveSoundtracksOrSoundsArePlaying()
        {
            // Arrange
            
            // Act
            this.m_AudioControls.OnPlay();
            
            // Assert
            Debug.Log(this.AudioSource.isPlaying
                ? "[SUCCESS] Audio is now playing."
                : "[FAIL] Audio is still paused or inactive.");
        }

        public void SetAudioClip_AddingNewClipForTheAudioManagerToPlay_NewAudioClipIsPlaying()
        {
            // Arrange
            
            // Act
            this.m_AudioControls.SetAudioClip(this.TestAudioClip);
            
            // Assert
            Debug.Log(this.AudioSource.resource == this.TestAudioClip
                ? "[SUCCESS] Audio clip has been set."
                : "[FAIL] Audio clip is not found.");
        }

        #endregion Tests

    }

}