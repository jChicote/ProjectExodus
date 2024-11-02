using System;
using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using ProjectExodus.UserInterface.OptionsMenu;
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
            this.ConfigureMediators();
        }

        private void ConfigureMappings()
        {
            // Configure Mappers
            _ = new AudioOptionsMapper(this.m_ObjectMapperRegister);
            _ = new GraphicsOptionsMapper(this.m_ObjectMapperRegister);
            _ = new UserInterfaceOptionsMapper(this.m_ObjectMapperRegister);
            
            _ = new GameSaveSelectionMenuMapper(this.m_ObjectMapperRegister);
        }

        private void ConfigureMediators()
        {
            IGameplayHUDMediator _GameplayHUDMediator = new GameplayHUDMediator();
            this.m_ServiceLocator.RegisterService(_GameplayHUDMediator);
        }

        #endregion Methods
  
    }

}