using System;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.Common;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.Common;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.DeleteGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Backend.UseCases.PlayerUseCases.CreatePlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.GetPlayer;
using ProjectExodus.Backend.UseCases.PlayerUseCases.UpdatePlayer;
using ProjectExodus.Backend.UseCases.ShipUseCases.Common;
using ProjectExodus.Backend.UseCases.ShipUseCases.CreateShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.GetShip;
using ProjectExodus.Backend.UseCases.ShipUseCases.UpdateShip;
using ProjectExodus.Backend.UseCases.WeaponUseCases.Common;
using ProjectExodus.Backend.UseCases.WeaponUseCases.CreateWeapon;
using ProjectExodus.Backend.UseCases.WeaponUseCases.GetWeapons;
using ProjectExodus.Backend.UseCases.WeaponUseCases.UpdateWeapon;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.Configuration
{

    public class BackendConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapper m_Mapper;
        private readonly IObjectMapperRegister m_MapperRegister;
        private readonly IServiceLocator m_ServiceLocator;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public BackendConfiguration(
            IObjectMapper mapper, 
            IObjectMapperRegister mapperRegister, 
            IServiceLocator serviceLocator)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_MapperRegister = mapperRegister ?? throw new ArgumentNullException(nameof(mapperRegister));
            this.m_ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            this.ConfigureMappingConfigurations();
            this.ConfigureUseCases();
        }

        private void ConfigureMappingConfigurations()
        {
            IDataContext _DataContext = this.m_ServiceLocator.GetService<IDataContext>();
            IProfileImageModelProvider _ProfileImageProvider =
                this.m_ServiceLocator.GetService<IProfileImageModelProvider>();
            
            // Game Options
            _ = new CreateGameOptionsMapper(this.m_MapperRegister);
            _ = new GameOptionsRepositoryMapper(this.m_MapperRegister);
            _ = new UpdateGameOptionsMapper(this.m_MapperRegister);
            
            // Game Save
            _ = new CreateGameSaveMapper(this.m_MapperRegister);
            _ = new GameSaveMapper(this.m_MapperRegister, _ProfileImageProvider);
            _ = new UpdateGameSaveMapper(this.m_MapperRegister);
            
            // Ship
            _ = new ShipMapper(_DataContext, this.m_Mapper, this.m_MapperRegister);
            _ = new CreateShipMapper(this.m_MapperRegister);
            _ = new UpdateShipMapper(this.m_MapperRegister);
            
            // Player
            _ = new PlayerMapper(_DataContext, this.m_Mapper, this.m_MapperRegister);
            _ = new CreatePlayerMapper(this.m_MapperRegister);
            _ = new UpdatePlayerMapper(this.m_MapperRegister);
            
            // Weapon
            _ = new WeaponMapper(this.m_MapperRegister);
            _ = new CreateWeaponMapper(this.m_MapperRegister);
            _ = new UpdateWeaponMapper(this.m_MapperRegister);
        }
        
        private void ConfigureUseCases()
        {
            IDataContext _DataContext = this.m_ServiceLocator.GetService<IDataContext>();
            IDataRepository<GameSave> _GameSaveRepository =
                this.m_ServiceLocator.GetService<IDataRepository<GameSave>>();
            IDataRepository<Weapon> _WeaponRepository =
                this.m_ServiceLocator.GetService<IDataRepository<Weapon>>();
            
            // GameSave
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>) 
                    new CreateGameSaveInteractor(_DataContext, this.m_Mapper));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<DeleteGameSaveInputPort, IDeleteGameSaveOutputPort>)
                    new DeleteGameSaveInteractor(_GameSaveRepository));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>)
                    new GetGameSavesInteractor(this.m_Mapper, _GameSaveRepository));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>)
                    new UpdateGameSaveInteractor(this.m_Mapper, _GameSaveRepository));
            
            // Weapon
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<CreateWeaponInputPort, ICreateWeaponOutputPort>)
                    new CreateWeaponInteractor(_DataContext, this.m_Mapper));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<GetWeaponInputPort, IGetWeaponOutputPort>)
                    new GetWeaponInteractor(this.m_Mapper, _WeaponRepository));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<UpdateWeaponInputPort, IUpdateWeaponOutputPort>)
                    new UpdateWeaponInteractor(_DataContext, this.m_Mapper));
            
            // Ship
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<CreateShipInputPort, ICreateShipOutputPort>)
                    new CreateShipInteractor(_DataContext, this.m_Mapper));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<GetShipInputPort, IGetShipOutputPort>)
                    new GetShipInteractor(_DataContext, this.m_Mapper));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<UpdateShipInputPort, IUpdateShipOutputPort>)
                    new UpdateShipInteractor(_DataContext, this.m_Mapper));
            
            // Player
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<CreatePlayerInputPort, ICreatePlayerOutputPort>)
                    new CreatePlayerInteractor(_DataContext, this.m_Mapper));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<GetPlayerInputPort, IGetPlayerOutputPort>)
                    new GetPlayerInteractor(_DataContext, this.m_Mapper));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<UpdatePlayerInputPort, IUpdatePlayerOutputPort>)
                    new UpdatePlayerInteractor(_DataContext, this.m_Mapper));
        }

        #endregion Methods
  
    }

}