
using System.Linq;
using ProjectExodus;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.GameLogic.Player.Weapons;
using ProjectExodus.GameLogic.Weapons;
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
        private IWeaponAssetProvider m_WeaponAssetProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PlayerSetupHandler()
        {
            
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
            this.m_PlayerProvider.SetActivePlayer(_Player); // Move to below method
            GameManager.Instance.SceneManager.SetCurrentPlayerModel(initializationContext.StartupDataOptions.Player); // Should have been done from right at the beginning
            
            // Hook to input system
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.DisableActiveInputControl();
            
            // Setup Gameplay HUD
            IUserInterfaceManager _UserInterfaceManager = initializationContext.ServiceLocator.GetService<IUserInterfaceManager>();
            IUserInterfaceController _ActiveUserInterfaceController =
                _UserInterfaceManager.GetTheActiveUserInterfaceController();
            _ActiveUserInterfaceController.OpenScreen(UIScreenType.GameplayHUD);

            ICommand _GameplayHUDInitializerCommand = new GameplayHUDInitializerCommand(
                Object.FindFirstObjectByType<GameplayHUDView>(FindObjectsInactive.Exclude),
                initializationContext.ServiceLocator.GetService<IGameplayHUDMediator>(),
                _ShipAsset,
                _ShipToSpawn);
           _GameplayHUDInitializerCommand.Execute();
            
            initializationContext.LoadingScreenController.UpdateLoadProgress(60f);
            this.m_NextHandler?.Handle(initializationContext);
        }

        #endregion Methods
  
    }

}