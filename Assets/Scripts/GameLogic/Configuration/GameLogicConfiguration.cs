using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.GameLogic.Mappers.MappingProfiles;
using ProjectExodus.GameLogic.Scene.SceneLoader;
using UnityEngine;

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
            // Mapper Registration
            _ = new GameOptionsMapper(this.m_ObjectMapperRegister);
            
            // Service Registration
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;

            ISceneLoader _SceneLoader = Object.FindFirstObjectByType<SceneLoader>();
            _ServiceLocator.RegisterService(_SceneLoader);
        }

        #endregion Methods
  
    }

}