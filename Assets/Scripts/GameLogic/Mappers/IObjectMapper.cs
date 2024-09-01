namespace ProjectExodus.GameLogic.Mappers
{

    /// <summary>
    /// Custom mapper service for model / data specific objects.
    /// </summary>
    /// <remarks>Currently only intended to handle through references of the source and destination.</remarks>
    public interface IObjectMapper
    {

        #region - - - - - - Methods - - - - - -

        TDestination Map<TSource, TDestination>(TSource sourceObject, TDestination destinationObject);
        
        #endregion Methods

    }

}