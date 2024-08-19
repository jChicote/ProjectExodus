using ProjectExodus.UserInterface.LoadingScreen;
using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;

namespace ProjectExodus.Management.Models
{

    public class GameScreens
    {
        
        #region - - - - - - Properties - - - - - -

        public IMainMenuController MainMenuController { get; private set; }
        public IOptionsMenuController OptionsMenuController { get; private set; }
        public ILoadingScreenController LoadingScreenController { get; private set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public GameScreens(
            IMainMenuController mainMenuController, 
            IOptionsMenuController optionsMenuController,
            ILoadingScreenController loadingScreenController)
        {
            this.MainMenuController = mainMenuController;
            this.OptionsMenuController = optionsMenuController;
            this.LoadingScreenController = loadingScreenController;
        }

        #endregion Constructors
        
    }

}