using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.GameLogic.Scene;
using UnityEngine;

namespace ProjectExodus.Management.SceneManager
{

    /// <summary>
    /// Responsible for managing the high-level aspects of gameplay and control of the scene's state.
    /// </summary>
    /// <remarks>This is a persistent manager and should not exist to any specific scene. Use the 'SceneController'
    /// class for control and high-level operation of a scene.</remarks>
    public class SceneManager : MonoBehaviour, ISceneManager, IPlayerProvider
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private PauseController m_PauseController;
        
        [Space]
        [SerializeField] private GameObject m_ActivePlayer; // Debug only
        [SerializeField] private SceneController m_ActiveSceneController; // Debug Only

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        void ISceneManager.InitialiseSceneManager() 
            => Debug.Log("SceneManager initialised."); // Temp debug only

        #endregion Initialisers
  
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            SceneManager[] _SceneManager = Object.FindObjectsByType<SceneManager>(FindObjectsSortMode.None);
            if (_SceneManager.Length > 1)
                Debug.LogError($"Multiple {nameof(SceneManager)}s detected. " +
                               $"Only one {nameof(SceneManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Methods
  
        #region - - - - - - Methods - - - - - -

        ISceneController ISceneManager.GetActiveSceneController() 
            => this.m_ActiveSceneController;

        GameObject IPlayerProvider.GetActivePlayer()
            => this.m_ActivePlayer;

        #endregion Methods

    }

}