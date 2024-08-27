namespace ProjectExodus.Common.Services
{

    public interface IMediator
    {

        #region - - - - - - Methods - - - - - -

        void Register<TMediatorConig>(TMediatorConig command) where TMediatorConig : class;

        #endregion Methods

    }

}