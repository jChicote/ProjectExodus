using System;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ProjectExodus.DebugSupport.Scene
{

    /// <summary>
    /// Manages the construction state of the scene in DEVELOPMENT.
    ///
    /// Ensures that the persistence scene is available before this scene can proceed.
    /// </summary>
    public class DebugSceneStartupSupport : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public bool IsSceneInDevelopment = false;

        private GameObject[] m_ActiveSceneObjects;

        #endregion Fields
  
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Awake()
        {
            if (!this.IsSceneInDevelopment) return;
            
            // Disable all scene object
            this.m_ActiveSceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject _SceneObject in this.m_ActiveSceneObjects)
            {
                if (!_SceneObject.layer.Equals(GameLayer.Ignore))
                    _SceneObject.SetActive(false);
            }
            
            // Loads the persistent scene first
            SceneManager.LoadScene(GameScenes.PersistenceScene.GetValue(), LoadSceneMode.Additive);
        }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        public void ActivateSceneObjects()
        {
            foreach (GameObject _SceneObject in this.m_ActiveSceneObjects)
            {
                if (!_SceneObject.layer.Equals(GameLayer.Ignore))
                    _SceneObject.SetActive(true);
            }

            // Clear debug object collection from memory
            this.m_ActiveSceneObjects = Array.Empty<GameObject>();
        }

        #endregion Methods
  
    }

}