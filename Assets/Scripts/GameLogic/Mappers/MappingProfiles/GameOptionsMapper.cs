using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Enumeration;

namespace ProjectExodus.GameLogic.Mappers.MappingProfiles
{

    public class GameOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public GameOptionsMapper(IObjectMapperRegister objectMapperRegister) 
            => objectMapperRegister
                .AddMappingAction<GameOptions, GameOptionsModel>(
                    MapGameOptionsToGameOptionsModel);

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapGameOptionsToGameOptionsModel(GameOptions source, GameOptionsModel destination)
        {
            destination.ID = source.ID;
            destination.EnvironmentFXVolume = source.EnvironmentFXVolume;
            destination.IsMuted = source.IsMuted;
            destination.GameMusicVolume = source.GameMusicVolume;
            destination.MasterVolume = source.MasterVolume;
            destination.SoundFXVolume = source.SoundFXVolume;
            destination.UIVolume = source.UIVolume;
            destination.IsHUDVisible = source.IsHUDVisible;
            destination.DisplayOption = (DisplayOption)source.DisplayOption;
            destination.DisplayHeight = source.DisplayHeight;
            destination.DisplayWidth = source.DisplayWidth;
        }

        #endregion Methods
  
    }

}