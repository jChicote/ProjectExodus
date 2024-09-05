using System;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface.GameSaveSelectionMenu.Mediator
{

    public class GameSaveSelectionMenuMediator : IGameSaveSelectionMenuMediator
    {

        #region - - - - - - Fields - - - - - -

        private readonly Dictionary<GameSaveMenuEventType, List<Action>> m_ParameterlessHandlers = new();
        private readonly Dictionary<GameSaveMenuEventType, List<Action<object>>> m_ParameterizedHandlers = new();

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        void IGameSaveSelectionMenuMediator.Register(GameSaveMenuEventType eventType, Action handler)
        {
            if (this.m_ParameterlessHandlers.TryGetValue(eventType, out var _Action))
            {
                _Action.Add(handler);
                return;
            }
            
            if (!this.m_ParameterlessHandlers.TryAdd(eventType, new List<Action> { handler }))
                Debug.LogError("[Error]: Registered handler cannot be added.");
        }

        // Action handler expects parameter objects
        void IGameSaveSelectionMenuMediator.Register<TParameter>(
            GameSaveMenuEventType eventType, 
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

        void IGameSaveSelectionMenuMediator.Invoke(GameSaveMenuEventType eventType)
        {
            if (!this.m_ParameterlessHandlers.TryGetValue(eventType, out var _Actions))
                Debug.LogWarning($"[WARNING]: Cannot find mediator event '{eventType.ToString()}'.");

            foreach (Action _ActionHandler in _Actions) 
                _ActionHandler.Invoke();
        }

        void IGameSaveSelectionMenuMediator.Invoke<TParameter>(
            GameSaveMenuEventType eventType, 
            TParameter parameter)
        {
            if (!this.m_ParameterizedHandlers.TryGetValue(eventType, out var _Actions))
                Debug.LogWarning($"[WARNING]: Cannot find mediator event '{eventType.ToString()}'.");
            
            foreach (Action<object> _ActionHandler in _Actions) 
                _ActionHandler.Invoke(parameter);
        }

        #endregion Methods
  
    }

    public enum GameSaveMenuEventType
    {
        // Game Save Screen
        GameSaveSlot_Selected,
        EmptySaveSlot_Selected,
        GameSaveMenuInteraction_Enabled,
        
        // Edit Game Slot Modal
        CreateNewGameSlot_Open,
        EditGameSlot_Open,
        EditGameSlotImage_Update,
        
        // Profile Image Selection Modal
        ProfileImageSelectionModal_Open
    }

}