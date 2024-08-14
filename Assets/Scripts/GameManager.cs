using System.Threading.Tasks;
using ProjectExodus.Backend.Configuration;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Configuration;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.GameSettings;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
using ProjectExodus.Management.AudioManager;
using ProjectExodus.Management.EventManager;
using ProjectExodus.Management.GameStateManager;
using ProjectExodus.Management.InputManager;
using ProjectExodus.Management.SceneManager;
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.UserInterface.Configuration;
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

        private GameSettings m_GameSettings;
        private ObjectMapper m_ObjectMapper;
        private IGameOptionsFacade m_GameOptionsFacade;
        private IDataContext m_DataContext;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public IAudioManager AudioManager
            => this.m_AudioManager;

        public IEventManager EventManager
            => this.m_EventManager;

        public IInputManager InputManager
            => this.m_InputManager;

        public IGameOptionsFacade GameOptionsFacade
            => this.m_GameOptionsFacade;

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
            
            _ = this.SetupGame();
        }

        #endregion Unity Methods

        #region - - - - - - Methods - - - - - -

        private async Task SetupGame()
        {
            // Setup settings
            // NOTE: This is temporary until a use case exists to initialise the settings option from a scriptable-object
            this.m_GameSettings = new GameSettings();
            this.m_GameSettings.SetGameOptions(new GameOptionsModel());
            
            // Setup Services and Configuration
            this.m_DataContext = new JsonDataContext(); // Temporarily be initialised here
            this.m_ObjectMapper = new ObjectMapper();
            ((IConfigure)new BackendConfiguration(this.m_ObjectMapper)).Configure();
            ((IConfigure)new GameLogicConfiguration(this.m_ObjectMapper)).Configure();
            ((IConfigure)new UserInterfaceConfiguration(this.m_ObjectMapper)).Configure();
            
            // Configure data for GameLogic, Management and UI
            GameOptionsRepository _GameOptionsRepository = new GameOptionsRepository(
                                                            this.m_DataContext, 
                                                            this.m_ObjectMapper);
            this.m_GameOptionsFacade = new GameOptionsFacade(
                                        _GameOptionsRepository, 
                                        this.m_GameSettings, 
                                        this.m_ObjectMapper);
            
            // Load save data
            await this.LoadData();
            await this.ConfigureGameState();
            await this.ConfigureManagers();
        }

        private async Task LoadData()
        {
            await this.m_DataContext.Load();
            Debug.Log("1. Loaded Data");
        }

        private Task ConfigureGameState()
        {
            this.m_GameOptionsFacade.GetGameOptions();
            
            Debug.Log("2. Configured GameState");
            return Task.CompletedTask;
        }
        
        private Task ConfigureManagers()
        {
            this.AudioManager.InitialiseAudioManager();
            this.InputManager.InitialiseInputManager();
            this.UserInterfaceManager.InitialiseUserInterfaceManager();
            this.GameStateManager.InitialiseGameStateManager();
            this.SceneManager.InitialiseSceneManager();
            
            Debug.Log("3.Configured Managers");
            return Task.CompletedTask;
        }

        #endregion Methods
  
    }

}
