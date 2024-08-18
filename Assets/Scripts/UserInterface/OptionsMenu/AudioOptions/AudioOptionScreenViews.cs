using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu.AudioOptions
{

    public class AudioOptionScreenViews
    {
        
        #region - - - - - - Constructors - - - - - -

        public AudioOptionScreenViews(
            Button audioOptionTabButton,
            Button muteButton,
            Slider environmentFXVolumeSlider,
            Slider gameMusicVolumeSlider,
            Slider masterVolumeSlider,
            Slider soundFXVolumeSlider,
            Slider uiVolumeSlider)
        {
            this.AudioOptionTabButton = audioOptionTabButton;
            this.MuteButton = muteButton;
            this.EnvironmentFXVolumeSlider = environmentFXVolumeSlider;
            this.GameMusicVolumeSlider = gameMusicVolumeSlider;
            this.MasterVolumeSlider = masterVolumeSlider;
            this.SoundFXVolumeSlider = soundFXVolumeSlider;
            this.UIVolumeSlider = uiVolumeSlider;
        }

        #endregion Constructors
        
        #region - - - - - - Properties - - - - - -

        public Button AudioOptionTabButton { get; private set; }
        
        public Button MuteButton { get; private set; }
        
        public Slider EnvironmentFXVolumeSlider { get; private set; }
        
        public Slider GameMusicVolumeSlider { get; private set; }
        
        public Slider MasterVolumeSlider { get; private set; }
        
        public Slider SoundFXVolumeSlider { get; private set; }
        
        public Slider UIVolumeSlider { get; private set; }

        #endregion Properties

    }

}