
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.Models;

namespace ProjectExodus.Management.UserInterfaceScreenStatesManager
{

    public interface IUserInterfaceScreenStateManager
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseUserInterfaceScreenStatesManager(GameScreens gameScreens);
        
        void OpenScreen(UIScreenType uiScreenType);

        void OpenPreviousScreen();

        #endregion Methods

    }

}