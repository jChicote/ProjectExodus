using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Scene.SetupHandlers;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene
{

    public class SceneController : MonoBehaviour, ISceneController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private SceneStartupHandler m_SceneStartupController;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        void ISceneController.InitialiseSceneController()
        {
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
            
            this.m_SceneStartupController
                .InitialiseSceneStartupController(
                    GameManager.Instance.InputManager,
                    _ServiceLocator);
        }

        #endregion Initialisers

        #region - - - - - - Methods - - - - - -

        bool ISceneController.IsActiveInScene()
            => this.gameObject.activeInHierarchy;

        void ISceneController.RunSceneStartup() 
            => this.StartCoroutine(this.m_SceneStartupController.RunSceneStartup());

        #endregion Methods
  
    }

}