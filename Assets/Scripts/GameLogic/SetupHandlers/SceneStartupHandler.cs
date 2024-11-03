using System;
using System.Collections;
using GameLogic.SetupHandlers;
using GameLogic.SetupHandlers.SceneHandlers;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.GameLogic.Infrastructure.DataLoading;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.GameSaveManager;
using ProjectExodus.Management.InputManager;
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

        private StartupDataOptions m_StartupDataOptions;

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

            yield return this.StartCoroutine(this.StartSceneStartup());
            yield return this.StartCoroutine(this.SetupSceneData());
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
            IDataLoader _DataLoader = this.m_ServiceLocator.GetService<IDataLoader>();
            StartupLoadCommand _LoadCommand = new StartupLoadCommand(
                this.m_ServiceLocator.GetService<IGameSaveManager>(),
                this.m_ServiceLocator.GetService<IPlayerActionFacade>());
            yield return StartCoroutine(
                _DataLoader.RunLoadOperation(
                    _LoadCommand, 
                    options => this.m_StartupDataOptions = options as StartupDataOptions));
            
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
            ISetupHandler _PlayerSetupHandler = new PlayerSetupHandler(
                this.m_InputManager,
                this.CameraController,
                this.PlayerProvider,
                this.PlayerSpawner);
            _PlayerSetupHandler.Handle(new SceneSetupInitializationContext()
            {
                LoadingScreenController = this.m_LoadingScreenController,
                ServiceLocator = this.m_ServiceLocator,
                StartupDataOptions = this.m_StartupDataOptions
            });
            
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

