using System;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Mappers.MappingProfiles;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using ProjectExodus.GameLogic.Settings;
using Object = UnityEngine.Object;

namespace ProjectExodus.GameLogic.Configuration
{

    public class GameLogicConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_ObjectMapper;
        private readonly IObjectMapperRegister m_ObjectMapperRegister;
        private readonly IServiceLocator m_ServiceLocator;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameLogicConfiguration(
            IObjectMapper objectMapper,
            IObjectMapperRegister objectMapperRegister,
            IServiceLocator serviceLocator)
        {
            this.m_ObjectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_ObjectMapperRegister =
                objectMapperRegister ?? throw new ArgumentNullException(nameof(objectMapperRegister));
            this.m_ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            this.ConfigureGameLogicServices();
            this.ConfigureUseCaseFacades();
        }

        private void ConfigureGameLogicServices()
        {
            // Mapper Registration
            _ = new GameOptionsMapper(this.m_ObjectMapperRegister);
            
            // Service Registration
            ISceneLoader _SceneLoader = Object.FindFirstObjectByType<SceneLoader>();
            this.m_ServiceLocator.RegisterService(_SceneLoader);
        }

        private void ConfigureUseCaseFacades()
        {
            // Temporarily will set the GameSettings here until a better location can be found.
            GameSettings _GameSettings = new GameSettings();
            GameManager.Instance.GameSettings = _GameSettings;
            
            GameOptionsFacade _GameOptionsFacade = new GameOptionsFacade(
                this.m_ServiceLocator.GetService<IDataRepository<GameOptions>>(), 
                _GameSettings, 
                this.m_ObjectMapper);
            ((IGameOptionsFacade)_GameOptionsFacade).GetGameOptions();
            // Tech-Debt - # : This could be changed to be used as some interface adapter. To ensure abstraction.
            this.m_ServiceLocator.RegisterService(_GameOptionsFacade);
            
            if (_GameSettings.GameOptionsModel == null)
                ((IGameOptionsFacade)_GameOptionsFacade).CreateGameOptions();
            
            GameSaveFacade _GameSaveFacade = new GameSaveFacade(
                this.m_ServiceLocator.GetService<IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<DeleteGameSaveInputPort, IDeleteGameSaveOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>>());
            // Tech-Debt - # : This could be changed to be used as some interface adapter. To ensure abstraction.
            this.m_ServiceLocator.RegisterService(_GameSaveFacade);
        }

        #endregion Methods
  
    }

}