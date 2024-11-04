using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.OptionsMenu.AudioOptions;
using ProjectExodus.UserInterface.OptionsMenu.GraphicsOptions;
using ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions;
using UserInterface.GameSaveSelectionMenu;

namespace ProjectExodus.UserInterface.Configuration
{

    /// <summary>
    /// Responsible for setup configuration of User-Interfaces.
    /// </summary>
    public class UserInterfaceConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapperRegister m_ObjectMapperRegister;
        private readonly IServiceLocator m_ServiceLocator;

        #endregion Fields
   
        #region - - - - - - Constructors - - - - - -

        public UserInterfaceConfiguration(IObjectMapperRegister objectMapperRegister, IServiceLocator serviceLocator)
        {
            this.m_ObjectMapperRegister =
                objectMapperRegister ?? throw new ArgumentNullException(nameof(objectMapperRegister));
            this.m_ServiceLocator = serviceLocator ?? throw new ArgumentNullException(nameof(serviceLocator));
        }

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            this.ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            // Configure Mappers
            _ = new AudioOptionsMapper(this.m_ObjectMapperRegister);
            _ = new GraphicsOptionsMapper(this.m_ObjectMapperRegister);
            _ = new UserInterfaceOptionsMapper(this.m_ObjectMapperRegister);
            
            _ = new GameSaveSelectionMenuMapper(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

}