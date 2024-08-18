using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions
{

    public class UpdateGameOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public UpdateGameOptionsMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<UpdateGameOptionsInputPort, Entities.GameOptions>(
                    this.MapUpdateGameOptionsInputPortToGameOptions);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapUpdateGameOptionsInputPortToGameOptions(
            UpdateGameOptionsInputPort source,
            Entities.GameOptions destination)
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