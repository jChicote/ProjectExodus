using System;
using ProjectExodus.GameLogic.GameSettings;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Models;
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

        private GameSettings m_GameSettings;
        private ObjectMapper m_ObjectMapper;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public IAudioManager AudioManager
            => this.m_AudioManager;

        public IEventManager EventManager
            => this.m_EventManager;

        public IInputManager InputManager
            => this.m_InputManager;

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
            // Setup settings
            // NOTE: This is temporary until a use case exists to initialise the settings option from a scriptable-object
            //  or from a source JSON file.
            this.m_GameSettings = new GameSettings();
            this.m_GameSettings.GameOptions = new GameOptions();
            
            // Setup Services
            this.m_ObjectMapper = new ObjectMapper();
            ((IConfigure)new ObjectMapperConfigurator(this.m_ObjectMapper)).Configure();
            
            // Setup managers
            this.AudioManager.InitialiseAudioManager();
            this.InputManager.InitialiseInputManager();
            this.UserInterfaceManager.InitialiseUserInterfaceManager();
            this.GameStateManager.InitialiseGameStateManager();
            this.SceneManager.InitialiseSceneManager();
        }

        #endregion Methods
  
    }

}