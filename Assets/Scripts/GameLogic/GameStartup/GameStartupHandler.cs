using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using ProjectExodus.Backend.Configuration;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Backend.Repositories.GameSaveRepository;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Configuration;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Configuration;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Infrastructure;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Settings;
using ProjectExodus.Management.GameSaveManager;
using ProjectExodus.Management.UserInterfaceScreenStatesManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.UserInterface.Configuration;
using UnityEngine;

namespace ProjectExodus.GameLogic.GameStartup
{

    public class GameStartupHandler : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private ServiceLocator m_ServiceLocator;
        [SerializeField] private UserInterfaceSettings m_UserInterfaceSettings;

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        public IEnumerator ConfigureGame(Action<GameSetupConfig> onConfigLoaded)
        {
            GameSetupConfig _SetupConfig = new GameSetupConfig();
            
            yield return StartCoroutine(SetupGameServices(_SetupConfig));
            yield return StartCoroutine(SetupGameData(_SetupConfig));
            yield return StartCoroutine(SetupUseCaseFacades(_SetupConfig));
            onConfigLoaded.Invoke(_SetupConfig);
            
            // Invoked separately as it only need to initialise the managers
            // Note: This might need to be DI'ed with services in the future
            yield return StartCoroutine(SetupGameManagers(_SetupConfig));
            
            yield return null;
        }

        private IEnumerator SetupGameServices(GameSetupConfig setupConfig)
        {
            // Setup settings
            GameSettings _GameSettings = new GameSettings();
            
            // Setup Services and Configuration
            JsonDataContext _DataContext = new JsonDataContext(); // Temporarily be initialised here
            ObjectMapper _ObjectMapper = new ObjectMapper();
            
            setupConfig.SetupGameServices(_DataContext, _GameSettings, _ObjectMapper, _ObjectMapper);
            
            yield return null;
        }
        
        private IEnumerator SetupGameData(GameSetupConfig setupConfig)
        {
            // Configure data for GameLogic, Management and UI
            GameOptionsRepository _GameOptionsRepository = new GameOptionsRepository(
                setupConfig.DataContext, 
                setupConfig.ObjectMapper);
            
            GameSaveRepository _GameSaveRepository = new GameSaveRepository(
                setupConfig.DataContext);
            
            setupConfig.SetupRepositories(_GameOptionsRepository);
            
            ((IServiceLocator)this.m_ServiceLocator).RegisterService((IDataRepository<GameSave>)_GameSaveRepository);
            
            // Load and await completion of loading data
            Task _LoadTask = setupConfig.DataContext.Load();
            while (!_LoadTask.IsCompleted)
            {
                yield return null;
            }

            yield return null;
        }

        private IEnumerator SetupUseCaseFacades(GameSetupConfig setupConfig)
        {
            SetupGameServicesOptions _Options = new SetupGameServicesOptions();
            _Options.Mapper = setupConfig.ObjectMapper;
            _Options.MapperRegister = setupConfig.ObjectMapperRegister;
            _Options.ServiceLocator = this.m_ServiceLocator;
            _Options.UserInterfaceSettings = this.m_UserInterfaceSettings;
            
            ((IConfigure)new DomainConfiguration(_Options)).Configure();
            ((IConfigure)new BackendConfiguration(_Options)).Configure();
            ((IConfigure)new GameLogicConfiguration(setupConfig.ObjectMapperRegister)).Configure();
            ((IConfigure)new UserInterfaceConfiguration(setupConfig.ObjectMapperRegister)).Configure();
            
            // Setup GameOptions
            GameOptionsFacade _GameOptionsFacade = new GameOptionsFacade(
                setupConfig.GameOptionsRepository, 
                setupConfig.GameSettings, 
                setupConfig.ObjectMapper);
            
            ((IGameOptionsFacade)_GameOptionsFacade).GetGameOptions();
            
            if (setupConfig.GameSettings.GameOptionsModel == null)
                ((IGameOptionsFacade)_GameOptionsFacade).CreateGameOptions();
            
            // Setup GameSave       
            GameSaveFacade _GameSaveFacade = new GameSaveFacade(
                ((IServiceLocator)this.m_ServiceLocator)
                    .GetService<IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>>(),
                ((IServiceLocator)this.m_ServiceLocator)
                    .GetService<IUseCaseInteractor<DeleteGameSaveInputPort, IDeleteGameSaveOutputPort>>(),
                ((IServiceLocator)this.m_ServiceLocator)
                    .GetService<IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>>(),
                ((IServiceLocator)this.m_ServiceLocator)
                    .GetService<IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>>());

            // Assign to setup configuration
            setupConfig.SetupUseCaseFacades(_GameOptionsFacade, _GameSaveFacade);
            
            yield return null;
        }

        private IEnumerator SetupGameManagers(GameSetupConfig setupConfig)
        {

            GameSaveManager _GameSaveManager = GameObject.FindFirstObjectByType<GameSaveManager>() ??
                                                    throw new NullReferenceException(nameof(GameSaveManager));
            ((IGameSaveManager)_GameSaveManager).InitializeGameSaveManager(setupConfig.DataContext);
            ((IServiceLocator)this.m_ServiceLocator).RegisterService((IGameSaveManager)_GameSaveManager);

            UserInterfaceScreenStateManager _UserInterfaceScreenStateManager =
                GameObject.FindFirstObjectByType<UserInterfaceScreenStateManager>()
                    ?? throw new NullReferenceException(nameof(UserInterfaceScreenStateManager));
            ((IServiceLocator)this.m_ServiceLocator)
                .RegisterService((IUserInterfaceScreenStateManager)_UserInterfaceScreenStateManager);
            
            GameManager _GameManager = GameManager.Instance;
            _GameManager.AudioManager.InitialiseAudioManager();
            _GameManager.InputManager.InitialiseInputManager();
            _GameManager.UserInterfaceManager.InitialiseUserInterfaceManager();
            _GameManager.GameStateManager.InitialiseGameStateManager();
            _GameManager.SceneManager.InitialiseSceneManager();
                
            yield return null;
        }
        
        #endregion Methods

    }

    public class SetupGameServicesOptions
    {

        #region - - - - - - Properties - - - - - -

        public IObjectMapper Mapper { get; set; }
        
        public IObjectMapperRegister MapperRegister { get; set; }
        
        public IServiceLocator ServiceLocator { get; set; }
        
        public UserInterfaceSettings UserInterfaceSettings { get; set; }

        #endregion Properties
  
    }

}