using System.Collections;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene.SceneStartup
{

    public class SceneStartupController : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        private ILoadingScreenController m_LoadingScreenController;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        public void InitialiseSceneStartupController(ILoadingScreenController loadingScreenController)
        {
            
        }

        #endregion Initialisers
          
        #region - - - - - - Methods - - - - - -

        public IEnumerator RunSceneStartup()
        {
            yield return this.StartCoroutine(this.SetupSceneData());
            yield return this.StartCoroutine(this.SetupSceneServicesAndControllers());
            yield return this.StartCoroutine(this.SetupPlayer());
            yield return this.StartCoroutine(this.SetupEnemies());
            yield return this.StartCoroutine(this.SetupGameplayHUD());
            
            Debug.Log("[LOG]: The scene is now prepared.");
        }

        private IEnumerator SetupSceneData()
        {
            yield return new WaitForSeconds(2); // Debug
        }

        private IEnumerator SetupSceneServicesAndControllers()
        {
            yield return new WaitForSeconds(2); // Debug
        }

        private IEnumerator SetupPlayer()
        {
            yield return new WaitForSeconds(2); // Debug
        }

        private IEnumerator SetupEnemies()
        {
            yield return new WaitForSeconds(2); // Debug
        }

        private IEnumerator SetupGameplayHUD()
        {
            yield return new WaitForSeconds(2); // Debug
        }
        
        #endregion Methods
  
    }
    
}

