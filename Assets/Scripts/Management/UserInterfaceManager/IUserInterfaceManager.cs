using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.Management.UserInterfaceManager
{

    // TODO: Remove the use of an interface for the UserInterfaceManager. It should be a singleton.
    public interface IUserInterfaceManager
    {

        #region - - - - - - Methods - - - - - -

        /// <remarks>
        /// This method is expensive, reserve for only transitions between scenes.
        /// </remarks>
        IUserInterfaceController GetTheActiveUserInterfaceController();

        #endregion Methods

    }

}