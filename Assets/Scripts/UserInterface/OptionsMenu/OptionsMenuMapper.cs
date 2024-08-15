using System;
using ProjectExodus.Backend.UseCases.GameOptions.UpdateGameOptions;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public class OptionsMenuMapper
    {

        #region - - - - - - Constructors - - - - - -

        public OptionsMenuMapper(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister.AddMappingAction<GameOptionsModel, OptionsMenuViewModel>(
                                    this.MapGameOptionsToViewModel);
            objectMapperRegister.AddMappingAction<OptionsMenuViewModel, UpdateGameOptionsInputPort>(
                                    this.MapViewModelToInputPort);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void MapGameOptionsToViewModel(GameOptionsModel source, OptionsMenuViewModel destination)
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

        private void MapViewModelToInputPort(OptionsMenuViewModel source, UpdateGameOptionsInputPort destination)
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