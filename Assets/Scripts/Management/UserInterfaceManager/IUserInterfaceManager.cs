using ProjectExodus.UserInterface.MainMenu;

namespace ProjectExodus.Management.UserInterfaceManager
{
    
    public interface IUserInterfaceManager
    {

        #region - - - - - - Properties - - - - - -

        IMainMenuController MainMenuController { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitialiseUserInterfaceManager();

        #endregion Methods

    }

}