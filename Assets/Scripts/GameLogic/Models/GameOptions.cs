using System;

namespace ProjectExodus.GameLogic.Models
{

    [Serializable]
    public class GameOptions
    {

        #region - - - - - - Properties - - - - - -

        // ---------------------------------------
        // Audio Options
        // ---------------------------------------
        
        public int EnvironmentFXVolume { get; set; }
        
        public bool IsMuted { get; set; }
        
        public int GameMusicVolume { get; set; }
        
        public int MasterVolume { get; set; }
        
        public int SoundFXVolume { get; set; }
        
        public int UIVolume { get; set; }
        
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
    
    public enum DisplayOption
    {
        Fullscreen,
        Windowed
    }

}