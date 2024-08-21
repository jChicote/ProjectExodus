using ProjectExodus.GameLogic.Scene.SceneStartup;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene
{

    public class SceneController : MonoBehaviour, ISceneController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private SceneStartupController m_SceneStartupController;

        #endregion Fields

        #region - - - - - - Initialisers - - - - - -

        void ISceneController.InitialiseSceneController() 
            => this.m_SceneStartupController
                .InitialiseSceneStartupController(
                    GameManager.Instance.InputManager,
                    GameManager.Instance.UserInterfaceManager.LoadingScreenController);

        #endregion Initialisers

        #region - - - - - - Methods - - - - - -

        void ISceneController.RunSceneStartup() 
            => this.StartCoroutine(this.m_SceneStartupController.RunSceneStartup());

        #endregion Methods
  
    }

}