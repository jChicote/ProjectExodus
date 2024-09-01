using ProjectExodus.Common.Services;
using ProjectExodus.Domain.Infrastructure.Providers;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Domain.Configuration
{

    public class DomainConfiguration : IConfigure
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapperRegister m_ObjectMapperRegister;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DomainConfiguration(IObjectMapperRegister objectMapperRegister) 
            => this.m_ObjectMapperRegister = objectMapperRegister;

        #endregion Constructors
        
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            _ = new ProfileImageModalProviderMapper(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

}