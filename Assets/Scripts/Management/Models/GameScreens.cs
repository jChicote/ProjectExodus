using ProjectExodus.UserInterface.GameplayHUD;
using ProjectExodus.UserInterface.GameSaveSelectionMenu;
using ProjectExodus.UserInterface.LoadingScreen;
using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;

namespace ProjectExodus.Management.Models
{

    public class GameScreens
    {
        
        #region - - - - - - Properties - - - - - -
        
        public IGameSaveSelectionMenuController GameSaveSelectionMenuController { get; private set;  }

        public IMainMenuController MainMenuController { get; private set; }
        
        public IOptionsMenuController OptionsMenuController { get; private set; }
        
        public ILoadingScreenController LoadingScreenController { get; private set; }
        
        public IGameplayHUDController GameplayHUDController { get; private set; }

        #endregion Properties

        #region - - - - - - Constructors - - - - - -

        public GameScreens(
            IGameSaveSelectionMenuController gameSaveSelectionMenuController,
            IMainMenuController mainMenuController, 
            IOptionsMenuController optionsMenuController,
            ILoadingScreenController loadingScreenController,
            IGameplayHUDController gameplayHUDController)
        {
            this.GameSaveSelectionMenuController = gameSaveSelectionMenuController;
            this.MainMenuController = mainMenuController;
            this.OptionsMenuController = optionsMenuController;
            this.LoadingScreenController = loadingScreenController;
            this.GameplayHUDController = gameplayHUDController;
        }

        #endregion Constructors
        
    }

}