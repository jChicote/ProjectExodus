namespace ProjectExodus
{

    public interface IInitialize<TInitializationData>
    {

        #region - - - - - - Methods - - - - - -

        void Initialize(TInitializationData initializationData);

        #endregion Methods

    }

}