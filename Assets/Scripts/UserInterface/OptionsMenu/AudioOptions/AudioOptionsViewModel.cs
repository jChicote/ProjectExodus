namespace ProjectExodus.UserInterface.OptionsMenu.AudioOptions
{

    public class AudioOptionsViewModel
    {

        #region - - - - - - Fields - - - - - -

        private readonly AudioOptionScreenViews _mAudioOptionViews;
        private readonly OptionsMenuContentViews m_OptionsMenuContentGroups;

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

        public AudioOptionsViewModel(
            AudioOptionScreenViews audioOptionScreenViews, 
            OptionsMenuContentViews optionsMenuContentGroups)
        {
            this._mAudioOptionViews = audioOptionScreenViews;
            this.m_OptionsMenuContentGroups = optionsMenuContentGroups;
            
            audioOptionScreenViews.AudioOptionTabButton.onClick.AddListener(this.OnShowAudioOptions);
            
            audioOptionScreenViews.EnvironmentFXVolumeSlider.onValueChanged.AddListener(this.OnEnvironmentVolumeChanged);
            audioOptionScreenViews.GameMusicVolumeSlider.onValueChanged.AddListener(this.OnGameMusicVolumeChanged);
            audioOptionScreenViews.MasterVolumeSlider.onValueChanged.AddListener(this.OnMasterVolumeChanged);
            audioOptionScreenViews.SoundFXVolumeSlider.onValueChanged.AddListener(this.OnSoundFXVolumeChanged);
            audioOptionScreenViews.UIVolumeSlider.onValueChanged.AddListener(this.OnUIVolumeChanged);
            audioOptionScreenViews.MuteButton.onClick.AddListener(this.OnToggleMute);
        }

        #endregion Constructors

        #region - - - - - - Events - - - - - -
        
        private void OnShowAudioOptions()
        {
            this._mAudioOptionViews.AudioOptionsContentGroup.SetActive(true);

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