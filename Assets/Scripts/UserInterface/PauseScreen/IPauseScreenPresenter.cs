using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.UserInterface.Controllers;

namespace ProjectExodus.UserInterface.PauseScreen
{

    public interface IPauseScreenPresenter
    {

        #region - - - - - - Initializers - - - - - -

        void Initialize(IPauseController pauseController, IUserInterfaceController userInterfaceController);

        #endregion Initializers

    }

}