using UnityEngine;
using UnityEngine.UI;

namespace ProjectExodus.UserInterface.OptionsMenu.AudioOptions
{

    public class AudioOptionScreenViews
    {
        
        #region - - - - - - Properties - - - - - -

        public GameObject AudioOptionsContentGroup { get; private set; }
        
        public Button AudioOptionTabButton { get; private set; }
        
        public Button MuteButton { get; private set; }
        
        public Slider EnvironmentFXVolumeSlider { get; private set; }
        
        public Slider GameMusicVolumeSlider { get; private set; }
        
        public Slider MasterVolumeSlider { get; private set; }
        
        public Slider SoundFXVolumeSlider { get; private set; }
        
        public Slider UIVolumeSlider { get; private set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public AudioOptionScreenViews(
            GameObject audioOptionsContentGroup,
            Button audioOptionTabButton,
            Button muteButton,
            Slider environmentFXVolumeSlider,
            Slider gameMusicVolumeSlider,
            Slider masterVolumeSlider,
            Slider soundFXVolumeSlider,
            Slider uiVolumeSlider)
        {
            this.AudioOptionsContentGroup = audioOptionsContentGroup;
            this.AudioOptionTabButton = audioOptionTabButton;
            this.MuteButton = muteButton;
            this.EnvironmentFXVolumeSlider = environmentFXVolumeSlider;
            this.GameMusicVolumeSlider = gameMusicVolumeSlider;
            this.MasterVolumeSlider = masterVolumeSlider;
            this.SoundFXVolumeSlider = soundFXVolumeSlider;
            this.UIVolumeSlider = uiVolumeSlider;
        }

        #endregion Constructors

    }

}