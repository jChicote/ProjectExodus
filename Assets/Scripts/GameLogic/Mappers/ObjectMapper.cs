using System;
using System.Collections.Generic;

namespace ProjectExodus.GameLogic.Mappers
{

    public class ObjectMapper : IObjectMapperRegister, IObjectMapper
    {

        #region - - - - - - Fields - - - - - -

        private readonly Dictionary<(Type, Type), Action<object, object>> m_Mappings = new();

        #endregion Fields
  
        #region - - - - - - Methods - - - - - -

        void IObjectMapperRegister.AddMappingAction<TSource, TDestination>(Action<TSource, TDestination> callback)
        {
            // Wrap to local function to cleanly handle callback with type casting
            void WrappedCallback(object source, object destination) 
                => callback((TSource)source, (TDestination)destination);
            
            this.m_Mappings.Add((typeof(TSource), typeof(TDestination)), WrappedCallback);
        }

        void IObjectMapper.Map<TSource, TDestination>(TSource sourceObject, TDestination destinationObject)
        {
            if (!this.m_Mappings.TryGetValue((typeof(TSource), typeof(TDestination)), out var _Action))
                throw new InvalidOperationException($"No mapping registered for types {typeof(TSource)} " +
                                                    $"and {typeof(TDestination)}.");
            
            _Action.Invoke(sourceObject, destinationObject);
        }

        #endregion Methods

    }

}