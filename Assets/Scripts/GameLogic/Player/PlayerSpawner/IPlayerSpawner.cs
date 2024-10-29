using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using UnityEngine;
using IPlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.IPlayerProvider;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public interface IPlayerSpawner
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerSpawner(
            IPlayerProvider playerProvider,
            IShipAssetProvider shipAssetProvider,
            IWeaponAssetProvider weaponAssetProvider);

        GameObject SpawnPlayer(ShipModel shipToSpawn);

        #endregion Methods

    }

}