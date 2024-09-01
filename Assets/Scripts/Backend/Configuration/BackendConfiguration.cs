using System;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Backend.UseCases;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.CreateGameSave;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.GetGameSaves;
using ProjectExodus.Backend.UseCases.GameSaveUseCases.UpdateGameSave;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.GameStartup;
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

        public BackendConfiguration(SetupGameServicesOptions setupGameServicesOptions)
        {
            this.m_Mapper = 
                setupGameServicesOptions.Mapper 
                    ?? throw new ArgumentNullException(nameof(setupGameServicesOptions.Mapper));
            this.m_MapperRegister =
                setupGameServicesOptions.MapperRegister 
                    ?? throw new ArgumentNullException(nameof(setupGameServicesOptions.MapperRegister));
            this.m_ServiceLocator = 
                setupGameServicesOptions.ServiceLocator 
                    ?? throw new ArgumentNullException(nameof(setupGameServicesOptions.ServiceLocator));
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
            IProfileImageModelProvider _ProfileImageProvider =
                this.m_ServiceLocator.GetService<IProfileImageModelProvider>();
            
            // Game Options
            _ = new CreateGameOptionsMapper(this.m_MapperRegister);
            _ = new GameOptionsRepositoryMapper(this.m_MapperRegister);
            _ = new UpdateGameOptionsMapper(this.m_MapperRegister);
            
            // Game Save
            _ = new CreateGameSaveMapper(this.m_MapperRegister, _ProfileImageProvider);
            _ = new UpdateGameSaveMapper(this.m_MapperRegister);
        }
        
        private void ConfigureUseCases()
        {
            IDataRepository<GameSave> _GameSaveRepository =
                this.m_ServiceLocator.GetService<IDataRepository<GameSave>>();
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<CreateGameSaveInputPort, ICreateGameSaveOutputPort>) 
                    new CreateGameSaveInteractor(this.m_Mapper, _GameSaveRepository));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<GetGameSavesInputPort, IGetGameSavesOutputPort>)
                    new GetGameSavesInteractor(this.m_Mapper, _GameSaveRepository));
            this.m_ServiceLocator.RegisterService(
                (IUseCaseInteractor<UpdateGameSaveInputPort, IUpdateGameSaveOutputPort>)
                    new UpdateGameSaveInteractor(this.m_Mapper, _GameSaveRepository));
        }

        #endregion Methods
  
    }

}