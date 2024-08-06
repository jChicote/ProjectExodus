using ProjectExodus.Management.UserInterfaceScreenStatesManager;

namespace ProjectExodus.Management.UserInterfaceManager
{

    public interface IUserInterfaceManager
    {

        #region - - - - - - Properties - - - - - -

        IUserInterfaceScreenStateManager UserInterfaceScreenStateManager { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        void InitialiseUserInterfaceManager();

        #endregion Methods

    }

}