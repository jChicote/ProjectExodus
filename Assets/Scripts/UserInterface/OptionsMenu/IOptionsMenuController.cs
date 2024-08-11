using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public interface IOptionsMenuController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseOptionsMenu(
            GameOptions gameOptions, 
            IObjectMapper mapper, 
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager);

        #endregion Methods

    }

}