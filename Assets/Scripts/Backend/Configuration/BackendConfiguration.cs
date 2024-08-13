using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Backend.UseCases.GameOptions.CreateGameOptions;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;

namespace ProjectExodus.Backend.Configuration
{

    public class BackendConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapperRegister m_ObjectMapperRegister;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public BackendConfiguration(IObjectMapperRegister objectMapperRegister) 
            => this.m_ObjectMapperRegister = objectMapperRegister;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            // Configure mappers
            _ = new CreateGameOptionsMapper(this.m_ObjectMapperRegister);
            _ = new GameOptionsRepositoryMapper(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

}