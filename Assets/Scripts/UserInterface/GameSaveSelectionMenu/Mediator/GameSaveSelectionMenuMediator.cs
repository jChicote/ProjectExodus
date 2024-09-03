using System;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface.GameSaveSelectionMenu.Mediator
{

    public class GameSaveSelectionMenuMediator : IGameSaveSelectionMenuMediator
    {

        #region - - - - - - Fields - - - - - -

        private readonly Dictionary<GameSaveMenuEventType, Action> m_ParameterlessHandlers = new();
        private readonly Dictionary<GameSaveMenuEventType, Action<object>> m_ParameterizedHandlers = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IGameSaveSelectionMenuMediator.Register(GameSaveMenuEventType eventType, Action handler)
        {
            if (this.m_ParameterlessHandlers.TryAdd(eventType, handler))
                return;
                    
            Debug.LogError("[Error]: You cannot log the handler more than once.");
        }

        // Action handler expects parameter objects
        void IGameSaveSelectionMenuMediator.Register<TParameter>(
            GameSaveMenuEventType eventType, 
            Action<TParameter> handler)
        {
            // Wrap handler to handle parameter casting
            void WrappedHandler(object parameter)
                => handler((TParameter)parameter);

            if (this.m_ParameterizedHandlers.TryAdd(eventType, WrappedHandler));
        }

        void IGameSaveSelectionMenuMediator.Invoke(GameSaveMenuEventType eventType)
        {
            if (!this.m_ParameterlessHandlers.TryGetValue(eventType, out var _Action))
                Debug.LogWarning($"[WARNING]: Cannot find mediator event '{eventType.ToString()}'.");
            
            _Action.Invoke();
        }

        void IGameSaveSelectionMenuMediator.Invoke<TParameter>(
            GameSaveMenuEventType eventType, 
            TParameter parameter)
        {
            if (!this.m_ParameterizedHandlers.TryGetValue(eventType, out var _Action))
                Debug.LogWarning($"[WARNING]: Cannot find mediator event '{eventType.ToString()}'.");
            
            _Action.Invoke(parameter);
        }

        #endregion Methods
  
    }

    public enum GameSaveMenuEventType
    {
        OnGameSaveSlotSelection,
        OnEmptySlotSelection,
        StartCreatingNewGameSlot,
        StartEditingGameSlot,
        ShowProfileImageSelectionMenu,
        UpdateProfileImageSelection,
        ShowGameSaveSlotSelectionMenu
    }

}