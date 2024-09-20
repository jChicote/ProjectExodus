using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectExodus.GameLogic.Scene.SceneLoader
{

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {

        #region - - - - - - Methods - - - - - -

        void ISceneLoader.LoadScene(int levelNumber)
        {
            if (levelNumber > SceneManager.sceneCount)
                throw new ArgumentOutOfRangeException();
                
            SceneManager.LoadScene(levelNumber);
        }

        #endregion Methods
        
    }

}