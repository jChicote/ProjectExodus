using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using ProjectExodus.GameLogic.Scene.SceneStartup;
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
                    GameManager.Instance.UserInterfaceManager.LoadingScreenController,
                    _ServiceLocator.GetService<ISceneLoader>());
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