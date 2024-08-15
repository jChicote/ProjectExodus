using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public interface IOptionsMenuController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseOptionsMenu(
            IDataContext dataContext,
            GameOptionsModel gameOptionsModel, 
            IGameOptionsFacade gameOptionsFacade,
            IObjectMapper mapper, 
            IUserInterfaceScreenStateManager userInterfaceScreenStateManager);

        #endregion Methods

    }

}