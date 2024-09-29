using ProjectExodus.DebugSupport.Scene;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

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

        public UnityEvent OnGameSetupComplete = new();

        #endregion Fields

        #region - - - - - - Unity Lifecycle - - - - - -

        /// <remarks>
        /// This class should only be awake if constructed after the debug 'game scene' is constructed.
        /// </remarks>
        private void Awake()
        {
            // Add all debug services and handlers requiring initialisation
            DebugSceneStartupSupport _SceneStartupSupport = Object.FindFirstObjectByType<DebugSceneStartupSupport>();
            if (_SceneStartupSupport != null)
            {
                this.OnGameSetupComplete.AddListener(_SceneStartupSupport.ActivateSceneObjects);
            }
            
            this.IN_DEVELOPMENT = _SceneStartupSupport.IsSceneInDevelopment;
        }

        #endregion Unity Lifecycle
  
    }

}