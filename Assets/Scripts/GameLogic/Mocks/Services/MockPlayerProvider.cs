using ProjectExodus.Management.SceneManager;
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

        #endregion Methods
    }

}