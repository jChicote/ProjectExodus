using ProjectExodus.DebugSupport.SceneStartup;
using ProjectExodus.GameLogic.GameStartup;
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

        #endregion Fields

        #region - - - - - - Unity Lifecycle - - - - - -

        /// <remarks>
        /// This class should only be awake if constructed after the debug 'game scene' is constructed.
        /// </remarks>
        private void Awake()
        {
            if (!Object.FindAnyObjectByType<DebugSceneStartupSupport>()) return;
            
            // Add all debug services and handlers requiring initialisation
            DebugSceneStartupSupport _SceneStartupSupport = Object.FindFirstObjectByType<DebugSceneStartupSupport>();
            GameStartupHandler _GameStartupHandler = Object.FindFirstObjectByType<GameStartupHandler>();
            if (_SceneStartupSupport != null) 
                _GameStartupHandler.OnGameSetupCompletion.AddListener(_SceneStartupSupport.ActivateSceneObjects);
            
            this.IN_DEVELOPMENT = _SceneStartupSupport.IsSceneInDevelopment;
        }

        #endregion Unity Lifecycle
  
    }

}