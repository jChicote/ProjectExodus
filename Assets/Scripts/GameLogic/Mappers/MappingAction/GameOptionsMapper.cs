using System;
using ProjectExodus.GameLogic.Dtos;
using ProjectExodus.GameLogic.Models;

namespace ProjectExodus.GameLogic.Mappers.GameOptionsMapper
{

    public class GameOptionsMappingAction
    {

        #region - - - - - - Constructors - - - - - -

        public GameOptionsMappingAction(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister.AddMappingAction<GameOptions, GameOptionsDto>(this.MapGameOptionsToDto);
            objectMapperRegister.AddMappingAction<GameOptionsDto, GameOptions>(this.MapDtoToGameOptions);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapGameOptionsToDto(GameOptions source, GameOptionsDto destination)
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

        private void MapDtoToGameOptions(GameOptionsDto source, GameOptions destination)
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