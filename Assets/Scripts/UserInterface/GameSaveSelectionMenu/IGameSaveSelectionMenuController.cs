using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public interface IGameSaveSelectionMenuController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitializeGameSelectionMenuController(
            IDataContext dataContext, 
            IGameSaveFacade gameSaveFacade, 
            IObjectMapper objectMapper);

        #endregion Methods

    }

}