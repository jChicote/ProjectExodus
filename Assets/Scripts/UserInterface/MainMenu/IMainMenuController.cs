using ProjectExodus.Management.GameStateManager;
using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.UserInterface.MainMenu
{

    public interface IMainMenuController: IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseMainMenuController(
            IGameStateManager gameStateManager, 
            IUserInterfaceController userInterfaceController);
        
        #endregion Methods

    }

}