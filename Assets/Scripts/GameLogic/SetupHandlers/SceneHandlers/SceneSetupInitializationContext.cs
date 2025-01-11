using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.Management.InputManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class SceneSetupInitializationContext : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [RequiredField] public Camera MainCamera;
        [RequiredField] public UserInterfaceSettings UserInterfaceSettings;
        public StartupDataContext StartupDataOptions;

        #endregion Fields
  
        #region - - - - - - Properties - - - - - -

        public IUserInterfaceController ActiveUserInterfaceController { get; set; }

        public IInputManager InputManager { get; set; }

        public ILoadingScreenController LoadingScreenController { get; set; }

        public IServiceLocator ServiceLocator { get; set; }
        
        #endregion Properties
  
    }

}