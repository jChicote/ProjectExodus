using System;
using System.Linq;
using ProjectExodus;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;
using IPlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.IPlayerProvider;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class PlayerSetupHandler : ISetupHandler
    {

        #region - - - - - - Fields - - - - - -

        private IInputManager m_InputManager;
        private ICameraController m_CameraController;
        private ISetupHandler m_NextHandler;
        private IPlayerProvider m_PlayerProvider;
        private IPlayerSpawner m_PlayerSpawner;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PlayerSetupHandler(
            IInputManager inputManager,
            ICameraController cameraController,
            IPlayerProvider playerProvider,
            IPlayerSpawner playerSpawner)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_CameraController = cameraController ?? throw new ArgumentNullException(nameof(cameraController));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            this.m_PlayerSpawner = playerSpawner ?? throw new ArgumentNullException(nameof(playerSpawner));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            this.m_PlayerSpawner.InitialisePlayerSpawner(
                initializationContext.ServiceLocator.GetService<IShipAssetProvider>(),
                initializationContext.ServiceLocator.GetService<IWeaponAssetProvider>());
            
            // Temp: The first ship object is used.
            ShipModel _ShipToSpawn = initializationContext.StartupDataOptions.Player.Ships.First();
            
            // Setup Gameplay HUD
            IUserInterfaceManager _UserInterfaceManager = 
                initializationContext.ServiceLocator.GetService<IUserInterfaceManager>();
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            _ActiveUserInterfaceController.InitialiseUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);
            
            // Create Player ship
            GameObject _Player = this.m_PlayerSpawner.SpawnPlayerShip(_ShipToSpawn);
            this.m_CameraController.SetCameraFollowTarget(_Player.transform);
            this.m_PlayerProvider.SetActivePlayer(_Player);
            GameManager.Instance.SceneManager.SetCurrentPlayerModel(initializationContext.StartupDataOptions.Player); // Should have been done from right at the beginning
            
            // Hook to input system
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.DisableActiveInputControl();
            
            // Set hud values
            if (_ActiveUserInterfaceController.TryGetInterfaceController(out object _IntefaceController))
            {
                IGameplayHUDController _HUDController = _IntefaceController as IGameplayHUDController;
                IPlayerHealthSystem _HealthSystem = _Player.GetComponent<IPlayerHealthSystem>();
                _HealthSystem.SetHUDController(_HUDController);
            }
            else
            {
                Debug.LogWarning("[WARNING]: No HUD was found.");
            }
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(60f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}