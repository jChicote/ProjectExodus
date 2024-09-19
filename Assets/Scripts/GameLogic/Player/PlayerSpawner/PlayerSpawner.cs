using System;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        private IPlayerProvider m_PlayerProvider;
        private IInputManager m_InputManager;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -

        public void InitialisePlayerSpawner(IInputManager inputManager, IPlayerProvider playerProvider)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        public void SpawnPlayer()
        {
            if (this.m_PlayerProvider.GetActivePlayer() != null) return;
            
            GameObject _Player = Instantiate(null, Vector2.zero, this.transform.rotation);
            this.m_PlayerProvider.SetActivePlayer(_Player);
            this.ConfigurePlayer(_Player);
        }

        private void ConfigurePlayer(GameObject playerInstance)
        {
            this.m_InputManager.PossesGameplayInputControls();
            this.m_InputManager.EnableActiveInputControl();
        }

        #endregion Methods
  
    }

}