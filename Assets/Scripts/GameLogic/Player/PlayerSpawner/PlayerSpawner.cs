using System;
using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Player.PlayerHealthSystem;
using ProjectExodus.GameLogic.Player.Weapons;
using ProjectExodus.ScriptableObjects.AssetEntities;
using UnityEngine;
using IPlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.IPlayerProvider;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        private IPlayerProvider m_PlayerProvider;
        private IShipAssetProvider m_ShipAssetProvider;
        private IWeaponAssetProvider m_WeaponAssetProvider;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -
        
        void IPlayerSpawner.InitialisePlayerSpawner(
            IPlayerProvider playerProvider,
            IShipAssetProvider shipAssetProvider,
            IWeaponAssetProvider weaponAssetProvider)
        {
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            this.m_ShipAssetProvider = shipAssetProvider ?? throw new ArgumentNullException(nameof(shipAssetProvider));
            this.m_WeaponAssetProvider =
                weaponAssetProvider ?? throw new ArgumentNullException(nameof(weaponAssetProvider));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        GameObject IPlayerSpawner.SpawnPlayer(ShipModel shipToSpawn)
        {
            if (this.m_PlayerProvider.GetActivePlayer() != null)
            {
                Debug.LogWarning("[WARNING]: Active player has been found.");
                return this.m_PlayerProvider.GetActivePlayer();
            }

            // Create Player Ship
            ShipAssetObject _ShipAsset = this.m_ShipAssetProvider.Provide(shipToSpawn.AssetID);
            GameObject _PlayerShip = Instantiate(_ShipAsset.Asset, Vector2.zero, this.transform.rotation);
            
            // Setup Weapons
            _PlayerShip.GetComponent<IPlayerWeaponSystems>()
                .InitialiseWeaponSystems(this.m_WeaponAssetProvider, shipToSpawn.Weapons.ToList());
            
            // Setup health system
            _PlayerShip.GetComponent<IPlayerHealthSystem>().SetHealth(
                    _ShipAsset.BaseShieldHealth + shipToSpawn.ShieldHealthModifier, 
                    _ShipAsset.BasePlatingHealth + shipToSpawn.PlatingHealthModifier);

            return _PlayerShip;
        }

        #endregion Methods
  
    }

}