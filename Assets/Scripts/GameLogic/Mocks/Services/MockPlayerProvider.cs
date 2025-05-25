using ProjectExodus.GameLogic.Player.PlayerProvider;
using UnityEngine;

namespace ProjectExodus.GameLogic.Mocks.Services
{

    public class MockPlayerProvider : MonoBehaviour, IPlayerProvider
    {

        #region - - - - - - Fields - - - - - -

        public GameObject PlayerObject;

        #endregion Fields

        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
            => Debug.Log($"[IMPORTANT]: You are using the mock object {nameof(MockPlayerProvider)}");

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        public GameObject GetActivePlayer()
        {
            if (this.PlayerObject == null)
                Debug.LogError("[ERROR]: No Player object found.");

            return this.PlayerObject;
        }

        public void SetActivePlayer(GameObject activePlayer)
        {
            if (activePlayer == null)
            {
                Debug.LogError("[ERROR]: active player set is null.");
                return;
            }

            this.PlayerObject = activePlayer;
        }

        #endregion Methods
    }

}