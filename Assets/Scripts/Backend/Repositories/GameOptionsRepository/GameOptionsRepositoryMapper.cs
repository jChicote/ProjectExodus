using System;
using ProjectExodus.Backend.Entities;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.Repositories.GameOptionsRepository
{

    public class GameOptionsRepositoryMapper
    {
        
        #region - - - - - - Constructors - - - - - -

        public GameOptionsRepositoryMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister.AddMappingAction<GameOptions, GameOptions>(this.MapGameOptionsToGameOptions);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -
        
        private void MapGameOptionsToGameOptions(GameOptions source, GameOptions destination)
        {
            if (source == null || destination == null)
                throw new ArgumentException($"Mapping objects of type: {source.GetType()}" +
                                            $" and {destination.GetType()}, cannot both or either be null.");
            
            destination.EnvironmentFXVolume = source.EnvironmentFXVolume;
            destination.IsMuted = source.IsMuted;
            destination.GameMusicVolume = source.GameMusicVolume;
            destination.MasterVolume = source.MasterVolume;
            destination.SoundFXVolume = source.SoundFXVolume;
            destination.UIVolume = source.UIVolume;
            destination.IsHUDVisible = source.IsHUDVisible;
            destination.DisplayOption = source.DisplayOption;
            destination.DisplayHeight = source.DisplayHeight;
            destination.DisplayWidth = source.DisplayWidth;
        }

        #endregion Methods
        
    }

}