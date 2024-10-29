using UnityEngine;

namespace ProjectExodus.GameLogic.Player.PlayerProvider
{

    public class PlayerProvider: MonoBehaviour, IPlayerProvider
    {

        #region - - - - - - Fields - - - - - -

        private GameObject m_ActivePlayer;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        GameObject IPlayerProvider.GetActivePlayer()
        {
            if (this.m_ActivePlayer == null)
                Debug.LogWarning("[WARNING]: No active player found.");

            return this.m_ActivePlayer;
        }

        void IPlayerProvider.SetActivePlayer(GameObject activePlayer)
            => this.m_ActivePlayer = activePlayer;

        #endregion Methods
  
    }

}