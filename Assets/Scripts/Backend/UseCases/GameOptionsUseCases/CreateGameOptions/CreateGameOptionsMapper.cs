using System;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions
{

    public class CreateGameOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public CreateGameOptionsMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister.
                    AddMappingAction<CreateGameOptionsInputPort, GameOptions>(
                        this.MapCreateGameOptionsInputPortToGameOptions);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapCreateGameOptionsInputPortToGameOptions(
            CreateGameOptionsInputPort source,
            GameOptions destination)
        {
            if (source == null || destination == null)
                throw new ArgumentException($"Mapping objects of type: {typeof(CreateGameOptionsInputPort)}" +
                                            $" and {typeof(GameOptions)}, cannot both or either be null.");

            destination.ID = new Guid();
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