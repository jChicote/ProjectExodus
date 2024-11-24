using System;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace GameLogic.SetupHandlers.SceneHandlers
{

    public class PlayerSetupHandler : MonoBehaviour, ISetupHandler
    {

        #region - - - - - - Fields - - - - - -
        
        [SerializeField] private PlayerSpawner PlayerSpawner;
        [SerializeField] private PlayerProvider PlayerProvider;
        [SerializeField] private CameraController CameraController;

        private ISetupHandler m_NextHandler;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void ISetupHandler.SetNext(ISetupHandler next)
            => this.m_NextHandler = next;

        void ISetupHandler.Handle(SceneSetupInitializationContext initializationContext)
        {
            ICameraController _CameraController = CameraController;
            IPlayerProvider _PlayerProvider = PlayerProvider;
            IPlayerSpawner _PlayerSpawner = PlayerSpawner;
            
            // TODO: Change this so that during the pipeline if it fails, a warning popup is presented to the Player.
            if (!initializationContext.ActiveUserInterfaceController.TryGetGUIControllers(out object _Controllers))
            {
                Debug.LogError("[ERROR]: No GameplayHUDController is found. Aborting setup pipeline.");
                return;
            }
            
            IGameplayHUDController _GameplayHUDController = 
                ((GameplaySceneGUIControllers)_Controllers).GetGameplayHUDController();
            _PlayerSpawner.InitialisePlayerSpawner(
                _GameplayHUDController,
                initializationContext.ServiceLocator.GetService<IShipAssetProvider>(),
                initializationContext.ServiceLocator.GetService<IWeaponAssetProvider>());
            
            ShipModel _ShipToSpawn = initializationContext
                .StartupDataOptions
                .Player
                .Ships
                .Single(sm => sm.ID == initializationContext.StartupDataOptions.SelectedShip.ID);
            
            // Create Player ship
            GameObject _Player = _PlayerSpawner.SpawnPlayerShip(_ShipToSpawn);
            _CameraController.SetCameraFollowTarget(_Player.transform);
            _PlayerProvider.SetActivePlayer(_Player);
            
            // Hook to input system
            initializationContext.InputManager.PossesGameplayInputControls();
            initializationContext.InputManager.DisableActiveInputControl();
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(60f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}