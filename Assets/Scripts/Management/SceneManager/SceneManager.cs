using System.Linq;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Scene;
using UnityEngine;

namespace ProjectExodus.Management.SceneManager
{

    /// <summary>
    /// Responsible for managing the high-level aspects of gameplay and control of the scene's state.
    /// </summary>
    /// <remarks>This is a persistent manager and should not exist to any specific scene. Use the 'SceneController'
    /// class for control and high-level operation of a scene.</remarks>
    public class SceneManager : MonoBehaviour, ISceneManager
    {

        #region - - - - - - Fields - - - - - -

        private ISceneController m_ActiveSceneController; // Debug Only
        private PlayerModel m_CurrentPlayer;

        #endregion Fields

        #region - - - - - - Properties' - - - - - -

        public PlayerModel Player
            => this.m_CurrentPlayer;

        #endregion Properties'
  
        #region - - - - - - Initialisers - - - - - -

        void ISceneManager.InitialiseSceneManager() 
            => Debug.Log("SceneManager initialised."); // Temp debug only

        #endregion Initialisers
  
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
        {
            SceneManager[] _SceneManager = Object.FindObjectsByType<SceneManager>(FindObjectsSortMode.None);
            if (_SceneManager.Length > 1)
                Debug.LogError($"Multiple {nameof(SceneManager)}s detected. " +
                               $"Only one {nameof(SceneManager)} should exist, unexpected behaviour will occur.");
        }

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        ISceneController ISceneManager.GetActiveSceneController()
        {
            if (this.m_ActiveSceneController != null && this.m_ActiveSceneController.IsActiveInScene())
                return this.m_ActiveSceneController;
            
            if (this.DoesMultipleSceneControllersExist())
                Debug.LogWarning("[WARNING]: Multiple SceneControllers were found.");
            
            this.m_ActiveSceneController = this.GetAllActiveSceneControllers().FirstOrDefault();
            return this.m_ActiveSceneController;
        }

        void ISceneManager.SetCurrentPlayerModel(PlayerModel currentPlayer) 
            => this.m_CurrentPlayer = currentPlayer;

        private bool DoesMultipleSceneControllersExist() 
            => this.GetAllActiveSceneControllers().Length > 1;

        private ISceneController[] GetAllActiveSceneControllers()
        {
            ISceneController[] _SceneControllers = 
                Object.FindObjectsByType<SceneController>(FindObjectsSortMode.None)
                    .Select(sc => (ISceneController)sc)
                    .Where(sc => sc.IsActiveInScene())
                    .ToArray();
            
            return _SceneControllers;
        }

        #endregion Methods

    }

}