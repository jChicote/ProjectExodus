using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.UserInterface.PauseScreen
{

    public interface IPauseScreenPresenter
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(
            IGameStateManager gameStateManager, 
            IPauseController pauseController, 
            IUserInterfaceController userInterfaceController);

        #endregion Initializers

    }

}