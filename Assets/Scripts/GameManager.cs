using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.GameStartup;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Settings;
using ProjectExodus.Management.AudioManager;
using ProjectExodus.Management.EventManager;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceManager;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus
{

    /// <summary>
    /// Responsible for managing the high-level components of the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static GameManager Instance;

        [Header("Persistent Managers")]
        [SerializeField] private AudioManager m_AudioManager;
        [SerializeField] private EventManager m_EventManager;
        [SerializeField] private GameStateManager m_GameStateManager;
        [SerializeField] private InputManager m_InputManager;
        [SerializeField] private SceneManager m_SceneManager;
        [SerializeField] private UserInterfaceManager m_UserInterfaceManager;

        private IGameOptionsFacade m_GameOptionsFacade;
        private IGameSaveFacade m_GameSaveFacade;
        
        private IDataContext m_DataContext;
        private GameSettings m_GameSettings;
        private IObjectMapper m_ObjectMapper;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public IAudioManager AudioManager
            => this.m_AudioManager;

        public IDataContext DataContext
            => this.m_DataContext;

        public IEventManager EventManager
            => this.m_EventManager;

        public IInputManager InputManager
            => this.m_InputManager;

        public IGameOptionsFacade GameOptionsFacade
            => this.m_GameOptionsFacade;

        public IGameSaveFacade GameSaveFacade
            => this.m_GameSaveFacade;

        public GameSettings GameSettings
            => this.m_GameSettings;

        public IGameStateManager GameStateManager
            => this.m_GameStateManager;

        public IObjectMapper Mapper
            => this.m_ObjectMapper;

        public ISceneManager SceneManager
            => this.m_SceneManager;

        public IUserInterfaceManager UserInterfaceManager
            => this.m_UserInterfaceManager;

        #endregion Properties
          
        #region - - - - - - Unity Methods - - - - - -

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Object.Destroy(gameObject);
            
            this.SetupGame();
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        private void SetupGame()
        {
            GameStartupHandler _GameStartHandler = this.GetComponent<GameStartupHandler>();
            this.StartCoroutine(_GameStartHandler.ConfigureGame(this.ConfigureGameManagerValues));
        }

        private void ConfigureGameManagerValues(GameSetupConfig setupConfig)
        {
            // Set game services
            this.m_DataContext = setupConfig.DataContext;
            this.m_GameSettings = setupConfig.GameSettings;
            this.m_ObjectMapper = setupConfig.ObjectMapper;

            // Set use case facades
            this.m_GameOptionsFacade = setupConfig.GameOptionsFacade;
        }

        #endregion Methods
  
    }

}
