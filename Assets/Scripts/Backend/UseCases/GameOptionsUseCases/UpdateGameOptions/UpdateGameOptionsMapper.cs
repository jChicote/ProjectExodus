using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions
{

    public class UpdateGameOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public UpdateGameOptionsMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<UpdateGameOptionsInputPort, GameOptions>(
                    MapUpdateGameOptionsInputPortToGameOptions);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapUpdateGameOptionsInputPortToGameOptions(
            UpdateGameOptionsInputPort source,
            GameOptions destination)
        {
            destination.EnvironmentFXVolume = source.EnvironmentFXVolume;
            destination.IsMuted = source.IsMuted;
            destination.GameMusicVolume = source.GameMusicVolume;
            destination.MasterVolume = source.MasterVolume;
            destination.SoundFXVolume = source.SoundFXVolume;
            destination.UIVolume = source.UIVolume;
            destination.IsHUDVisible = source.IsHUDVisible;
            destination.DisplayOption = (int)source.DisplayOption;
            destination.DisplayHeight = source.DisplayHeight;
            destination.DisplayWidth = source.DisplayWidth;
        }

        #endregion Methods
  
    }

}