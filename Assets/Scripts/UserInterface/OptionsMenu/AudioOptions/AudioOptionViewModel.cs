using UnityEngine;

namespace ProjectExodus.UserInterface.OptionsMenu.AudioOptions
{

    public class AudioOptionViewModel
    {

        #region - - - - - - Fields - - - - - -
        
        private readonly AudioOptionScreenViews m_AudioOptionViews;
        private readonly OptionsMenuContentViews m_OptionsMenuContentGroups;

        private float m_EnvironmentFXVolume;
        private float m_GameMusicVolume;
        private float m_MasterVolume;
        private float m_SoundFXVolume;
        private float m_UIVolume ;

        #endregion Fields
  
        #region - - - - - - Constructors - - - - - -

        public AudioOptionViewModel(
            AudioOptionScreenViews audioOptionScreenViews, 
            OptionsMenuContentViews optionsMenuContentGroups)
        {
            this.m_AudioOptionViews = audioOptionScreenViews;
            this.m_OptionsMenuContentGroups = optionsMenuContentGroups;
            
            this.m_AudioOptionViews.AudioOptionTabButton.onClick.AddListener(this.OnShowAudioOptions);
            
            // Bind events from views
            this.m_AudioOptionViews.EnvironmentFXVolumeSlider.onValueChanged.AddListener(this.OnEnvironmentVolumeChanged);
            this.m_AudioOptionViews.GameMusicVolumeSlider.onValueChanged.AddListener(this.OnGameMusicVolumeChanged);
            this.m_AudioOptionViews.MasterVolumeSlider.onValueChanged.AddListener(this.OnMasterVolumeChanged);
            this.m_AudioOptionViews.SoundFXVolumeSlider.onValueChanged.AddListener(this.OnSoundFXVolumeChanged);
            this.m_AudioOptionViews.UIVolumeSlider.onValueChanged.AddListener(this.OnUIVolumeChanged);
            this.m_AudioOptionViews.MuteButton.onClick.AddListener(this.OnToggleMute);
        }

        #endregion Constructors
        
        #region - - - - - - Properties - - - - - -

        public float EnvironmentFXVolume
        {
            get => this.m_EnvironmentFXVolume;
            set
            {
                if (Mathf.Approximately(this.m_EnvironmentFXVolume, value)) return;
                this.m_EnvironmentFXVolume = value;
                this.m_AudioOptionViews.EnvironmentFXVolumeSlider.value = value;
            }
        }

        private bool IsMuted { get; set; }

        public float GameMusicVolume
        {
            get => this.m_GameMusicVolume;
            set
            {
                if (Mathf.Approximately(this.m_GameMusicVolume, value)) return;
                this.m_GameMusicVolume = value;
                this.m_AudioOptionViews.GameMusicVolumeSlider.value = value;
            }
        }

        public float MasterVolume
        {
            get => this.m_MasterVolume;
            set
            {
                if (Mathf.Approximately(this.m_MasterVolume, value)) return;
                this.m_MasterVolume = value;
                this.m_AudioOptionViews.MasterVolumeSlider.value = value;
            }
        }

        public float SoundFXVolume
        {
            get => this.m_SoundFXVolume;
            set
            {
                if (Mathf.Approximately(this.m_SoundFXVolume, value)) return;
                this.m_SoundFXVolume = value;
                this.m_AudioOptionViews.SoundFXVolumeSlider.value = value;
            }
        }

        public float UIVolume
        {
            get => this.m_UIVolume;
            set
            {
                if (Mathf.Approximately(this.m_UIVolume, value)) return;
                this.m_UIVolume = value;
                this.m_AudioOptionViews.UIVolumeSlider.value = value;
            }
        }

        #endregion Properties

        #region - - - - - - Events - - - - - -
        
        private void OnShowAudioOptions()
        {
            this.m_OptionsMenuContentGroups.AudioOptionsContentGroup.SetActive(true);

            this.m_OptionsMenuContentGroups.GraphicsOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentGroups.InputOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentGroups.UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnEnvironmentVolumeChanged(float sliderValue) 
            => this.m_EnvironmentFXVolume = sliderValue;

        private void OnGameMusicVolumeChanged(float sliderValue) 
            => this.m_GameMusicVolume = sliderValue;

        private void OnMasterVolumeChanged(float sliderValue) 
            => this.m_MasterVolume = sliderValue;

        private void OnSoundFXVolumeChanged(float sliderValue) 
            => this.m_SoundFXVolume = sliderValue;

        private void OnUIVolumeChanged(float sliderValue) 
            => this.m_UIVolume = sliderValue;

        private void OnToggleMute()
            => this.IsMuted = !this.IsMuted;

        #endregion Events
        
    }

}