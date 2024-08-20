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
            => this.m_LoadingScreenController = loadingScreenController;

        #endregion Initialisers
          
        #region - - - - - - Methods - - - - - -

        public IEnumerator RunSceneStartup()
        {
            this.m_LoadingScreenController.ShowScreen();
            
            yield return this.StartCoroutine(this.SetupSceneData());
            yield return this.StartCoroutine(this.SetupSceneServicesAndControllers());
            yield return this.StartCoroutine(this.SetupPlayer());
            yield return this.StartCoroutine(this.SetupEnemies());
            yield return this.StartCoroutine(this.SetupGameplayHUD());
            yield return this.StartCoroutine(this.CompleteGameStartup());
            
            Debug.Log("[LOG]: The scene is now prepared.");
        }

        private IEnumerator SetupSceneData()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(20f);
        }

        private IEnumerator SetupSceneServicesAndControllers()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(40f);
        }

        private IEnumerator SetupPlayer()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(60f);
        }

        private IEnumerator SetupEnemies()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(80f);
        }

        private IEnumerator SetupGameplayHUD()
        {
            yield return new WaitForSeconds(2); // Debug
            
            // Note: This method will bind to the HUD
            this.m_LoadingScreenController.UpdateLoadProgress(100f);
        }

        private IEnumerator CompleteGameStartup()
        {
            this.m_LoadingScreenController.HideScreen();
            this.m_LoadingScreenController.ResetLoadingScreen();
            
            yield return null;
        }
        
        #endregion Methods
  
    }
    
}

