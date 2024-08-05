using ProjectExodus.UserInterface.MainMenu;
using UnityEngine;

namespace ProjectExodus.Management.UserInterfaceManager
{
    
    public interface IUserInterfaceManager
    {

        #region - - - - - - Properties - - - - - -

        IMainMenuController MainMenuController { get; }

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        void InitialiseUserInterfaceManager();

        void OpenMenu(GUIScreen guiScreen);

        #endregion Methods

    }

    public enum GUIScreen
    {
        MainMenu,
        GameplayScreen
    }

}