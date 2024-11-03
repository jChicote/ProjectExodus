using System;

namespace ProjectExodus.UserInterface.GameplayHUD.Mediator
{

    /// <summary>
    /// Handles actions undertaken on GUI relating to the GameplayHUD.
    /// </summary>
    /// <remarks>The mediator is reset per instance of the screen.</remarks>
    public interface IGameplayHUDMediator
    {
        
        #region - - - - - - Methods - - - - - -

        void Register(GameplayHUDMediatorEvent eventType, Action handler);
        
        void Register<TParameter>(GameplayHUDMediatorEvent eventType, Action<TParameter> handler);

        void Invoke(GameplayHUDMediatorEvent eventType);

        void Invoke<TParameter>(GameplayHUDMediatorEvent eventType, TParameter parameter);

        void ClearRegisteredActions();

        #endregion Methods

    }

}