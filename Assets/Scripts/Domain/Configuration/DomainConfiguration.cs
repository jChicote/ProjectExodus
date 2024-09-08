using System;
using ProjectExodus.Common.Services;
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

        private readonly IObjectMapper m_Mapper;
        private readonly IObjectMapperRegister m_MapperRegister;
        private readonly IServiceLocator m_ServiceLocator;
        private readonly UserInterfaceSettings m_UserInterfaceSettings;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DomainConfiguration(SetupGameServicesOptions setupGameServicesOptions)
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
            this.m_UserInterfaceSettings =
                setupGameServicesOptions.UserInterfaceSettings 
                    ?? throw new ArgumentNullException(nameof(setupGameServicesOptions.UserInterfaceSettings));
        }

        #endregion Constructors
        
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            _ = new ProfileImageModalProviderMapper(this.m_MapperRegister);
            
            // Configure Services
            this.m_ServiceLocator.RegisterService(
                (IProfileImageModelProvider) new ProfileImageModelProvider(this.m_Mapper, this.m_UserInterfaceSettings));
        }

        #endregion Methods
  
    }

}