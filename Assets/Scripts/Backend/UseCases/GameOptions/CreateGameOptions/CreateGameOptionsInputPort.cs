using ProjectExodus.GameLogic.Enumeration;

namespace ProjectExodus.Backend.UseCases.GameOptions.CreateGameOptions
{

    public class CreateGameOptionsInputPort
    {
        
        #region - - - - - - Properties - - - - - -

        public DisplayOption DisplayOption { get; set; }
        
        public int DisplayHeight { get; set; }
        
        public int DisplayWidth { get; set; }

        public float EnvironmentFXVolume { get; set; }
        
        public float GameMusicVolume { get; set; }
        
        public bool IsHUDVisible { get; set; }
        
        public bool IsMuted { get; set; }
        
        public float MasterVolume { get; set; }
        
        public float SoundFXVolume { get; set; }
        
        public float UIVolume { get; set; }

        #endregion Properties
        
    }

}