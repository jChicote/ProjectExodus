using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using UserInterface.GameSaveSelectionMenu.Dtos;

namespace UserInterface.GameSaveSelectionMenu
{

    public class GameSaveSelectionMenuMapper
    {

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuMapper(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister
                .AddMappingAction<GameSaveModel, GameSaveSlotViewModel>(
                    MapGameSaveModelToGameSaveSlotViewModel);
            objectMapperRegister
                .AddMappingAction<GameSaveModel, GameSaveSlotDto>(
                    MapGameSaveModelToGameSaveSlotDto);
            objectMapperRegister
                .AddMappingAction<GameSaveSlotDto, GameSaveModel>(
                    MapGameSaveSlotDtoToGameSaveModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapGameSaveModelToGameSaveSlotViewModel(
            GameSaveModel source,
            GameSaveSlotViewModel destination)
        {
            destination.ID = source.ID;
            destination.CompletionProgress = source.CompletionProgress;
            destination.DisplayIndex = source.GameSlotDisplayIndex;
            destination.LastAccessedDate = source.LastAccessedDate;
            destination.GameSaveName = source.GameSaveName;
        }

        private static void MapGameSaveModelToGameSaveSlotDto(
            GameSaveModel source,
            GameSaveSlotDto destination)
        {
            destination.DisplayName = source.GameSaveName;
            destination.ProfileImage = source.ProfileImage;
        }

        private static void MapGameSaveSlotDtoToGameSaveModel(
            GameSaveSlotDto source,
            GameSaveModel destination)
        {
            destination.GameSaveName = source.DisplayName;
            destination.ProfileImage = source.ProfileImage;
        }

        #endregion Methods
  
    }

}