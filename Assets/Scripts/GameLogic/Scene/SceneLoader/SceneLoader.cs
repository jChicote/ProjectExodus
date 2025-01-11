using System;
using System.Collections;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectExodus.GameLogic.Scene.SceneLoader
{

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {

        #region - - - - - - Fields - - - - - -

        private int m_SceneCount;

        #endregion Fields
  
        #region - - - - - - Unity Lifecycle Methods - - - - - -

        private void Start()
            => this.m_SceneCount = SceneManager.sceneCountInBuildSettings;

        #endregion Unity Lifecycle Methods
  
        #region - - - - - - Methods - - - - - -

        IEnumerator ISceneLoader.LoadScene(GameScenes gameScene)
        {
            if (gameScene > this.m_SceneCount)
                throw new ArgumentOutOfRangeException();
                
            SceneManager.LoadScene(gameScene.GetValue());
            Debug.Log($"[LOG]: Loading scene {gameScene.GetValue()} -> '{gameScene}'");

            yield return null;
        }

        #endregion Methods
        
    }

}