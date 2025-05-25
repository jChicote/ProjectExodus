using System;
using System.Linq;
using Cinemachine;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using ProjectExodus.GameLogic.Player.Weapons;
using ProjectExodus.GameLogic.Scene;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.ScriptableObjects.AssetEntities;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        private GameplaySceneGUIControllers m_GameplaySceneGUIControllers;
        private IPlayerObserver m_PlayerObserver;
        private IShipAssetProvider m_ShipAssetProvider;
        private IWeaponAssetProvider m_WeaponAssetProvider;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -
        
        void IPlayerSpawner.InitialisePlayerSpawner(
            GameplaySceneGUIControllers gameplaySceneGUIControllers,
            IPlayerObserver playerObserver,
            IShipAssetProvider shipAssetProvider,
            IWeaponAssetProvider weaponAssetProvider)
        {
            this.m_GameplaySceneGUIControllers = gameplaySceneGUIControllers 
                ?? throw new ArgumentNullException(nameof(gameplaySceneGUIControllers));
            this.m_PlayerObserver = playerObserver
                ?? throw new ArgumentNullException(nameof(playerObserver));
            this.m_ShipAssetProvider = shipAssetProvider 
                ?? throw new ArgumentNullException(nameof(shipAssetProvider));
            this.m_WeaponAssetProvider = weaponAssetProvider 
                ?? throw new ArgumentNullException(nameof(weaponAssetProvider));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        GameObject IPlayerSpawner.SpawnPlayerShip(ShipModel shipToSpawn)
        {
            ISceneController _SceneController = SceneManager.Instance.GetActiveSceneController();
            IGameplayHUDController _GameplayHUDController =
                this.m_GameplaySceneGUIControllers.GetGameplayHUDController();
            ShipAssetObject _ShipAsset = this.m_ShipAssetProvider.Provide(shipToSpawn.AssetID);
            
            GameObject _PlayerShip = Instantiate(_ShipAsset.Asset, Vector2.zero, this.transform.rotation);
            _PlayerShip.layer = GameLayer.Player;
            
            // Setup Weapons
            _PlayerShip.GetComponent<IPlayerWeaponSystems>()
                .InitialiseWeaponSystems(this.m_WeaponAssetProvider, shipToSpawn.Weapons.ToList());
            
            // Setup health system
            _PlayerShip.GetComponent<IPlayerHealthSystem>()
                .Initializer(
                    _GameplayHUDController,
                    this.m_PlayerObserver,
                    _ShipAsset.BaseShieldHealth + shipToSpawn.ShieldHealthModifier, 
                    _ShipAsset.BasePlatingHealth + shipToSpawn.PlatingHealthModifier);
            
            // Setup Targeting System
            _PlayerShip.GetComponent<IPlayerTargetingSystem>()
                .Initialize(_SceneController.Camera, this.m_GameplaySceneGUIControllers.PlayerTargetingHUDController);
            _PlayerShip.GetComponent<IInitialize<TractorBeamTrackingHandlerInitializationData>>()
                .Initialize(new()
                {
                    Camera = SceneManager.Instance.GetActiveSceneController().Camera,
                    PlayerTransform = _PlayerShip.transform,
                    TractorBeamTrackingHUDController = this.m_GameplaySceneGUIControllers.TractorBeamTrackingHUDController
                });
            _PlayerShip.GetComponent<IInitialize<WeaponTargetingHandlerInitializationData>>()
                .Initialize(new()
                {
                    Camera = SceneManager.Instance.GetActiveSceneController().Camera,
                    WeaponTrackingHUDController = this.m_GameplaySceneGUIControllers.WeaponTrackingHUDController
                });
            
            // Broadcast to all components reliant on the Player's gameobject
            this.m_PlayerObserver.OnPlayerSpawned?.Invoke(_PlayerShip);

            return _PlayerShip;
        }

        #endregion Methods
  
    }

}