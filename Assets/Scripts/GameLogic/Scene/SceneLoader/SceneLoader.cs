using System;
using ProjectExodus.GameLogic.Enumeration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectExodus.GameLogic.Scene.SceneLoader
{

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {

        #region - - - - - - Methods - - - - - -

        
        void ISceneLoader.LoadScene(GameScenes gameScene)
        {
            if (gameScene > SceneManager.sceneCount - 1)
                throw new ArgumentOutOfRangeException();
                
            gameScene.Action.Invoke();
            SceneManager.LoadScene((int)gameScene);
        }

        #endregion Methods
        
    }

}