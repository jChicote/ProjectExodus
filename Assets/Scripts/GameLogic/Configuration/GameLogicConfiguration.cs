using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Mappers.MappingProfiles;

namespace ProjectExodus.GameLogic.Configuration
{

    public class GameLogicConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapperRegister m_ObjectMapperRegister;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GameLogicConfiguration(IObjectMapperRegister objectMapperRegister) 
            => this.m_ObjectMapperRegister = objectMapperRegister;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            _ = new GameOptionsMapper(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

}