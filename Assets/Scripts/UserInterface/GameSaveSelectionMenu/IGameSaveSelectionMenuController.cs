using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public interface IGameSaveSelectionMenuController
    {

        #region - - - - - - Methods - - - - - -

        void InitializeGameSelectionMenuController(
            IDataContext dataContext, 
            IGameSaveFacade gameSaveFacade, 
            IObjectMapper objectMapper);

        IScreenStateController GetScreenController();

        #endregion Methods

    }

}