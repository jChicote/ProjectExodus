using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ProjectExodus;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.InputManager;
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
        private IUserInterfaceController m_UserInterfaceController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PlayerSetupHandler(
            IInputManager inputManager,
            ICameraController cameraController,
            IPlayerProvider playerProvider,
            IPlayerSpawner playerSpawner,
            IUserInterfaceController userInterfaceController)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_CameraController = cameraController ?? throw new ArgumentNullException(nameof(cameraController));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            this.m_PlayerSpawner = playerSpawner ?? throw new ArgumentNullException(nameof(playerSpawner));
            this.m_UserInterfaceController = 
                userInterfaceController ?? throw new ArgumentNullException(nameof(userInterfaceController));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            // TODO: Change this so that during the pipeline if it fails, a warning popup is presented to the Player.
            if (!this.m_UserInterfaceController.TryGetGUIControllers(out object _Controllers))
            {
                Debug.LogError("[ERROR]: No GameplayHUDController is found. Aborting setup pipeline.");
                return;
            }
            
            IGameplayHUDController _GameplayHUDController = 
                ((GameplaySceneGUIControllers)_Controllers).GetGameplayHUDController();
            this.m_PlayerSpawner.InitialisePlayerSpawner(
                _GameplayHUDController,
                initializationContext.ServiceLocator.GetService<IShipAssetProvider>(),
                initializationContext.ServiceLocator.GetService<IWeaponAssetProvider>());
            
            // Temp: The first ship object is used.
            ShipModel _ShipToSpawn = initializationContext
                .StartupDataOptions
                .Player
                .Ships
                .Single(sm => sm.ID == initializationContext.SelectedShipID);
            
            // Create Player ship
            GameObject _Player = this.m_PlayerSpawner.SpawnPlayerShip(_ShipToSpawn);
            this.m_CameraController.SetCameraFollowTarget(_Player.transform);
            this.m_PlayerProvider.SetActivePlayer(_Player);
            
            // // TODO: Move this out of the context of the scene setup to the start of the main menu.
            // GameManager.Instance.SceneManager.SetCurrentPlayerModel(initializationContext.StartupDataOptions.Player);
            
            // Hook to input system
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.DisableActiveInputControl();
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(60f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}