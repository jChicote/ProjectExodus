using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectExodus.GameLogic.Scene.SceneLoader
{

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {

        #region - - - - - - Methods - - - - - -

        void ISceneLoader.LoadScene(int sceneNumber)
        {
            Debug.Log($"[LOG] Scene being loaded '{sceneNumber.ToString()}'");
            SceneManager.LoadScene(sceneNumber);
        }

        #endregion Methods
    }

}