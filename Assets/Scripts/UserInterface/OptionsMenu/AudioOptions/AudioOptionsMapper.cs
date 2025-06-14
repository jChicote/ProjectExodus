using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.UserInterface.OptionsMenu.AudioOptions
{

    public class AudioOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public AudioOptionsMapper(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister
                .AddMappingAction<AudioOptionViewModel, UpdateGameOptionsInputPort>(
                    MapAudioOptionsViewModelToInputPort);
            objectMapperRegister
                .AddMappingAction<GameOptionsModel, AudioOptionViewModel>(
                    MapGameOptionsToAudioOptionsViewModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapAudioOptionsViewModelToInputPort(
            AudioOptionViewModel source,
            UpdateGameOptionsInputPort destination)
        {
            destination.EnvironmentFXVolume = source.EnvironmentFXVolume;
            destination.GameMusicVolume = source.GameMusicVolume;
            destination.MasterVolume = source.MasterVolume;
            destination.SoundFXVolume = source.SoundFXVolume;
            destination.UIVolume = source.UIVolume;
        }

        private static void MapGameOptionsToAudioOptionsViewModel(
            GameOptionsModel source,
            AudioOptionViewModel destination)
        {
            destination.EnvironmentFXVolume = source.EnvironmentFXVolume;
            destination.GameMusicVolume = source.GameMusicVolume;
            destination.MasterVolume = source.MasterVolume;
            destination.SoundFXVolume = source.SoundFXVolume;
            destination.UIVolume = source.UIVolume;
        }

        #endregion Methods
  
    }

}