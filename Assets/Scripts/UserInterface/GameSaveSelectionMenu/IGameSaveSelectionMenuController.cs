using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.UserInterface.GameSaveSelectionMenu
{

    public interface IGameSaveSelectionMenuController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitializeGameSelectionMenuController(IGameSaveFacade gameSaveFacade, IObjectMapper objectMapper);

        #endregion Methods

    }

}