namespace ProjectExodus.UserInterface.OptionsMenu.OptionsAudio
{

    public class OptionsAudioViewModel
    {

        #region - - - - - - Fields - - - - - -

        private OptionsAudioScreenViews m_OptionsAudioViews;
        private OptionsMenuContentViews m_OptionsMenuContentGroups;

        #endregion Fields
  
        #region - - - - - - Properties - - - - - -

        public float EnvironmentFXVolume { get; set; }
        
        public bool IsMuted { get; set; }
        
        public float GameMusicVolume { get; set; }
        
        public float MasterVolume { get; set; }
        
        public float SoundFXVolume { get; set; }
        
        public float UIVolume { get; set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public OptionsAudioViewModel(
            OptionsAudioScreenViews optionsAudioScreenViews, 
            OptionsMenuContentViews optionsMenuContentGroups)
        {
            this.m_OptionsAudioViews = optionsAudioScreenViews;
            this.m_OptionsMenuContentGroups = optionsMenuContentGroups;
            
            optionsAudioScreenViews.AudioOptionTabButton.onClick.AddListener(this.OnShowAudioOptions);
            
            optionsAudioScreenViews.EnvironmentFXVolumeSlider.onValueChanged.AddListener(this.OnEnvironmentVolumeChanged);
            optionsAudioScreenViews.GameMusicVolumeSlider.onValueChanged.AddListener(this.OnGameMusicVolumeChanged);
            optionsAudioScreenViews.MasterVolumeSlider.onValueChanged.AddListener(this.OnMasterVolumeChanged);
            optionsAudioScreenViews.SoundFXVolumeSlider.onValueChanged.AddListener(this.OnSoundFXVolumeChanged);
            optionsAudioScreenViews.UIVolumeSlider.onValueChanged.AddListener(this.OnUIVolumeChanged);
            optionsAudioScreenViews.MuteButton.onClick.AddListener(this.OnToggleMute);
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -
        
        private void OnShowAudioOptions()
        {
            this.m_OptionsAudioViews.AudioOptionsContentGroup.SetActive(true);

            this.m_OptionsMenuContentGroups.GraphicsOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentGroups.InputOptionsContentGroup.SetActive(false);
            this.m_OptionsMenuContentGroups.UserInterfaceOptionsContentGroup.SetActive(false);
        }

        private void OnEnvironmentVolumeChanged(float sliderValue) 
            => this.EnvironmentFXVolume = sliderValue;

        private void OnGameMusicVolumeChanged(float sliderValue) 
            => this.GameMusicVolume = sliderValue;

        private void OnMasterVolumeChanged(float sliderValue) 
            => this.MasterVolume = sliderValue;

        private void OnSoundFXVolumeChanged(float sliderValue) 
            => this.SoundFXVolume = sliderValue;

        private void OnUIVolumeChanged(float sliderValue) 
            => this.UIVolume = sliderValue;

        private void OnToggleMute()
            => this.IsMuted = !this.IsMuted;

        #endregion Events
  
    }

}