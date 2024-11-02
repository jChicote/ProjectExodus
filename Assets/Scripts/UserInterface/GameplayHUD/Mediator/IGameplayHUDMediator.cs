using System;

namespace ProjectExodus.UserInterface.GameplayHUD.Mediator
{

    public interface IGameplayHUDMediator
    {
        
        #region - - - - - - Methods - - - - - -

        void Register(GameplayHUDMediatorEvent eventType, Action handler);
        
        void Register<TParameter>(GameplayHUDMediatorEvent eventType, Action<TParameter> handler);

        void Invoke(GameplayHUDMediatorEvent eventType);

        void Invoke<TParameter>(GameplayHUDMediatorEvent eventType, TParameter parameter);

        #endregion Methods
        
    }

}