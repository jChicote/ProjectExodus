namespace ProjectExodus.Common.Services
{

    public interface IServiceLocator
    {

        #region - - - - - - Methods - - - - - -

        void RegisterService<TService>(TService service);

        TService GetService<TService>();

        #endregion Methods

    }

}