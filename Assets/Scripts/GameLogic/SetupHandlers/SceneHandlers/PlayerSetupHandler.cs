using System.Linq;
using ProjectExodus;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.Management.InputManager;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.Utility.GameLogging;
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
            ICameraController _CameraController = initializationContext.CameraController;
            IPlayerProvider _PlayerProvider = PlayerProvider;
            IPlayerSpawner _PlayerSpawner = PlayerSpawner;
            IPlayerObserver _PlayerObserver = initializationContext.PlayerObserver;
            
            // TODO: Change this so that during the pipeline if it fails, a warning popup is presented to the Player.
            if (!initializationContext.ActiveUserInterfaceController.TryGetGUIControllers(out object _Controllers))
            {
                Debug.LogError("[ERROR]: No GameplayHUDController is found. Aborting setup pipeline.");
                return;
            }
            
            _PlayerSpawner.InitialisePlayerSpawner(
                (GameplaySceneGUIControllers)_Controllers,
                initializationContext.PlayerObserver,
                initializationContext.ServiceLocator.GetService<IShipAssetProvider>(),
                initializationContext.ServiceLocator.GetService<IWeaponAssetProvider>());
            
            ShipModel _ShipToSpawn = initializationContext
                .StartupDataOptions
                .Player
                .Ships
                .Single(sm => sm.ID == initializationContext.StartupDataOptions.SelectedShip.ID);
            
            // Assign listeners to Player Observer for spawning
            _PlayerObserver.OnPlayerSpawned.AddListener(newPlayer => _PlayerProvider.SetActivePlayer(newPlayer));
            _PlayerObserver.OnPlayerSpawned.AddListener(newPlayer => _CameraController.SetCameraFollowTarget(newPlayer.transform));
            _PlayerObserver.OnPlayerSpawned.AddListener(_ => InputManager.Instance.PossesGameplayInputControls());
            _PlayerObserver.OnPlayerDeath.AddListener(initializationContext.InputManager.UnpossesGameplayInputControls);
            
            // Create Player ship
            _ = _PlayerSpawner.SpawnPlayerShip(_ShipToSpawn);
            
            // Hook to input system
            initializationContext.InputManager.PossesGameplayInputControls();
            initializationContext.InputManager.DisableActiveInputControl();
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(60f);
            
            GameLogger.Log("PlayerSetupHandler has run.");
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}