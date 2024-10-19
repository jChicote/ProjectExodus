using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.Management.InputManager;
using UnityEngine;
using IPlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.IPlayerProvider;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public interface IPlayerSpawner
    {

        #region - - - - - - Methods - - - - - -

        void InitialisePlayerSpawner(
            ICameraController cameraController, 
            IInputManager inputManager, 
            IPlayerProvider playerProvider,
            IShipAssetProvider shipAssetProvider);

        GameObject SpawnPlayer(ShipModel shipToSpawn);

        #endregion Methods

    }

}