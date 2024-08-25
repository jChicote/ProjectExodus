using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;

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

        #endregion Methods
  
    }

}