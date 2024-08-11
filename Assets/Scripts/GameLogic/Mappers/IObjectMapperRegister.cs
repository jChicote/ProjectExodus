using System;

namespace ProjectExodus.GameLogic.Mappers
{
    
    public interface IObjectMapperRegister
    {

        #region - - - - - - Methods - - - - - -

        void AddMappingAction<TSource, TDestination>(Action<TSource, TDestination> callback);
        
        #endregion Methods

    }

}