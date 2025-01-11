using ProjectExodus.Common.Services;

namespace ProjectExodus.Management.Configuration
{

    public class ManagementConfiguration : IConfigure
    {

        #region - - - - - - Methods - - - - - -

        public void Configure()
        {
            IServiceLocator _ServiceLocator = GameManager.Instance.ServiceLocator;
        }
        

        #endregion Methods

    }

}