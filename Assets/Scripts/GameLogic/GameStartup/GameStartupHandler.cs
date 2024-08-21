using System;
using System.Collections;
using System.Threading.Tasks;
using ProjectExodus.Backend.Configuration;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Configuration;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Settings;
using ProjectExodus.UserInterface.Configuration;
using UnityEngine;

namespace ProjectExodus.GameLogic.GameStartup
{

    public class GameStartupHandler : MonoBehaviour
    {

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

        private static IEnumerator SetupGameServices(GameSetupConfig setupConfig)
        {
            // Setup settings
            GameSettings _GameSettings = new GameSettings();
            
            // Setup Services and Configuration
            JsonDataContext _DataContext = new JsonDataContext(); // Temporarily be initialised here
            ObjectMapper _ObjectMapper = new ObjectMapper();
            
            ((IConfigure)new BackendConfiguration(_ObjectMapper)).Configure();
            ((IConfigure)new GameLogicConfiguration(_ObjectMapper)).Configure();
            ((IConfigure)new UserInterfaceConfiguration(_ObjectMapper)).Configure();
            
            setupConfig.SetupGameServices(_DataContext, _GameSettings, _ObjectMapper);
            
            yield return null;
        }

        private static IEnumerator SetupUseCaseFacades(GameSetupConfig setupConfig)
        {
            // Setup GameOptions
            GameOptionsFacade _GameOptionsFacade = new GameOptionsFacade(
                setupConfig.GameOptionsRepository, 
                setupConfig.GameSettings, 
                setupConfig.ObjectMapper);
            
            ((IGameOptionsFacade)_GameOptionsFacade).GetGameOptions();
            
            if (setupConfig.GameSettings.GameOptionsModel == null)
                ((IGameOptionsFacade)setupConfig.GameOptionsFacade).CreateGameOptions();

            setupConfig.SetupUseCaseFacades(_GameOptionsFacade);
            
            yield return null;
        }

        private static IEnumerator SetupGameData(GameSetupConfig setupConfig)
        {
            // Configure data for GameLogic, Management and UI
            GameOptionsRepository _GameOptionsRepository = new GameOptionsRepository(
                setupConfig.DataContext, 
                setupConfig.ObjectMapper);
            
            setupConfig.SetupRepositories(_GameOptionsRepository);
            
            // Load and await completion of loading data
            Task _LoadTask = setupConfig.DataContext.Load();
            while (!_LoadTask.IsCompleted)
            {
                yield return null;
            }

            yield return null;
        }

        private static IEnumerator SetupGameManagers(GameSetupConfig setupConfig)
        {
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

}