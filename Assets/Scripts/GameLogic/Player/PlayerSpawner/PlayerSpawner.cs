using System;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.Management.InputManager;
using UnityEngine;
using IPlayerProvider = ProjectExodus.GameLogic.Player.PlayerProvider.IPlayerProvider;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        private ICameraController m_CameraController;
        private IShipAssetProvider m_ShipAssetProvider;
        private IPlayerProvider m_PlayerProvider;
        private IInputManager m_InputManager;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -
        
        void IPlayerSpawner.InitialisePlayerSpawner(
            ICameraController cameraController, 
            IInputManager inputManager, 
            IPlayerProvider playerProvider,
            IShipAssetProvider shipAssetProvider)
        {
            this.m_CameraController = cameraController ?? throw new ArgumentNullException(nameof(cameraController));
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
            this.m_ShipAssetProvider = shipAssetProvider ?? throw new ArgumentNullException(nameof(shipAssetProvider));
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

            GameObject _ShipPrefab = this.m_ShipAssetProvider.Provide(shipToSpawn.AssetID).Asset;
            GameObject _PlayerShip = Instantiate(_ShipPrefab, Vector2.zero, this.transform.rotation);
            this.m_PlayerProvider.SetActivePlayer(_PlayerShip);
            this.m_CameraController.SetCameraFollowTarget(_PlayerShip.transform);
            this.ConfigurePlayer(_PlayerShip);

            return _PlayerShip;
        }

        private void ConfigurePlayer(GameObject playerInstance)
        {
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.EnableActiveInputControl();
        }

        #endregion Methods
  
    }

}