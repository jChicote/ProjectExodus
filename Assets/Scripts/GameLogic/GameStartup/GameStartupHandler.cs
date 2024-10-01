using System;
using System.Collections;
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
using ProjectExodus.Management.UserInterfaceManager;
using ProjectExodus.ScriptableObjects;
using ProjectExodus.UserInterface.Configuration;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectExodus.GameLogic.GameStartup
{

    /// <summary>
    /// Handles setup behavior for managers and services...
    /// </summary>
    public class GameStartupHandler : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private ServiceLocator m_ServiceLocator;
        [SerializeField] private UserInterfaceSettings m_UserInterfaceSettings;

        #endregion Fields

        #region - - - - - - Events - - - - - -

        public UnityEvent OnGameStart;
        
        public UnityEvent OnGameSetupCompletion;

        #endregion Events
  
        #region - - - - - - Methods - - - - - -

        public IEnumerator ConfigureGame(Action<GameSetupConfig> onConfigLoaded)
        {
            yield return StartCoroutine(this.SetupBackEndServices());
            yield return StartCoroutine(this.SetupDomainServices());
            yield return StartCoroutine(this.SetupGameLogicServices());
            yield return StartCoroutine(this.SetupUserInterfaceServices());
            // onConfigLoaded.Invoke(_SetupConfig);
            
            yield return StartCoroutine(this.SetupManagers());
            yield return StartCoroutine(this.EndGameSetup());
            
            yield return null;
        }

        private IEnumerator SetupBackEndServices()
        {
            IServiceLocator _ServiceLocator = this.m_ServiceLocator;
            
            // Setup Services and Configuration
            JsonDataContext _DataContext = new JsonDataContext(); // Temporarily be initialised here
            ObjectMapper _ObjectMapper = new ObjectMapper();
            
            _ServiceLocator.RegisterService<IDataContext>(_DataContext);
            _ServiceLocator.RegisterService<IObjectMapper>(_ObjectMapper);
            _ServiceLocator.RegisterService<IObjectMapperRegister>(_ObjectMapper);
            
            ((IConfigure)new BackendConfiguration(_ObjectMapper, _ObjectMapper, _ServiceLocator)).Configure();
            
            yield return null;
        }

        private IEnumerator SetupDomainServices()
        {
            IServiceLocator _ServiceLocator = this.m_ServiceLocator;
            IDataContext _DataContext = _ServiceLocator.GetService<IDataContext>();
            IObjectMapper _ObjectMapper = _ServiceLocator.GetService<IObjectMapper>();
            IObjectMapperRegister _MapperRegister = _ServiceLocator.GetService<IObjectMapperRegister>();
            
            // Load and await completion of loading data
            Task _LoadTask = _DataContext.Load();
            while (!_LoadTask.IsCompleted)
                yield return null;
            
            ((IConfigure)new DomainConfiguration(
                _DataContext,
                _ObjectMapper, 
                _MapperRegister, 
                _ServiceLocator, 
                this.m_UserInterfaceSettings)).Configure();
            
            yield return null;
        }

        private IEnumerator SetupGameLogicServices()
        {
            IServiceLocator _ServiceLocator = this.m_ServiceLocator;
            IObjectMapper _ObjectMapper = _ServiceLocator.GetService<IObjectMapper>();
            IObjectMapperRegister _ObjectMapperRegister = _ServiceLocator.GetService<IObjectMapperRegister>();
            
            ((IConfigure)new GameLogicConfiguration(_ObjectMapper, _ObjectMapperRegister, _ServiceLocator)).Configure();
            
            yield return null;
        }

        private IEnumerator SetupUserInterfaceServices()
        {
            IServiceLocator _ServiceLocator = this.m_ServiceLocator;
            IObjectMapperRegister _ObjectMapperRegister = _ServiceLocator.GetService<IObjectMapperRegister>();
            
            ((IConfigure)new UserInterfaceConfiguration(_ObjectMapperRegister)).Configure();
            
            yield return null;
        }
        
        private IEnumerator SetupManagers()
        {
            IServiceLocator _ServiceLocator = this.m_ServiceLocator;
            IDataContext _DataContext = _ServiceLocator.GetService<IDataContext>();
            
            GameSaveManager _GameSaveManager = 
                GameObject.FindFirstObjectByType<GameSaveManager>() 
                    ?? throw new NullReferenceException(nameof(GameSaveManager));
            ((IGameSaveManager)_GameSaveManager).InitializeGameSaveManager(_DataContext);
            _ServiceLocator.RegisterService((IGameSaveManager)_GameSaveManager);

            UserInterfaceManager _UserInterfaceManager =
                GameObject.FindFirstObjectByType<UserInterfaceManager>() 
                    ?? throw new NullReferenceException(nameof(UserInterfaceManager));
            _ServiceLocator.RegisterService((IUserInterfaceManager)_UserInterfaceManager);
                                            
            GameManager _GameManager = GameManager.Instance;
            _GameManager.AudioManager.InitialiseAudioManager();
            _GameManager.InputManager.InitialiseInputManager();
            _GameManager.GameStateManager.InitialiseGameStateManager();
            _GameManager.SceneManager.InitialiseSceneManager();
                
            yield return null;
        }

        private IEnumerator EndGameSetup()
        {
            this.OnGameSetupCompletion.Invoke();
            yield return null;
        }
        
        #endregion Methods

    }

}