using System;
using ProjectExodus.UserInterface.GameplayHUD.Mediator;
using UnityEngine;

namespace ProjectExodus.UserInterface.GameplayHUD
{

    public class GameplayHUDController : MonoBehaviour, IGameplayHUDController, IScreenStateController
    {

        #region - - - - - - Fields - - - - - -

        private IGameplayHUDMediator m_Mediator;
        private IGameplayHUDView m_View;

        #endregion Fields
  
        #region - - - - - - Initializers - - - - - -

        void IGameplayHUDController.Initialize(IGameplayHUDMediator gameplayHUDMediator) 
            => this.m_Mediator =
                gameplayHUDMediator ?? throw new ArgumentNullException(nameof(gameplayHUDMediator));

        #endregion Initializers

        #region - - - - - - Methods - - - - - -

        void IGameplayHUDController.SetHUDValues(GameplayHUDValues values) 
            => this.m_View.SetGameplayHUDValues(values);

        void IScreenStateController.HideScreen() 
            => this.m_Mediator.Invoke(GameplayHUDMediatorEvent.GameplayHUD_InVisible);

        void IScreenStateController.ShowScreen()
            => this.m_Mediator.Invoke(GameplayHUDMediatorEvent.GameplayHUD_Visible);

        #endregion Methods

    }

}