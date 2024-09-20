using System;
using System.Collections;
using ProjectExodus.GameLogic.Camera;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.GameLogic.Player.PlayerSpawner;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using ProjectExodus.Management.InputManager;
using ProjectExodus.UserInterface.LoadingScreen;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene.SceneStartup
{

    public class SceneStartupHandler : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private PlayerSpawner PlayerSpawner;
        [SerializeField] private PlayerProvider PlayerProvider;
        [SerializeField] private CameraController CameraController;

        private IInputManager m_InputManager;
        private ILoadingScreenController m_LoadingScreenController;
        private ISceneLoader m_SceneLoader;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        public void InitialiseSceneStartupController(
            IInputManager inputManager,
            ILoadingScreenController loadingScreenController,
            ISceneLoader sceneLoader)
        {
            this.m_InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
            this.m_LoadingScreenController = loadingScreenController
                                                ?? throw new ArgumentException(nameof(loadingScreenController));
            this.m_SceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
        }

        #endregion Initialisers
          
        #region - - - - - - Methods - - - - - -

        public IEnumerator RunSceneStartup()
        {
            this.m_LoadingScreenController.ShowScreen();

            yield return this.StartCoroutine(this.LoadScene());
            yield return this.StartCoroutine(this.StartSceneStartup());
            yield return this.StartCoroutine(this.SetupSceneData());
            yield return this.StartCoroutine(this.SetupSceneServicesAndControllers());
            yield return this.StartCoroutine(this.SetupPlayer());
            yield return this.StartCoroutine(this.SetupEnemies());
            yield return this.StartCoroutine(this.SetupGameplayHUD());
            yield return this.StartCoroutine(this.CompleteGameStartup());
            
            Debug.Log("[LOG]: The scene is now prepared.");
        }

        private IEnumerator LoadScene()
        {
            this.m_SceneLoader.LoadScene(GameScenes.DebugScene1); // Hardcoded for now
            yield return null;
        }

        private IEnumerator StartSceneStartup()
        {
            this.m_InputManager.DisableActiveInputControl();
            yield return new WaitForSeconds(2); // Debug
        }

        private IEnumerator SetupSceneData()
        {
            yield return new WaitForSeconds(2); // Debug
            this.m_LoadingScreenController.UpdateLoadProgress(20f);
        }

        private IEnumerator SetupSceneServicesAndControllers()
        {
            ((IPlayerSpawner)this.PlayerSpawner).InitialisePlayerSpawner(
                this.CameraController, 
                this.m_InputManager, 
                this.PlayerProvider);
            
            this.m_LoadingScreenController.UpdateLoadProgress(40f);
            
            yield return null;
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
            
            this.m_InputManager.EnableActiveInputControl();
            
            yield return null;
        }
        
        #endregion Methods
  
    }
    
}

