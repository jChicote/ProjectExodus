using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.UserInterface.LoadingScreen;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class SceneSetupInitializationContext
    {

        #region - - - - - - Properties - - - - - -

        public ILoadingScreenController LoadingScreenController { get; set; }

        public IServiceLocator ServiceLocator { get; set; }
        
        public StartupDataOptions StartupDataOptions { get; set; }

        #endregion Properties
  
    }

}