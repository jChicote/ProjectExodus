using ProjectExodus.Management.Enumeration;

namespace ProjectExodus.UserInterface.Controllers
{

    public interface IUserInterfaceController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseUserInterfaceController();
        
        void OpenScreen(UIScreenType uiScreenType);

        void OpenPreviousScreen();

        bool TryGetGUIControllers(out object _Controller);

        #endregion Methods

    }

}