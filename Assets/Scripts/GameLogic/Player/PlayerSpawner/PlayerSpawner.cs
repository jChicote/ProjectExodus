using System;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_PlayerPrefab; // Move this to the scriptable object.

        private ICameraController m_CameraController;
        private IPlayerProvider m_PlayerProvider;
        private IInputManager m_InputManager;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -
        
        void IPlayerSpawner.InitialisePlayerSpawner(
            ICameraController cameraController, 
            IInputManager inputManager, 
            IPlayerProvider playerProvider)
        {
            this.m_CameraController = cameraController ?? throw new ArgumentNullException(nameof(cameraController));
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        GameObject IPlayerSpawner.SpawnPlayer()
        {
            if (this.m_PlayerProvider.GetActivePlayer() != null)
            {
                Debug.LogWarning("[WARNING]: No active player found.");
                return null;
            }
            
            GameObject _Player = Instantiate(this.m_PlayerPrefab, Vector2.zero, this.transform.rotation);
            this.m_PlayerProvider.SetActivePlayer(_Player);
            this.m_CameraController.SetCameraFollowTarget(_Player.transform);
            this.ConfigurePlayer(_Player);

            return _Player;
        }

        private void ConfigurePlayer(GameObject playerInstance)
        {
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.EnableActiveInputControl();
        }

        #endregion Methods
  
    }

}