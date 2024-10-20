using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Facades.PlayerControllers;
using ProjectExodus.GameLogic.Infrastructure.DataLoading;
using ProjectExodus.GameLogic.Infrastructure.DataLoading.LoadCommands;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.GameLogic.Player.Weapons;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.GameSaveManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;
using PlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.PlayerProvider;

namespace ProjectExodus.GameLogic.Scene.SceneStartup
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
            yield return this.StartCoroutine(this.SetupGameplayHUD());
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
                this.m_ServiceLocator.GetService<IPlayerControllers>());
            yield return StartCoroutine(
                _DataLoader.RunLoadOperation(
                    _LoadCommand, 
                    options => this.m_StartupDataOptions = options as StartupDataOptions));
            
            this.m_LoadingScreenController.UpdateLoadProgress(20f);
            yield return null;
        }

        private IEnumerator SetupSceneServicesAndControllers()
        {
            ((IPlayerSpawner)this.PlayerSpawner).InitialisePlayerSpawner(
                this.CameraController, 
                this.m_InputManager, 
                this.PlayerProvider,
                this.m_ServiceLocator.GetService<IShipAssetProvider>());
            
            this.m_LoadingScreenController.UpdateLoadProgress(40f);
            yield return null;
        }

        private IEnumerator SetupPlayer()
        {
            // Temp: The first ship object is used.
            ShipModel _ShipToSpawn = this.m_StartupDataOptions.Player.Ships.First();
            
            // Debug Only 
            // ShipModel _ShipToSpawn = new ShipModel();
            // List<WeaponModel> _DebugWeaponData = new List<WeaponModel>
            // {
            //     new() { AssetID = 0, AssignedBayID = 999 },
            //     new() { AssetID = 0, AssignedBayID = 888 },
            //     new() { AssetID = 0, AssignedBayID = 777 }
            // };
            // _ShipToSpawn.Weapons = _DebugWeaponData;
            
            // Create Player ship
            GameObject _Player = ((IPlayerSpawner)this.PlayerSpawner)
                .SpawnPlayer(_ShipToSpawn);
            _Player.GetComponent<IPlayerWeaponSystems>().InitialiseWeaponSystems(
                this.m_ServiceLocator.GetService<IWeaponAssetProvider>(),
                _ShipToSpawn.Weapons.ToList());
            this.m_InputManager.DisableActiveInputControl();
            
            GameManager.Instance.SceneManager.SetCurrentPlayerModel(this.m_StartupDataOptions.Player);
            
            this.m_LoadingScreenController.UpdateLoadProgress(60f);
            yield return null;
        }

        private IEnumerator SetupEnemies()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(80f);
        }

        private IEnumerator SetupGameplayHUD()
        {
            IUserInterfaceManager _UserInterfaceManager = this.m_ServiceLocator.GetService<IUserInterfaceManager>();
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);
            
            // Note: This method will bind to the HUD
            this.m_LoadingScreenController.UpdateLoadProgress(100f);
            yield return null;
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

