using ProjectExodus.Management.AudioManager;
using UnityEngine;

namespace ProjectExodus.Tests.TestHarness.TestAudioManagerHarness
{

    public class Test_AudioManagerHarness : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public AudioManager AudioManager;
        public AudioSource AudioSource;
        public AudioClip TestClip;

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
            if (this.AudioSource.isPlaying)
                Return Debug.Log("[Success] Audio can now pause.]");
        }

        public void OnPlay_PlayingAudioManager_AnyActiveSoundtracksOrSoundsArePlaying()
        {
            // Arrange
            
            // Act
            
            // Assert
            
        }

        public void SetAudioClip_AddingNewClipForTheAudioManagerToPlay_NewAudioClipIsPlaying()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        #endregion Tests

    }

}