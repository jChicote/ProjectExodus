using System;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Entities;
using ProjectExodus.GameLogic.Facades.GameOptionsFacade;
using ProjectExodus.GameLogic.Facades.GameSaveFacade;
using ProjectExodus.GameLogic.Facades.PlayerControllers;
using ProjectExodus.GameLogic.Facades.ShipActionFacade;
using ProjectExodus.GameLogic.Facades.WeaponActionFacade;
using ProjectExodus.GameLogic.Infrastructure.DataLoading;
using ProjectExodus.GameLogic.Infrastructure.Providers;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Mappers.MappingProfiles;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using ProjectExodus.GameLogic.Settings;
using ProjectExodus.ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectExodus.GameLogic.Configuration
{

    public class GameLogicConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField] private GamePrefabAssets m_GamePrefabAssets;

        private readonly IObjectMapper m_ObjectMapper;
        private readonly IObjectMapperRegister m_ObjectMapperRegister;
        private readonly IServiceLocator m_ServiceLocator;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameLogicConfiguration(
            GamePrefabAssets gamePrefabAssets,
            IObjectMapper objectMapper,
            IObjectMapperRegister objectMapperRegister,
            IServiceLocator serviceLocator)
        {
            this.m_GamePrefabAssets = gamePrefabAssets ?? throw new ArgumentNullException(nameof(gamePrefabAssets));
            this.m_ObjectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            this.m_ObjectMapperRegister =
                objectMapperRegister ?? throw new ArgumentNullException(nameof(objectMapperRegister));
            this.m_ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            this.ConfigureGameLogicMappers();
            this.ConfigureLoaders();
            this.ConfigureProviders();
            this.ConfigureUseCaseFacades();
        }

        private void ConfigureGameLogicMappers()
        {
            _ = new GameOptionsMapper(this.m_ObjectMapperRegister);
        }

        private void ConfigureLoaders()
        {
            ISceneLoader _SceneLoader = Object.FindFirstObjectByType<SceneLoader>();
            this.m_ServiceLocator.RegisterService(_SceneLoader);

            IDataLoader _DataLoader = new DataLoader();
            this.m_ServiceLocator.RegisterService(_DataLoader);
        }

        private void ConfigureProviders()
        {
            IWeaponAssetProvider _WeaponAssetProvider = new WeaponAssetProvider(this.m_GamePrefabAssets);
            this.m_ServiceLocator.RegisterService(_WeaponAssetProvider);

            IShipAssetProvider _ShipAssetProvider = new ShipAssetProvider(this.m_GamePrefabAssets);
            this.m_ServiceLocator.RegisterService(_ShipAssetProvider);
        }

        private void ConfigureUseCaseFacades()
        {
            // Tech-Debt: Temporarily will set the GameSettings here until a better location can be found.
            GameSettings _GameSettings = new GameSettings();
            GameManager.Instance.GameSettings = _GameSettings;
            
            IGameOptionsFacade _GameOptionsFacade = new GameOptionsFacade(
                this.m_ServiceLocator.GetService<IDataRepository<GameOptions>>(), 
                _GameSettings, 
                this.m_ObjectMapper);
            _GameOptionsFacade.GetGameOptions();
            // Tech-Debt: This could be changed to be used as some interface adapter. To ensure abstraction.
            this.m_ServiceLocator.RegisterService(_GameOptionsFacade);
            
            if (_GameSettings.GameOptionsModel == null)
                _GameOptionsFacade.CreateGameOptions();
            
            IGameSaveFacade _GameSaveFacade = new GameSaveFacade(
                this.m_ServiceLocator.GetService<IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<DeleteGameSaveInputPort, IDeleteGameSaveOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>>());
            this.m_ServiceLocator.RegisterService(_GameSaveFacade);

            IPlayerControllers _PlayerControllers = new PlayerControllers(
                this.m_ServiceLocator.GetService<IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<UpdatePlayerInputPort, IUpdatePlayerOutputPort>>());
            this.m_ServiceLocator.RegisterService(_PlayerControllers);

            IShipActionFacade _ShipActionFacade = new ShipActionFacade(
                this.m_ServiceLocator.GetService<IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort>>());
            this.m_ServiceLocator.RegisterService(_ShipActionFacade);
            
            IWeaponActionFacade _WeaponActionFacade = new WeaponActionFacade(
                this.m_ServiceLocator.GetService<IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort>>(),
                this.m_ServiceLocator.GetService<IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort>>());
            this.m_ServiceLocator.RegisterService(_WeaponActionFacade);
        }

        #endregion Methods
  
    }

}