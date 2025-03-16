using ProjectExodus;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Camera;
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

        [Header("Camera")]
        [RequiredField] public Camera MainCamera;
        [RequiredField] public CameraController CameraController;
        
        [Header("Player Systems")]
        [RequiredField] public PlayerObserver PlayerObserver;
        
        [Header("Settings")]
        [RequiredField] public UserInterfaceSettings UserInterfaceSettings;
        public StartupDataContext StartupDataOptions;

        [Header("User Interface")]
        [RequiredField] public GameObject UserInterfaceEventMediatorObject;

        #endregion Fields
  
        #region - - - - - - Properties - - - - - -

        public IUserInterfaceController ActiveUserInterfaceController { get; set; }

        public IInputManager InputManager { get; set; }

        public ILoadingScreenController LoadingScreenController { get; set; }

        public IServiceLocator ServiceLocator { get; set; }
        
        #endregion Properties
  
    }

}