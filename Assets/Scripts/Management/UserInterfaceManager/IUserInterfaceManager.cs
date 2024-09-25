using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.Management.UserInterfaceManager
{

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