using ProjectExodus.GameLogic.Enumeration;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public class OptionsMenuViewModel
    {

        #region - - - - - - Properties - - - - - -

        // ---------------------------------------
        // Audio Options
        // ---------------------------------------
        
        public float EnvironmentFXVolume { get; set; }
        
        public bool IsMuted { get; set; }
        
        public float GameMusicVolume { get; set; }
        
        public float MasterVolume { get; set; }
        
        public float SoundFXVolume { get; set; }
        
        public float UIVolume { get; set; }
        
        // ---------------------------------------
        // User-Interface Options
        // ---------------------------------------
        
        public bool IsHUDVisible { get; set; }
        
        // ---------------------------------------
        // Graphics Options
        // ---------------------------------------

        public DisplayOption DisplayOption { get; set; }
        
        public int DisplayHeight { get; set; }
        
        public int DisplayWidth { get; set; }
        
        #endregion Properties
  
    }

}