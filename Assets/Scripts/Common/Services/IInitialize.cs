namespace ProjectExodus
{

    public interface IInitialize<TInitializationData>
    {

        #region - - - - - - Methods - - - - - -

        void Initialize(TInitializationData initializationData);
        
        #endregion Methods

    }

    /// <summary>
    /// Custom initializer that is managed by the custom setup logic
    /// </summary>
    /// <remarks>This is not a replacement for Unity's 'Awake' method</remarks>
    public interface IInitialize
    {

        #region - - - - - - Methods - - - - - -

        void Initialize();

        #endregion Methods

    }

}