using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public interface IPlayerSpawner
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerSpawner(
            IShipAssetProvider shipAssetProvider,
            IWeaponAssetProvider weaponAssetProvider);

        GameObject SpawnPlayerShip(ShipModel shipToSpawn);

        #endregion Methods

    }

}