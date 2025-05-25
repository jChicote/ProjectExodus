using System;
using System.Collections.Generic;
using ProjectExodus.Common.Services;
using UnityEngine;

namespace ProjectExodus.GameLogic.Infrastructure
{

    /// <summary>
    /// Responsible for storing references to shared services with a persistent lifetime.
    /// </summary>
    /// <remarks>
    /// Avoid registering services with short lifetimes or is scoped to the lifetime of a scene.
    /// </remarks>
    public class ServiceLocator : MonoBehaviour, IServiceLocator
    {

        #region - - - - - - Fields - - - - - -

        private Dictionary<Type, object> m_RegisteredServices = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IServiceLocator.RegisterService<TService>(TService service)
        {
            var _Type = typeof(TService);
            if (!this.m_RegisteredServices.ContainsKey(_Type))
            {
                this.m_RegisteredServices[_Type] = service;
                Debug.Log($"[LOG]: Service of type '{_Type}' has been registered.");
            }
            else
            {
                Debug.LogWarning($"[WARNING]: Service of type '{_Type}' has already been registered");
            }
        }

        TService IServiceLocator.GetService<TService>()
        {
            var _Type = typeof(TService);
            if (this.m_RegisteredServices.TryGetValue(_Type, out var _Service))
                return (TService)_Service;

            Debug.LogError($"Service of type '{_Type}' is not found.");
            return default;
        }

        #endregion Methods

    }

}