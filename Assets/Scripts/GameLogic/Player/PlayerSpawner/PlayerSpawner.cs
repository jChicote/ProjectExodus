using System;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.Management.InputManager;
using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerSpawner
{

    public class PlayerSpawner : MonoBehaviour, IPlayerSpawner
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GameObject m_PlayerPrefab; // Move this to the scriptable object.
        
        private IPlayerProvider m_PlayerProvider;
        private IInputManager m_InputManager;

        #endregion Fields

        #region - - - - - - Initializers - - - - - -
        
        void IPlayerSpawner.InitialisePlayerSpawner(IInputManager inputManager, IPlayerProvider playerProvider)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_PlayerProvider = playerProvider ?? throw new ArgumentNullException(nameof(playerProvider));
        }

        #endregion Initializers
  
        #region - - - - - - Methods - - - - - -

        void IPlayerSpawner.SpawnPlayer()
        {
            if (this.m_PlayerProvider.GetActivePlayer() != null) return;
            
            GameObject _Player = Instantiate(this.m_PlayerPrefab, Vector2.zero, this.transform.rotation);
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