using ProjectExodus.GameLogic.Pause.PauseController;
using UnityEngine;

namespace ProjectExodus.Management.SceneManager
{

    /// <summary>
    /// Responsible for managing the high-level aspects of gameplay and control of the scene's state.
    /// </summary>
    public class SceneManager : MonoBehaviour, ISceneManager, IPlayerProvider
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private PauseController m_PauseController;
        [SerializeField] private GameObject m_ActivePlayer; // Debug only

        #endregion Fields
  
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

        void ISceneManager.InitialiseSceneManager()
        {
            Debug.Log("SceneManager initialised."); // Temp debug only
        }

        GameObject IPlayerProvider.GetActivePlayer()
            => this.m_ActivePlayer;

        #endregion Methods

    }

}