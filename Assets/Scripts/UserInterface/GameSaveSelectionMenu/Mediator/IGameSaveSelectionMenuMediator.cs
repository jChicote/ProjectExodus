using System;

namespace UserInterface.GameSaveSelectionMenu.Mediator
{

    public interface IGameSaveSelectionMenuMediator
    {

        #region - - - - - - Methods - - - - - -

        void Register(GameSaveMenuEventType eventType, Action handler);
        
        void Register<TParameter>(GameSaveMenuEventType eventType, Action<TParameter> handler);

        void Invoke(GameSaveMenuEventType eventType);

        void Invoke<TParameter>(GameSaveMenuEventType eventType, TParameter parameter);

        #endregion Methods

    }

}