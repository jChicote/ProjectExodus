using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.Mappers
{

    // TODO: Move mappers to different use cases
    public class GameOptionMappings
    {
        
        #region - - - - - - Constructors - - - - - -

        public GameOptionMappings(IObjectMapperRegister objectMapperRegister)
        {
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -
        
        // private void MapEntityToGameOptions(GameOptionsEntity source, GameOptions destination)
        // {
        //     if (source == null || destination == null)
        //         throw new ArgumentException($"Mapping objects of type: {source.GetType()}" +
        //                                     $" and {destination.GetType()}, cannot both or either be null.");
        //     
        //     destination.EnvironmentFXVolume = source.EnvironmentFXVolume;
        //     destination.IsMuted = source.IsMuted;
        //     destination.GameMusicVolume = source.GameMusicVolume;
        //     destination.MasterVolume = source.MasterVolume;
        //     destination.SoundFXVolume = source.SoundFXVolume;
        //     destination.UIVolume = source.UIVolume;
        //     destination.IsHUDVisible = source.IsHUDVisible;
        //     destination.DisplayOption = (DisplayOption)source.DisplayOption;
        //     destination.DisplayHeight = source.DisplayHeight;
        //     destination.DisplayWidth = source.DisplayWidth;
        // }

        #endregion Methods
        
    }

}