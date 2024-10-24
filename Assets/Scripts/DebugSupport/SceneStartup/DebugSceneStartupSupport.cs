using System;
using System.Collections;
using System.Linq;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Common.Services;
using ProjectExodus.DebugSupport.OutputHandlers;
using ProjectExodus.DebugSupport.Provider;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Enumeration;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Facades.PlayerActionFacades;
using ProjectExodus.GameLogic.Facades.ShipActionFacade;
using ProjectExodus.GameLogic.Facades.WeaponActionFacade;
using ProjectExodus.GameLogic.Scene.SceneStartup;
using ProjectExodus.Management.GameSaveManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace ProjectExodus.DebugSupport.SceneStartup
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
            // Validate whether boot scene was run first
            if (this.ValidateSupportRequirements()) return;
            
            DebugSceneStartupSupport[] _StartupSupport = 
                Object.FindObjectsByType<DebugSceneStartupSupport>(FindObjectsSortMode.None);

            // Remove duplicate instance
            if (_StartupSupport.Length > 1)
            {
                Debug.LogWarning(
                    $"[WARNING]: Startup Support Detected. Deleting this object for scene " +
                    $"{SceneManager.GetActiveScene().name}");
                Destroy(this.gameObject);
                return;
            }
            
            this.LoadPersistenceScene();
        }

        #endregion Unity Lifecycle Methods

        #region - - - - - - Methods - - - - - -

        public virtual void ActivateScene() 
            => this.StartCoroutine(this.HandleSceneActivationWithDefaultData());

        private IEnumerator HandleSceneActivationWithDefaultData()
        {
            this.SetGameSave();
            this.ActivateSceneObjects();
            
            // Run the scene startup behavior
            SceneStartupHandler _StartupHandler =
                Object.FindFirstObjectByType<SceneStartupHandler>(FindObjectsInactive.Exclude);
            if (_StartupHandler != null)
            {
                _StartupHandler.InitialiseSceneStartupController(
                    GameManager.Instance.InputManager, 
                    GameManager.Instance.ServiceLocator);
                this.StartCoroutine(_StartupHandler.RunSceneStartup());
            }
            
            yield return null;
        }

        protected void LoadPersistenceScene()
        {
            if (!this.IsSceneInDevelopment) return;
            this.DeactivateScene();
            
            // Loads the persistent scene first
            SceneManager.LoadScene(GameScenes.PersistenceScene.GetValue(), LoadSceneMode.Additive);
        }

        private void ActivateSceneObjects()
        {
            foreach (GameObject _SceneObject in this.m_ActiveSceneObjects)
            {
                if (!_SceneObject.layer.Equals(GameLayer.Ignore))
                    _SceneObject.SetActive(true);
            }

            this.m_ActiveSceneObjects = Array.Empty<GameObject>();
        }

        private void DeactivateScene()
        {
            this.gameObject.transform.parent = null;
            
            // Disable all scene object
            this.m_ActiveSceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject _SceneObject in this.m_ActiveSceneObjects)
            {
                if (!_SceneObject.layer.Equals(GameLayer.Ignore) 
                    && _SceneObject.GetInstanceID() != this.gameObject.GetInstanceID())
                    _SceneObject.SetActive(false);
            }
        }

        private void SetGameSave()
        {
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
            IGameSaveFacade _GameSaveFacade = _ServiceLocator.GetService<IGameSaveFacade>();
            DebugGetGameSavesPresenter _GameSavePresenter = new DebugGetGameSavesPresenter();
            _GameSaveFacade.GetGameSaves(_GameSavePresenter);

            GameSaveModel _DebugGameSaveModel = new();
            if (_GameSavePresenter.Result.All(gs => 
                    gs.GameSaveName != DebugGameContansts.DEBUG_GAMESAVENAME))
            {
                // Generate GameSave if no instance is found
                DebugDefaultGameSaveGenerator _Generator = new(
                    _ServiceLocator.GetService<IDataContext>(),
                    _GameSaveFacade,
                    _ServiceLocator.GetService<IPlayerActionFacade>(),
                    _ServiceLocator.GetService<IShipActionFacade>(),
                    _ServiceLocator.GetService<IWeaponActionFacade>());
                
                _Generator.GenerateDefaultGameSave();
                _DebugGameSaveModel = _Generator.GeneratedGameSave;
            }
            else
            {
                _DebugGameSaveModel =
                    _GameSavePresenter.Result
                        .Single(gs => gs.GameSaveName == DebugGameContansts.DEBUG_GAMESAVENAME);
            }
            
            IGameSaveManager _GameSaveManager = _ServiceLocator.GetService<IGameSaveManager>();
            _GameSaveManager.SetGameSave(_DebugGameSaveModel);
        }

        protected bool ValidateSupportRequirements() 
            => Object.FindAnyObjectByType<DebugGameManagerSupport>(FindObjectsInactive.Exclude);

        #endregion Methods
  
    }

}