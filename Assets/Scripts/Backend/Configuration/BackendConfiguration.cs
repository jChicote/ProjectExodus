using ProjectExodus.Backend.Repositories.GameOptionsRepository;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.CreateGameOptions;
using ProjectExodus.Backend.UseCases.GameOptionsUseCases.UpdateGameOptions;
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
            _ = new CreateGameOptionsMapper(this.m_ObjectMapperRegister);
            _ = new GameOptionsRepositoryMapper(this.m_ObjectMapperRegister);
            _ = new UpdateGameOptionsMapper(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

}