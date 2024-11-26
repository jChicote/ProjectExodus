using System;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using ProjectExodus.GameLogic.Player.PlayerTargetingSystem;
using ProjectExodus.GameLogic.Player.Weapons;
using ProjectExodus.GameLogic.Scene;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.ScriptableObjects.AssetEntities;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        private IGameplayHUDController m_GameplayHUDController;
        private IShipAssetProvider m_ShipAssetProvider;
        private IWeaponAssetProvider m_WeaponAssetProvider;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -
        
        void IPlayerSpawner.InitialisePlayerSpawner(
            IGameplayHUDController gameplayHUDController,
            IShipAssetProvider shipAssetProvider,
            IWeaponAssetProvider weaponAssetProvider)
        {
            this.m_GameplayHUDController =
                gameplayHUDController ?? throw new ArgumentNullException(nameof(gameplayHUDController));
            this.m_ShipAssetProvider = shipAssetProvider ?? throw new ArgumentNullException(nameof(shipAssetProvider));
            this.m_WeaponAssetProvider =
                weaponAssetProvider ?? throw new ArgumentNullException(nameof(weaponAssetProvider));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        GameObject IPlayerSpawner.SpawnPlayerShip(ShipModel shipToSpawn)
        {
            ISceneController _SceneController = SceneManager.Instance.GetActiveSceneController();
            ShipAssetObject _ShipAsset = this.m_ShipAssetProvider.Provide(shipToSpawn.AssetID);
            GameObject _PlayerShip = Instantiate(_ShipAsset.Asset, Vector2.zero, this.transform.rotation);
            
            // Setup Weapons
            _PlayerShip.GetComponent<IPlayerWeaponSystems>()
                .InitialiseWeaponSystems(this.m_WeaponAssetProvider, shipToSpawn.Weapons.ToList());
            
            // Setup health system
            _PlayerShip.GetComponent<IPlayerHealthSystem>()
                .Initializer(
                    this.m_GameplayHUDController,
                    _ShipAsset.BaseShieldHealth + shipToSpawn.ShieldHealthModifier, 
                    _ShipAsset.BasePlatingHealth + shipToSpawn.PlatingHealthModifier);
            
            // Setup Targeting System
            _PlayerShip.GetComponent<IPlayerTargetingSystem>()
                .Initialize(_SceneController.Camera);

            return _PlayerShip;
        }

        #endregion Methods
  
    }

}