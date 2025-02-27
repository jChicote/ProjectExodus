using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Pause.PauseController;
using ProjectExodus.GameLogic.Player.PlayerProvider;
using ProjectExodus.GameLogic.Scene.SetupHandlers;
using UnityEngine;

namespace ProjectExodus.GameLogic.Scene
{

    public class SceneController : MonoBehaviour, ISceneController
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private UnityEngine.Camera m_Camera;
        [SerializeField] private SceneStartupHandler m_SceneStartupController;
        [SerializeField] private PauseController m_PauseController;

        [Header("Player Related")]
        [RequiredField]
        [SerializeField]
        private PlayerProvider m_PlayerProvider;
        [RequiredField] 
        [SerializeField] 
        private PlayerObserver m_PlayerObserver;

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

        #region - - - - - - Properties - - - - - -

        public UnityEngine.Camera Camera
            => this.m_Camera;
        
        public IPlayerProvider PlayerProvider
            => this.m_PlayerProvider;

        public IPlayerObserver PlayerObserver
            => this.m_PlayerObserver;
        
        IPauseController ISceneController.PauseController
            => this.m_PauseController;

        #endregion Properties
  
        #region - - - - - - Methods - - - - - -

        bool ISceneController.IsActiveInScene()
            => this.gameObject.activeInHierarchy;

        void ISceneController.RunSceneStartup() 
            => this.m_SceneStartupController.RunSceneStartup();

        #endregion Methods
  
    }

}