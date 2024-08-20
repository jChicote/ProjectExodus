using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using ProjectExodus.UserInterface.LoadingScreen;

namespace ProjectExodus.Management.UserInterfaceManager
{

    public interface IUserInterfaceManager
    {

        #region - - - - - - Properties - - - - - -

        ILoadingScreenController LoadingScreenController { get; }

        IUserInterfaceScreenStateManager UserInterfaceScreenStateManager { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        void InitialiseUserInterfaceManager();

        #endregion Methods

    }

}