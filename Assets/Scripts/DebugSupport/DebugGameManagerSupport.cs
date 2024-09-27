using UnityEngine;
using UnityEngine.Events;

namespace ProjectExodus.DebugSupport
{

    /// <summary>
    /// Support class for managing the 'In Development' state of the GameManager.
    /// </summary>
    public class DebugGameManagerSupport : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        // Debug Flag
        public bool IN_DEVELOPMENT = false;
                
        public UnityEvent OnGameSetupComplete;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void SetGameManagerToBeInDevelopment(bool state)
        {
            this.IN_DEVELOPMENT = state;
            Debug.LogWarning("[WARNING]: The game is running in 'DEVELOPMENT'");
        }

        #endregion Methods
  
    }

}