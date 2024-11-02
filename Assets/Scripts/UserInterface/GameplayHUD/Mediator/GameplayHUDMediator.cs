using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD.Mediator
{

    public class GameplayHUDMediator : IGameplayHUDMediator
    {
        
        #region - - - - - - Fields - - - - - -

        private readonly Dictionary<GameplayHUDMediatorEvent, List<Action>> m_ParameterlessHandlers = new();
        private readonly Dictionary<GameplayHUDMediatorEvent, List<Action<object>>> m_ParameterizedHandlers = new();

        #endregion Fields
        
        #region - - - - - - Methods - - - - - -

        void IGameplayHUDMediator.Register(GameplayHUDMediatorEvent eventType, Action handler)
        {
            if (this.m_ParameterlessHandlers.TryGetValue(eventType, out var _Action))
            {
                _Action.Add(handler);
                return;
            }
            
            if (!this.m_ParameterlessHandlers.TryAdd(eventType, new List<Action> { handler }))
                Debug.LogError("[Error]: Registered handler cannot be added.");
        }

        void IGameplayHUDMediator.Register<TParameter>(
            GameplayHUDMediatorEvent eventType, 
            Action<TParameter> handler)
        {
            // Wrap handler to handle parameter casting
            void WrappedHandler(object parameter)
                => handler((TParameter)parameter);
            
            if (this.m_ParameterizedHandlers.TryGetValue(eventType, out var _Action))
            {
                _Action.Add(WrappedHandler);
                return;
            }

            if (!this.m_ParameterizedHandlers.TryAdd(eventType, new List<Action<object>> { WrappedHandler }))
                Debug.LogError("[Error]: Registered handler cannot be added.");
        }

        void IGameplayHUDMediator.Invoke(GameplayHUDMediatorEvent eventType)
        {
            if (!this.m_ParameterlessHandlers.TryGetValue(eventType, out var _Actions))
                Debug.LogWarning($"[WARNING]: Cannot find mediator event '{eventType.ToString()}'.");

            foreach (Action _ActionHandler in _Actions) 
                _ActionHandler.Invoke();
        }

        void IGameplayHUDMediator.Invoke<TParameter>(
            GameplayHUDMediatorEvent eventType, 
            TParameter parameter)
        {
            if (!this.m_ParameterizedHandlers.TryGetValue(eventType, out var _Actions))
                Debug.LogWarning($"[WARNING]: Cannot find mediator event '{eventType.ToString()}'.");
            
            foreach (Action<object> _ActionHandler in _Actions) 
                _ActionHandler.Invoke(parameter);
        }

        #endregion Methods
        
    }

    public enum GameplayHUDMediatorEvent
    {
        AmmoCountBar_Update,
        HealthBars_Update,
    }

}