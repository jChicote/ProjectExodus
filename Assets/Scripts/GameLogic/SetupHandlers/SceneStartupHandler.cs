using System;
using System.Collections;
using GameLogic.SetupHandlers;
using GameLogic.SetupHandlers.SceneHandlers;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.GameLogic.Infrastructure.DataLoading;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameDataManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;
using PlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.PlayerProvider;

namespace ProjectExodus.GameLogic.Scene.SetupHandlers
{

    public class SceneStartupHandler : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        // All field names need to be better
        [SerializeField] private PlayerSpawner PlayerSpawner;
        [SerializeField] private PlayerProvider PlayerProvider;
        [SerializeField] private CameraController CameraController;
        [SerializeField] private LoadingScreenController LoadingScreenController;

        private IInputManager m_InputManager;
        private ILoadingScreenController m_LoadingScreenController;
        private IServiceLocator m_ServiceLocator;

        // private StartupDataContext m_StartupDataOptions;
        private SceneSetupInitializationContext m_SceneSetupInitializationContext;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        public void InitialiseSceneStartupController(
            IInputManager inputManager,
            IServiceLocator serviceLocator)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_LoadingScreenController = LoadingScreenController 
                                                 ?? throw new NullReferenceException(nameof(ILoadingScreenController));
            this.m_ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
        }

        #endregion Initialisers
          
        #region - - - - - - Methods - - - - - -

        public IEnumerator RunSceneStartup()
        {
            this.m_LoadingScreenController.ShowScreen();

            StartupDataContext _StartupData = new StartupDataContext()
            {
                Player = GameDataManager.Instance.SelectedPlayer,
                SelectedShip = GameDataManager.Instance.SelectedShip
            };

            this.m_SceneSetupInitializationContext = new SceneSetupInitializationContext()
            {
                LoadingScreenController = this.m_LoadingScreenController,
                ServiceLocator = this.m_ServiceLocator,
                StartupDataOptions = _StartupData
            };

            yield return this.StartCoroutine(this.StartSceneStartup());
            // yield return this.StartCoroutine(this.SetupSceneData());
            yield return this.StartCoroutine(this.SetupSceneServicesAndControllers());
            yield return this.StartCoroutine(this.SetupPlayer());
            yield return this.StartCoroutine(this.SetupEnemies());
            yield return this.StartCoroutine(this.CompleteGameStartup());
            
            Debug.Log("[LOG]: The scene is now prepared.");
        }

        private IEnumerator StartSceneStartup()
        {
            this.m_InputManager.DisableActiveInputControl();
            yield return null;
        }

        private IEnumerator SetupSceneData()
        {
            // Run load options and prepare data
            // IDataLoader _DataLoader = this.m_ServiceLocator.GetService<IDataLoader>();
            // StartupLoadCommand _LoadCommand = new StartupLoadCommand(
            //     this.m_ServiceLocator.GetService<IGameDataManager>(),
            //     this.m_ServiceLocator.GetService<IPlayerActionFacade>());
            // yield return StartCoroutine(
            //     _DataLoader.RunLoadOperation(
            //         _LoadCommand, 
            //         options => this.m_StartupDataOptions = options as StartupDataContext));
            
            this.m_LoadingScreenController.UpdateLoadProgress(20f);
            yield return null;
        }

        private IEnumerator SetupSceneServicesAndControllers()
        {
            this.m_LoadingScreenController.UpdateLoadProgress(40f);
            yield return null;
        }

        private IEnumerator SetupPlayer()
        {
            // Setup Gameplay HUD
            IUserInterfaceManager _UserInterfaceManager = 
                this.m_ServiceLocator.GetService<IUserInterfaceManager>();
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            _ActiveUserInterfaceController.InitialiseUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);
            
            ISetupHandler _PlayerSetupHandler = new PlayerSetupHandler(
                this.m_InputManager,
                this.CameraController,
                this.PlayerProvider,
                this.PlayerSpawner,
                _ActiveUserInterfaceController);
            
            // TODO: Refactor into a mapper 
            _PlayerSetupHandler.Handle(this.m_SceneSetupInitializationContext);
            
            yield return null;
        }

        private IEnumerator SetupEnemies()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(80f);
        }

        private IEnumerator CompleteGameStartup()
        {
            this.m_LoadingScreenController.HideScreen();
            this.m_LoadingScreenController.ResetLoadingScreen();
            
            this.m_InputManager.EnableActiveInputControl();
            
            yield return null;
        }
        
        #endregion Methods
  
    }

}

