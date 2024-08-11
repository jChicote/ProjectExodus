using ProjectExodus.GameLogic.Mappers.GameOptionsMapper;

namespace ProjectExodus.GameLogic.Mappers
{

    public class ObjectMapperConfigurator: IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private IObjectMapperRegister m_ObjectMapperRegister;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ObjectMapperConfigurator(IObjectMapperRegister objectMapperRegister) 
            => this.m_ObjectMapperRegister = objectMapperRegister;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            _ = new GameOptionsMappingAction(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

    public interface IConfigure
    {

        #region - - - - - - Methods - - - - - -

        void Configure();

        #endregion Methods

    }

}