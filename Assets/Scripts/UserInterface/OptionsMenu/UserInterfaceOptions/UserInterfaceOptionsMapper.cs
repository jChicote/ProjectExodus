using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions
{

    public class UserInterfaceOptionsMapper
    {

        #region - - - - - - Constructors - - - - - -

        public UserInterfaceOptionsMapper(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister
                .AddMappingAction<UserInterfaceOptionViewModel, UpdateGameOptionsInputPort>(
                    MapUserInterfaceOptionViewModelToInputPort);
            objectMapperRegister
                .AddMappingAction<GameOptionsModel, UserInterfaceOptionViewModel>(
                    MapGameOptionsToUserInterfaceOptionViewModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapUserInterfaceOptionViewModelToInputPort(
            UserInterfaceOptionViewModel source,
            UpdateGameOptionsInputPort destination) 
            => destination.IsHUDVisible = source.IsHUDVisible;

        private static void MapGameOptionsToUserInterfaceOptionViewModel(
            GameOptionsModel source,
            UserInterfaceOptionViewModel destination)
            => destination.IsHUDVisible = source.IsHUDVisible;

        #endregion Methods
        
    }

}