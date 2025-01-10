using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.UserInterface.Controllers;
using ProjectExodus.UserInterface.GameplayHUD;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public interface IPlayerSpawner
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerSpawner(
            GameplaySceneGUIControllers gameplaySceneGUIControllers,
            IShipAssetProvider shipAssetProvider,
            IWeaponAssetProvider weaponAssetProvider);

        GameObject SpawnPlayerShip(ShipModel shipToSpawn);

        #endregion Methods

    }

}