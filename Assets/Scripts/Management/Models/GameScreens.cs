using ProjectExodus.UserInterface.MainMenu;
using ProjectExodus.UserInterface.OptionsMenu;

namespace ProjectExodus.Management.Models
{

    public class GameScreens
    {
        
        #region - - - - - - Fields - - - - - -

        public IMainMenuController MainMenuController;
        public IOptionsMenuController OptionsMenuController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameScreens(IMainMenuController mainMenuController, IOptionsMenuController optionsMenuController)
        {
            this.MainMenuController = mainMenuController;
            this.OptionsMenuController = optionsMenuController;
        }

        #endregion Constructors
        
    }

}