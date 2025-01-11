using System;
using System.Collections.Generic;
using UnityEngine;

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
            if (this.m_Mappings.TryGetValue((typeof(TSource), typeof(TDestination)), out _))
            {
                Debug.LogWarning($"[WARNING]: The mapping between '{typeof(TSource)}', " +
                                    $"'{typeof(TDestination)}' already exists.");
                return;
            }
            
            // Wrap to local function to cleanly handle callback with type casting
            void WrappedCallback(object source, object destination) 
                => callback((TSource)source, (TDestination)destination);
            
            this.m_Mappings.Add((typeof(TSource), typeof(TDestination)), WrappedCallback);
        }

        TDestination IObjectMapper.Map<TSource, TDestination>(TSource sourceObject, TDestination destinationObject)
        {
            if (!this.m_Mappings.TryGetValue((typeof(TSource), typeof(TDestination)), out var _Action))
                throw new InvalidOperationException($"No mapping registered for types {typeof(TSource)} " +
                                                    $"and {typeof(TDestination)}.");
            
            _Action.Invoke(sourceObject, destinationObject);

            return destinationObject;
        }

        #endregion Methods

    }

}