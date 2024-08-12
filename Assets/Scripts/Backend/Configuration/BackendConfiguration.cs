using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using UnityEngine;

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
            Debug.LogError("No configuration implemented.");
        }

        #endregion Methods
  
    }

}