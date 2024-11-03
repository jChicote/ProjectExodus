using System;
using System.Linq;
using ProjectExodus;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.Enumeration;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.ScriptableObjects.AssetEntities;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using ProjectExodus.UserInterface.GameplayHUD.Initializer;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;
using IPlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.IPlayerProvider;
using Object = UnityEngine.Object;

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
        private IShipAssetProvider m_ShipAssetProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PlayerSetupHandler(
            IInputManager inputManager,
            ICameraController cameraController,
            IPlayerProvider playerProvider,
            IPlayerSpawner playerSpawner,
            IShipAssetProvider shipAssetProvider)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_CameraController = cameraController ?? throw new ArgumentNullException(nameof(cameraController));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            this.m_PlayerSpawner = playerSpawner ?? throw new ArgumentNullException(nameof(playerSpawner));
            this.m_ShipAssetProvider = shipAssetProvider ?? throw new ArgumentNullException(nameof(shipAssetProvider));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            // Temp: The first ship object is used.
            ShipModel _ShipToSpawn = initializationContext.StartupDataOptions.Player.Ships.First();
            ShipAssetObject _ShipAsset = this.m_ShipAssetProvider.Provide(_ShipToSpawn.AssetID);
            
            // Create Player ship
            GameObject _Player = this.m_PlayerSpawner.SpawnPlayerShip(_ShipToSpawn);
            this.m_CameraController.SetCameraFollowTarget(_Player.transform);
            this.m_PlayerProvider.SetActivePlayer(_Player);
            GameManager.Instance.SceneManager.SetCurrentPlayerModel(initializationContext.StartupDataOptions.Player); // Should have been done from right at the beginning
            
            // Hook to input system
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.DisableActiveInputControl();
            
            // Setup Gameplay HUD
            IUserInterfaceManager _UserInterfaceManager = 
                initializationContext.ServiceLocator.GetService<IUserInterfaceManager>();
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);

            initializationContext.LoadingScreenController.UpdateLoadProgress(60f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}