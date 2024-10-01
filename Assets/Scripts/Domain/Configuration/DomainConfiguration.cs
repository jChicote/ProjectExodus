using System;
using ProjectExodus.Backend.JsonDataContext;
using ProjectExodus.Backend.Repositories;
using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Backend.Repositories.GameSaveRepository;
using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Entities;
using ProjectExodus.Domain.Infrastructure.Providers;
using ProjectExodus.Domain.Services;
using ProjectExodus.GameLogic.GameStartup;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.ScriptableObjects;

namespace ProjectExodus.Domain.Configuration
{

    public class DomainConfiguration : IConfigure
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IDataContext m_DataContext;
        private readonly IObjectMapper m_Mapper;
        private readonly IObjectMapperRegister m_MapperRegister;
        private readonly IServiceLocator m_ServiceLocator;
        private readonly UserInterfaceSettings m_UserInterfaceSettings;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DomainConfiguration(
            IDataContext dataContext,
            IObjectMapper mapper, 
            IObjectMapperRegister mapperRegister, 
            IServiceLocator serviceLocator, 
            UserInterfaceSettings userInterfaceSettings)
        {
            this.m_DataContext = dataContext 
                    ?? throw new ArgumentNullException(nameof(dataContext));
            this.m_Mapper = mapper 
                    ?? throw new ArgumentNullException(nameof(mapper));
            this.m_MapperRegister = mapperRegister 
                    ?? throw new ArgumentNullException(nameof(mapperRegister));
            this.m_ServiceLocator = serviceLocator 
                    ?? throw new ArgumentNullException(nameof(serviceLocator));
            this.m_UserInterfaceSettings = userInterfaceSettings 
                    ?? throw new ArgumentNullException(nameof(userInterfaceSettings));
        }

        #endregion Constructors
        
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            _ = new ProfileImageModalProviderMapper(this.m_MapperRegister);
            
            // Configure Services
            this.m_ServiceLocator.RegisterService(
                (IProfileImageModelProvider) new ProfileImageModelProvider(this.m_Mapper, this.m_UserInterfaceSettings));
            
            // Register repositories
            GameOptionsRepository _GameOptionsRepository = new GameOptionsRepository(this.m_DataContext, this.m_Mapper);
            GameSaveRepository _GameSaveRepository = new GameSaveRepository(this.m_DataContext);
            this.m_ServiceLocator.RegisterService((IDataRepository<GameOptions>)_GameOptionsRepository);
            this.m_ServiceLocator.RegisterService((IDataRepository<GameSave>)_GameSaveRepository);
        }

        #endregion Methods
  
    }

}