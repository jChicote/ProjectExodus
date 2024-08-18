using ProjectExodus.Common.Services;
using ProjectExodus.GameLogic.Mappers;
using ProjectExodus.UserInterface.OptionsMenu;
using ProjectExodus.UserInterface.OptionsMenu.AudioOptions;
using ProjectExodus.UserInterface.OptionsMenu.GraphicsOptions;
using ProjectExodus.UserInterface.OptionsMenu.UserInterfaceOptions;

namespace ProjectExodus.UserInterface.Configuration
{

    /// <summary>
    /// Responsible for setup configuration of User-Interfaces.
    /// </summary>
    public class UserInterfaceConfiguration : IConfigure
    {

        #region - - - - - - Fields - - - - - -

        private readonly IObjectMapperRegister m_ObjectMapperRegister;

        #endregion Fields
   
        #region - - - - - - Constructors - - - - - -

        public UserInterfaceConfiguration(IObjectMapperRegister objectMapperRegister) 
            => this.m_ObjectMapperRegister = objectMapperRegister;

        #endregion Constructors
  
        #region - - - - - - Methods - - - - - -

        void IConfigure.Configure()
        {
            // Configure Mappers
            _ = new OptionsMenuMapper(this.m_ObjectMapperRegister);
            _ = new AudioOptionsMapper(this.m_ObjectMapperRegister);
            _ = new GraphicsOptionsMapper(this.m_ObjectMapperRegister);
            _ = new UserInterfaceOptionsMapper(this.m_ObjectMapperRegister);
        }

        #endregion Methods
  
    }

}