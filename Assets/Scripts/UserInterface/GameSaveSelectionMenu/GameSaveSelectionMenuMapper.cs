using System;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu.EditGameSlotModal;
using UserInterface.GameSaveSelectionMenu.Dtos;

namespace UserInterface.GameSaveSelectionMenu
{

    public class GameSaveSelectionMenuMapper
    {

        #region - - - - - - Constructors - - - - - -

        public GameSaveSelectionMenuMapper(IObjectMapperRegister objectMapperRegister)
        {
            objectMapperRegister
                .AddMappingAction<EditGameSlotViewModel, CreateGameSaveInputPort>(
                    MapEditGameSlotViewModelToCreateGameSaveInputPort);
            objectMapperRegister
                .AddMappingAction<EditGameSlotViewModel, UpdateGameSaveInputPort>(
                    MapEditGameSlotViewModelToUpdateGameSaveInputPort);
            objectMapperRegister
                .AddMappingAction<GameSaveModel, GameSaveSlotDto>(
                    MapGameSaveModelToGameSaveSlotDto);
            objectMapperRegister
                .AddMappingAction<GameSaveSlotDto, GameSaveModel>(
                    MapGameSaveSlotDtoToGameSaveModel);
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private static void MapEditGameSlotViewModelToCreateGameSaveInputPort(
            EditGameSlotViewModel source,
            CreateGameSaveInputPort destination)
        {
            destination.CompletionProgress = 0f;
            destination.GameSlotDisplayIndex = source.DisplayIndex;
            destination.LastAccessedDate = DateTime.Now;
            destination.GameSaveName = source.DisplayName;
            destination.ProfileImageID = source.SelectedProfileImage.ID;
        }

        private static void MapEditGameSlotViewModelToUpdateGameSaveInputPort(
            EditGameSlotViewModel source,
            UpdateGameSaveInputPort destination)
        {
            destination.ID = source.ID;
            destination.GameSaveName = source.DisplayName;
            destination.SelectedProfileImageID = source.SelectedProfileImage.ID;
        }

        private static void MapGameSaveModelToGameSaveSlotDto(
            GameSaveModel source,
            GameSaveSlotDto destination)
        {
            destination.ID = source.ID;
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