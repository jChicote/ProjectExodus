using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Models;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Infrastructure;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Settings;
using ProjectExodus.GameLogic.SetupHandlers;
using ProjectExodus.Management.AudioManager;
using ProjectExodus.Management.EventManager;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.Utility.GameValidation;
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
        [SerializeField] private ServiceLocator m_ServiceLocator;
        
        // Game-Level data
        private GameSaveModel m_GameSave;

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

        public GameSettings GameSettings // Allowed to be set as its configuration is handled in setup handler.
        {
            get => this.m_GameSettings;
            set => this.m_GameSettings = value;
        }

        public IGameStateManager GameStateManager
            => this.m_GameStateManager;

        public IObjectMapper Mapper
            => this.m_ObjectMapper;

        public ISceneManager SceneManager
            => this.m_SceneManager;

        public IUserInterfaceManager UserInterfaceManager
            => this.m_UserInterfaceManager;

        public IServiceLocator ServiceLocator
            => this.m_ServiceLocator;

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

        public bool IsMembersValid()
        {
            return GameValidator.NotNull(this.m_GameOptionsFacade, nameof(this.m_GameOptionsFacade))
                   && GameValidator.NotNull(this.m_GameSaveFacade, nameof(this.m_GameSaveFacade))
                   && GameValidator.NotNull(this.m_DataContext, nameof(this.m_DataContext))
                   && GameValidator.NotNull(this.m_GameSettings, nameof(this.m_GameSettings))
                   && GameValidator.NotNull(this.m_ObjectMapper, nameof(this.m_ObjectMapper))
                   && GameValidator.NotNull(this.m_ServiceLocator, nameof(this.m_ServiceLocator));
        }

        private void SetupGame()
        {
            GameStartupHandler _GameStartHandler = this.GetComponent<GameStartupHandler>();
            _GameStartHandler.OnGameServicesRegistered.AddListener(this.ConfigureGameManagerServices);
            this.StartCoroutine(_GameStartHandler.ConfigureGame());
        }

        private void ConfigureGameManagerServices()
        {
            // Set game services
            this.m_DataContext = this.ServiceLocator.GetService<IDataContext>();
            this.m_ObjectMapper = this.ServiceLocator.GetService<IObjectMapper>();

            // Set use case facades
            this.m_GameOptionsFacade = this.ServiceLocator.GetService<IGameOptionsFacade>();
            this.m_GameSaveFacade = this.ServiceLocator.GetService<IGameSaveFacade>();
        }

        #endregion Methods
  
    }

}
