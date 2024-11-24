using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.Management.InputManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.LoadingScreen;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class SceneSetupInitializationContext
    {

        #region - - - - - - Properties - - - - - -

        public IUserInterfaceController ActiveUserInterfaceController;

        public IInputManager InputManager { get; set; }

        public ILoadingScreenController LoadingScreenController { get; set; }

        public IServiceLocator ServiceLocator { get; set; }

        public StartupDataContext StartupDataOptions { get; set; }
        
        #endregion Properties
  
    }

}