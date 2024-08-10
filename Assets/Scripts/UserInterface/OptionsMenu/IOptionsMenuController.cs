using ProjectExodus.GameLogic.Models;

namespace ProjectExodus.UserInterface.OptionsMenu
{

    public interface IOptionsMenuController : IScreenStateController
    {

        #region - - - - - - Methods - - - - - -

        void InitialiseOptionsMenu(GameOptions gameOptions);

        #endregion Methods

    }

}