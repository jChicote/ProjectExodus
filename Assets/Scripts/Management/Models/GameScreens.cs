using ProjectExodus.UserInterface.MainMenu;

namespace ProjectExodus.Management.Models
{

    public class GameScreens
    {
        
        #region - - - - - - Fields - - - - - -

        public IMainMenuController MainMenuController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameScreens(IMainMenuController mainMenuController) 
            => this.MainMenuController = mainMenuController;

        #endregion Constructors
        
    }

}