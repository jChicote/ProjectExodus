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
            destination.ProfileImage = source.SelectedProfileImage;
        }

        private static void MapEditGameSlotViewModelToUpdateGameSaveInputPort(
            EditGameSlotViewModel source,
            UpdateGameSaveInputPort destination)
        {
            destination.GameSaveName = source.DisplayName;
            destination.SelectedProfileImage = source.SelectedProfileImage;
        }

        private static void MapGameSaveModelToGameSaveSlotDto(
            GameSaveModel source,
            GameSaveSlotDto destination)
        {
            destination.DisplayName = source.GameSaveName;
            destination.ProfileImage = source.ProfileImage.Image;
        }

        private static void MapGameSaveSlotDtoToGameSaveModel(
            GameSaveSlotDto source,
            GameSaveModel destination)
        {
            destination.GameSaveName = source.DisplayName;
            destination.ProfileImage.Image = source.ProfileImage;
        }

        #endregion Methods
  
    }

}